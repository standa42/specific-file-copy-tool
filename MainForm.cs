using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Channels;
using Tridic.Algorithm;
using Tridic.Utils;

namespace Tridic
{
    public partial class MainForm : Form
    {
        private Status status { get; set; }
        private ConcurrentQueue<(UIMessageType, string)> channel { get; set; }
        private CopyAlgorithm copyAlgorithm { get; set; }

        public MainForm()
        {
            InitializeComponent();

            copyAlgorithm = new CopyAlgorithm();
            channel = new ConcurrentQueue<(UIMessageType, string)>();
            status = Status.NotRunning;
        }

        private void SourceFolderButton_Click(object sender, EventArgs e)
        {
            SourceFolderTextBox.Text = SelectFolder();
        }

        private void TargetFolderButton_Click(object sender, EventArgs e)
        {
            TargetFolderTextBox.Text = SelectFolder();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (status == Status.NotRunning)
            {
                AlgorithmStartUI();
            }
            else if (status == Status.Running)
            {
                AlgorithmStopUI();
            }
        }

        private void ReadProgressTimer_Tick(object sender, EventArgs e)
        {
            if (copyAlgorithm.FileCount != null && copyAlgorithm.FileCount > 0)
            {
                StatusLabel.Text = $"Status: zkopírováno {copyAlgorithm.CopiedFilesCounter} z {copyAlgorithm.FileCount}, souborů s chybami v kopírování: {copyAlgorithm.CopyErrors}";
                ProgressBar.Value = Math.Clamp((int)Math.Floor(((float)copyAlgorithm.CopiedFilesCounter / copyAlgorithm.FileCount.Value) * 100), 0, 100);
            }


            (UIMessageType, string) item;

            while (!channel.IsEmpty)
            {
                if (channel.TryDequeue(out item))
                {
                    switch (item.Item1)
                    {
                        case UIMessageType.Info:
                            LogListBox.Items.Add($"Info: {item.Item2}");
                            break;
                        case UIMessageType.Error:
                            LogListBox.Items.Add($"Error: {item.Item2}");
                            break;
                        case UIMessageType.Finish:
                            LogListBox.Items.Add($"Done: {item.Item2}");
                            AlgorithmStopUI();
                            break;
                        case UIMessageType.FileCopyError:
                            LogListBox.Items.Add($"Error: {item.Item2}");
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private string SelectFolder()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            else
            {
                return "Issues with selecting folder";
            }
        }

        private void AlgorithmStartUI()
        {
            StartButton.Enabled = false;

            if (string.IsNullOrEmpty(SourceFolderTextBox.Text) || string.IsNullOrEmpty(TargetFolderTextBox.Text))
            {
                MessageBox.Show("Prosím vyplňte výchozí i cílovou složku", "Nevyplněné cesty", MessageBoxButtons.OK);
                StartButton.Enabled = true;
                return;
            }

            LogListBox.Items.Clear();
            StatusLabel.Text = "Status: Started";
            ProgressBar.Value = 0;

            copyAlgorithm.RunAsync(
                new AlgorithmParameters(
                    SourceFolderTextBox.Text,
                    TargetFolderTextBox.Text,
                    channel)
                );

            status = Status.Running;
            StartButton.Text = "Stop";
            StartButton.Enabled = true;
        }

        private void AlgorithmStopUI()
        {
            StartButton.Enabled = false;

            copyAlgorithm.Stop();

            StatusLabel.Text = "Status: Finished";
            if ((copyAlgorithm?.FileCount == null ? -1 : copyAlgorithm.FileCount.Value) == copyAlgorithm.CopiedFilesCounter)
                ProgressBar.Value = 100;

            status = Status.NotRunning;
            StartButton.Text = "Start";
            StartButton.Enabled = true;
        }

        private void TargetFolderTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SourceFolderTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}