using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tridic.Utils
{
    internal class DataGenerator
    {
        static void Run()
        {
            //int numberOfFiles = 250; // 250 = 50gb
            //int fileSize = 1024 * 1024 * 200; // in bytes = 1kb, 1mb, 200mb

            int numberOfFiles = 20000; // 20k 1mb files = 20gb
            int fileSize = 1024 * 1024 * 1; // in bytes = 1kb, 1mb

            Random rng = new Random();

            string dirPath = "---";

            for (int i = 0; i < numberOfFiles; i++)
            {
                var randomFilename = $"{RandomDigits(3, rng)}-{RandomDigits(3, rng)}-{RandomDigits(3, rng)}-{RandomDigits(1, rng)}.txt";
                var generatedPath = Path.Combine(dirPath, randomFilename);
                using (var fileStream = new FileStream(generatedPath, FileMode.Create))
                {
                    byte[] data = new byte[fileSize];
                    rng.NextBytes(data); // Fill the array with random bytes
                    fileStream.Write(data, 0, data.Length);
                }
            }

            Console.WriteLine("File generation complete.");
        }
        static string RandomDigits(int length, Random rng)
        {
            string digits = "";
            for (int i = 0; i < length; i++)
            {
                digits += rng.Next(0, 10).ToString();
            }
            return digits;
        }
    }
}
