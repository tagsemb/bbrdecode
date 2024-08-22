// NuGet console: Install-Package DotNetZip

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zlib;

namespace BBRDecode
{
    internal class Program
    {
        public static byte[] DecompressZlib(byte[] compressedData)
        {
            using (var compressedStream = new MemoryStream(compressedData))
            using (var zlibStream = new ZlibStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zlibStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

        static void Main(string[] args)
        {
            // Specify the path to your base64-encoded file
            string filePath = "../../data/replays/2024-08-19_18-33_1-4-0-0_7c511a4d-5e59-11ef-be7b-bc24112ec32e.bbr";

            try
            {
                System.Console.WriteLine("Reading text");
                string base64EncodedContent = System.IO.File.ReadAllText(filePath);

                System.Console.WriteLine("Decoding base64");
                byte[] data = Convert.FromBase64String(base64EncodedContent);

                System.Console.WriteLine("Decompressing data");
                byte[] decompressed = DecompressZlib(data);

                System.Console.WriteLine("Getting string");
                string decompressedString = System.Text.Encoding.UTF8.GetString(decompressed);

                // Display the decoded string
                // Console.WriteLine(decompressedString);

                string outPath = Directory.GetCurrentDirectory() + "/out.xml";
                System.IO.File.WriteAllText(outPath, decompressedString);
                Console.WriteLine("XML written to " + outPath);
            }
            catch (Exception ex)
            {
                // Handle exceptions such as file not found or invalid base64 content
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
