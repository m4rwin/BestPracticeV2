using PdfToImage;
using System;

namespace IO
{
    public class Conversation
    {
        public static void FromPDF2TIFF()
        {
            Console.WriteLine("\nConverting PDF to TIFF:\n");
            using (PDFConvert converter = new PDFConvert())
            {
                //PDFConvert converter = new PDFConvert();
                converter.ThrowOnlyException = true;
                converter.UseMutex = true;
                converter.TextAlphaBit = 0;
                converter.FirstPageToConvert = -1;
                converter.LastPageToConvert = -1;
                converter.FitPage = false;
                converter.JPEGQuality = 10;
                converter.OutputFormat = "tiffg4";
                converter.ResolutionX = 300;
                converter.ResolutionY = 300;
                converter.Convert("file.pdf", "result.tiff");
            }
        }
    }
}
