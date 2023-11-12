using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tridic.Utils;

namespace Tridic.Algorithm
{
    internal class CopyAlgorithm
    {
        public int CopiedFilesCounter;
        public int CopyErrors;
        public int? FileCount = null;

        private CancellationTokenSource? cts { get; set; }

        public void RunAsync(AlgorithmParameters algParams)
        {
            CopiedFilesCounter = 0;
            CopyErrors = 0;
            FileCount = null;
            cts = new CancellationTokenSource();

            Task.Run(() => Run(algParams, cts));
        }

        public void Stop()
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        private async void Run(AlgorithmParameters algParams, CancellationTokenSource cts)
        {
            var degreeOfParalelism = Math.Clamp(Environment.ProcessorCount - 1, 1, int.MaxValue);
            var semaphore = new SemaphoreSlim(degreeOfParalelism, degreeOfParalelism);

            SendToUIThread(algParams.Channel, UIMessageType.Info, $"Start, paralelismus: {degreeOfParalelism}");
            SendToUIThread(algParams.Channel, UIMessageType.Info, $"Přesun ze složky {algParams.SourceFolder}");
            SendToUIThread(algParams.Channel, UIMessageType.Info, $"Do složky {algParams.TargetFolder}");

            var tasks = new List<Task>();

            var directoryInfo = new DirectoryInfo(algParams.SourceFolder);

            SendToUIThread(algParams.Channel, UIMessageType.Info, $"Počítání souborů k přesunu");
            FileCount = directoryInfo
                   .EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
                   .Count();
            SendToUIThread(algParams.Channel, UIMessageType.Info, $"Celkově k přesunu {FileCount} souborů");

            var sourceFiles = Directory.EnumerateFiles(algParams.SourceFolder);

            foreach (var sourceFile in sourceFiles)
            {
                try
                {
                    cts.Token.ThrowIfCancellationRequested();

                    var destinationFile = GetTargetFile(sourceFile, algParams.TargetFolder);

                    await semaphore.WaitAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    SendToUIThread(algParams.Channel, UIMessageType.Info, "Kopírování přerušeno");
                    break;
                }
                
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        var targetFile = GetTargetFile(sourceFile, algParams.TargetFolder);
                        CopyFile(sourceFile, targetFile);
                        Interlocked.Increment(ref CopiedFilesCounter);
                    }
                    catch(Exception ex)
                    {
                        Interlocked.Increment(ref CopyErrors);
                        SendToUIThread(algParams.Channel, UIMessageType.FileCopyError, $"Error při kopírování souboru {sourceFile}");
                        throw;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cts.Token));
            }

            SendToUIThread(algParams.Channel, UIMessageType.Info, "Probíhá přesouvání posledních souborů");
            await Task.WhenAll(tasks.Where(t => t.Status != TaskStatus.Canceled));
            SendToUIThread(algParams.Channel, UIMessageType.Finish, $"Kopírování dokončeno");
            SendToUIThread(algParams.Channel, UIMessageType.Info, $"Celkově zkopírováno {CopiedFilesCounter} z {FileCount} souborů, počet chyb {CopyErrors}");
        }

        private string GetTargetFile(string sourceFile, string targetFolder)
        {
            var tokens = sourceFile.Split('\\');
            var filename = tokens.Last();

            var first = filename.Substring(0, 3);
            var second = filename.Substring(0, 5);
            var third = filename.Substring(0, 6);
            var forth = filename.Substring(0, 7);
            var fifth = filename.Substring(0, 9);

            return Path.Combine(targetFolder, first, second, third, forth, fifth, filename);
        }

        private static void CopyFile(string sourceFile, string destinationFile)
        {
            CreateDirectory(destinationFile);

            // https://stackoverflow.com/questions/882686/asynchronous-file-copy-move-in-c-sharp
            var openForReading = new FileStreamOptions { Mode = FileMode.Open };
            using FileStream source = new FileStream(sourceFile, openForReading);

            var createForWriting = new FileStreamOptions
            {
                Mode = FileMode.Create,
                Access = FileAccess.Write,
                Options = FileOptions.WriteThrough,
                BufferSize = 0, // disable FileStream buffering
                PreallocationSize = source.Length // specify size up-front
            };
            using FileStream destination = new FileStream(destinationFile, createForWriting);
            source.CopyTo(destination);
        }

        private static void CreateDirectory(string filePath)
        {
            bool success = true;
            Exception exception = null;

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    string destinationDirectory = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }
                    continue;
                }
                catch (IOException ex)
                {
                    exception = ex;
                    success = false;
                    Thread.Sleep(50);
                }
            }

            if (!success)
                throw exception;
        }

        private static void SendToUIThread(ConcurrentQueue<(UIMessageType, string)> channel, UIMessageType key, string message)
        {
            channel.Enqueue((key, message));
        }
    }
}
