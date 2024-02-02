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
            string filePath = "../../data/replays/2024-02-01_16-56_1-3-0-0_e1f6146a-c122-11ee-a745-02000090a64f.bbr";

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
                Console.WriteLine(decompressedString);
            }
            catch (Exception ex)
            {
                // Handle exceptions such as file not found or invalid base64 content
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
