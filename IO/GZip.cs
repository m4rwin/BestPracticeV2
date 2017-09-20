using System;
using System.IO;
using System.IO.Compression;

namespace IO
{
    public class GZip
    {
        public static void ArchiveByGZip()
        {
            Console.WriteLine("(GZip) Archiving...");

            int buggerSize = 16384;
            //bool compress = true;
            byte[] buffer = new byte[buggerSize];
            long sourceSize = -1, destSize = -1;
            using (Stream inFileStream = File.Open("source.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                sourceSize = inFileStream.Length;
                using (Stream outFileStream = File.Open("dest.gzip", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (GZipStream gzipStream = new GZipStream(outFileStream, CompressionMode.Compress))
                    {
                        Stream inStream = inFileStream;
                        Stream outStream = gzipStream;

                        int bytesRead = 0;
                        do
                        {
                            bytesRead = inStream.Read(buffer, 0, buggerSize);
                            outStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                        destSize = outFileStream.Length;
                    }
                }
            }

            Console.WriteLine("(GZip) Archive done.");
            Console.WriteLine("Source size: {0}kb, Result: {1}kb", sourceSize / 1024, destSize / 1024);
        }
    }
}
