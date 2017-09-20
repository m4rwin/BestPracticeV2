namespace IO
{
    class Program
    {
        static void Main(string[] args)
        {
            /* GZip compression */
            GZip.ArchiveByGZip();

            /* PDF to TIFF conversation */
            Conversation.FromPDF2TIFF();
        }
    }
}
