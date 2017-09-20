using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

namespace PdfToImage
{
    public class PDFConvert : IDisposable
    {
        IntPtr _objHandle;
        IntPtr callerHandle, intptrArgs;
        IntPtr[] aPtrArgs;
        GCHandle[] aGCHandle;
        GCHandle gchandleArgs;
        IntPtr intGSInstanceHandle = IntPtr.Zero;
        IntPtr ptrByte;

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);
        [DllImport(GhostScriptDLLName, EntryPoint = "gsapi_new_instance")]
        private static extern int gsapi_new_instance(out IntPtr pinstance, IntPtr caller_handle);
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args")]
        private static extern int gsapi_init_with_args(IntPtr instance, int argc, IntPtr argv);
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit")]
        private static extern int gsapi_exit(IntPtr instance);
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance")]
        private static extern void gsapi_delete_instance(IntPtr instance);
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_revision")]
        private static extern int gsapi_revision(ref GS_Revision pGSRevisionInfo, int intLen);
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_stdio")]
        private static extern int gsapi_set_stdio(IntPtr lngGSInstance, StdioCallBack gsdll_stdin, StdioCallBack gsdll_stdout, StdioCallBack gsdll_stderr);

        #region var
        public const string GhostScriptDLLName = "gsdll32.dll";
        private static bool useSimpleAnsiConversion = true;
        private const string GS_OutputFileFormat = "-sOutputFile={0}";
        private const string GS_DeviceFormat = "-sDEVICE={0}";
        private const string GS_FirstParameter = "pdf2img";
        private const string GS_ResolutionXFormat = "-r{0}";
        private const string GS_ResolutionXYFormat = "-r{0}x{1}";
        private const string GS_GraphicsAlphaBits = "-dGraphicsAlphaBits={0}";
        private const string GS_TextAlphaBits = "-dTextAlphaBits={0}";
        private const string GS_FirstPageFormat = "-dFirstPage={0}";
        private const string GS_LastPageFormat = "-dLastPage={0}";
        private const string GS_FitPage = "-dPDFFitPage";
        private const string GS_PageSizeFormat = "-g{0}x{1}";
        private const string GS_DefaultPaperSize = "-sPAPERSIZE={0}";
        private const string GS_JpegQualityFormat = "-dJPEGQ={0}";
        private const string GS_RenderingThreads = "-dNumRenderingThreads={0}";
        private const string GS_Fixed1stParameter = "-dNOPAUSE";
        private const string GS_Fixed2ndParameter = "-dBATCH";
        private const string GS_Fixed3rdParameter = "-dSAFER";
        private const string GS_FixedMedia = "-dFIXEDMEDIA";
        private const string GS_QuiteOperation = "-q";
        private const string GS_StandardOutputDevice = "-";
        private const string GS_MultiplePageCharacter = "%";
        private const string GS_FontPath = "-sFONTPATH={0}";
        private const string GS_NoPlatformFonts = "-dNOPLATFONTS";
        private const string GS_NoFontMap = "-dNOFONTMAP";
        private const string GS_FontMap = "-sFONTMAP={0}";
        private const string GS_SubstitutionFont = "-sSUBSTFONT={0}";
        private const string GS_FCOFontFile = "-sFCOfontfile={0}";
        private const string GS_FAPIFontMap = "-sFAPIfontmap={0}";
        private const string GS_NoPrecompiledFonts = "-dNOCCFONTS";
        private static System.Threading.Mutex mutex;
        const int e_Quit = -101;
        const int e_NeedInput = -106;
        private string _sDeviceFormat;
        private string _sParametersUsed;
        private int _iWidth;
        private int _iHeight;
        private int _iResolutionX;
        private int _iResolutionY;
        private int _iJPEGQuality;
        private int _iFirstPageToConvert = -1;
        private int _iLastPageToConvert = -1;
        private int _iGraphicsAlphaBit = -1;
        private int _iTextAlphaBit = -1;
        private int _iRenderingThreads = -1;
        public int RenderingThreads
        {
            get { return _iRenderingThreads; }
            set
            {
                if (value == 0)
                    _iRenderingThreads = Environment.ProcessorCount;
                else
                    _iRenderingThreads = value;
            }
        }
        private bool _bFitPage;
        private bool _bThrowOnlyException = false;
        private bool _bRedirectIO = false;
        private bool _bForcePageSize = false;
        private string _sDefaultPageSize;
        private bool _didOutputToMultipleFile = false;
        private System.Diagnostics.Process myProcess;
        public StringBuilder output;
        private List<string> _sFontPath = new List<string>();
        private bool _bDisablePlatformFonts = false;
        private bool _bDisableFontMap = false;
        private List<string> _sFontMap = new List<string>();
        private string _sSubstitutionFont;
        private string _sFCOFontFile;
        private string _sFAPIFontMap;
        private bool _bDisablePrecompiledFonts = false;
        #endregion

        #region Proprieties
        public string OutputFormat
        {
            get { return _sDeviceFormat; }
            set { _sDeviceFormat = value; }
        }
        public string DefaultPageSize
        {
            get { return _sDefaultPageSize; }
            set { _sDefaultPageSize = value; }
        }
        public bool ForcePageSize
        {
            get { return _bForcePageSize; }
            set { _bForcePageSize = value; }
        }
        public string ParametersUsed
        {
            get { return _sParametersUsed; }
            set { _sParametersUsed = value; }
        }
        public int Width
        {
            get { return _iWidth; }
            set { _iWidth = value; }
        }
        public int Height
        {
            get { return _iHeight; }
            set { _iHeight = value; }
        }
        public int ResolutionX
        {
            get { return _iResolutionX; }
            set { _iResolutionX = value; }
        }
        public int ResolutionY
        {
            get { return _iResolutionY; }
            set { _iResolutionY = value; }
        }
        public int GraphicsAlphaBit
        {
            get { return _iGraphicsAlphaBit; }
            set
            {
                if ((value > 4) | (value == 3))
                    throw new ArgumentOutOfRangeException("The Graphics Alpha Bit must have a value between 1 2 and 4, or <= 0 if not set");
                _iGraphicsAlphaBit = value;
            }
        }
        public int TextAlphaBit
        {
            get { return _iTextAlphaBit; }
            set
            {
                if ((value > 4) | (value == 3))
                    throw new ArgumentOutOfRangeException("The Text Alpha Bit must have a value between 1 2 and 4, or <= 0 if not set");
                _iTextAlphaBit = value;
            }
        }
        public Boolean FitPage
        {
            get { return _bFitPage; }
            set { _bFitPage = value; }
        }
        public int JPEGQuality
        {
            get { return _iJPEGQuality; }
            set { _iJPEGQuality = value; }
        }
        public int FirstPageToConvert
        {
            get { return _iFirstPageToConvert; }
            set { _iFirstPageToConvert = value; }
        }
        public int LastPageToConvert
        {
            get { return _iLastPageToConvert; }
            set { _iLastPageToConvert = value; }
        }
        public Boolean ThrowOnlyException
        {
            get { return _bThrowOnlyException; }
            set { _bThrowOnlyException = value; }
        }
        public bool RedirectIO
        {
            get { return _bRedirectIO; }
            set { _bRedirectIO = value; }
        }
        public bool OutputToMultipleFile
        {
            get { return _didOutputToMultipleFile; }
            set { _didOutputToMultipleFile = value; }
        }
        public bool UseMutex
        {
            get { return mutex != null; }
            set
            {
                if (!value)//if i don't want to use it
                {
                    if (mutex != null)//if it exist
                    {   //close and delete it
                        mutex.ReleaseMutex();
                        mutex.Close();
                        mutex = null;
                    }
                }
                else//If i want to use mutex create it if it doesn't exist
                {
                    if (mutex == null)
                        mutex = new System.Threading.Mutex(false, "MutexGhostscript");
                }
            }
        }
        public List<string> FontPath
        {
            get { return _sFontPath; }
            set { _sFontPath = value; }
        }

        public bool DisablePlatformFonts
        {
            get { return _bDisablePlatformFonts; }
            set { _bDisablePlatformFonts = value; }
        }

        public bool DisableFontMap
        {
            get { return _bDisableFontMap; }
            set { _bDisableFontMap = value; }
        }

        public List<string> FontMap
        {
            get { return _sFontMap; }
            set { _sFontMap = value; }
        }

        public string SubstitutionFont
        {
            get { return _sSubstitutionFont; }
            set { _sSubstitutionFont = value; }
        }

        public string FCOFontFile
        {
            get { return _sFCOFontFile; }
            set { _sFCOFontFile = value; }
        }

        public string FAPIFontMap
        {
            get { return _sFAPIFontMap; }
            set { _sFAPIFontMap = value; }
        }

        public bool DisablePrecompiledFonts
        {
            get { return _bDisablePrecompiledFonts; }
            set { _bDisablePrecompiledFonts = value; }
        }
        #endregion

        #region c-tor
        public PDFConvert(IntPtr objHandle) 
        {
            _objHandle = objHandle;
        }

        public PDFConvert()
        {
            _objHandle = IntPtr.Zero;
        }
        #endregion

        #region d-tor
        //~PDFConvert()
        //{
            
        //    aPtrArgs = new IntPtr[0];
        //    aGCHandle = new GCHandle[0];
        //    gchandleArgs = new GCHandle();

        //    if (_objHandle != IntPtr.Zero)
        //    {
        //        CloseHandle(_objHandle);
        //    }

        //    if (callerHandle != IntPtr.Zero)
        //    {
        //        CloseHandle(callerHandle);
        //    }

        //    if (ptrByte != IntPtr.Zero)
        //    {
        //        CloseHandle(ptrByte);
        //    }

        //    if (intGSInstanceHandle != IntPtr.Zero)
        //    {
        //        //CloseHandle(intGSInstanceHandle);
        //    }

        //    if (intptrArgs != IntPtr.Zero)
        //    {
        //        //CloseHandle(intptrArgs);
        //    }
        //}
        #endregion

        public bool Convert(string inputFile, string outputFile)
        {
            return Convert(inputFile, outputFile, _bThrowOnlyException, null);
        }
        public bool Convert(string inputFile, string outputFile, string parameters)
        {
            return Convert(inputFile, outputFile, _bThrowOnlyException, parameters);
        }
        private bool Convert(string inputFile, string outputFile, bool throwException, string options)
        {
            #region Check Input
            //Avoid to work when the file doesn't exist
            if (string.IsNullOrEmpty(inputFile))
                throw new ArgumentNullException("inputFile");
            if (!System.IO.File.Exists(inputFile))
                throw new ArgumentException(string.Format("The file :'{0}' doesn't exist", inputFile), "inputFile");
            if (string.IsNullOrEmpty(_sDeviceFormat))
                throw new ArgumentNullException("Device");
            //be sure that if i specify multiple page outpage i added the % to the filename!
            #endregion
            //If i create a Mutex it means i want to protect concurrent access to the library
            if (mutex != null) mutex.WaitOne();
            bool result = false;
            try
            {
                result = ExecuteGhostscriptCommand(GetGeneratedArgs(inputFile, outputFile, options));
            }
            finally { if (mutex != null) mutex.ReleaseMutex(); }
            return result;
        }
        public bool Print(string inputFile, string printParametersFile)
        {
            #region Check Input
            //Avoid to work when the file doesn't exist
            if (string.IsNullOrEmpty(inputFile))
                throw new ArgumentNullException("inputFile");
            if (!System.IO.File.Exists(inputFile))
                throw new ArgumentException(string.Format("The file :'{0}' doesn't exist", inputFile), "inputFile");
            //Avoid to work when the file doesn't exist
            if (string.IsNullOrEmpty(printParametersFile))
                throw new ArgumentNullException("printParametersFile");
            if (!System.IO.File.Exists(printParametersFile))
                throw new ArgumentException(string.Format("The file :'{0}' doesn't exist", printParametersFile), "printParametersFile");
            #endregion
            // Example : gswin32.exe" -dNOPAUSE -dBATCH -dFirstPage=1 -dLastPage=1 setup.ps mio.pdf -c quit
            List<string> args = new List<string>(7);
            args.Add("printPdf");
            args.Add("-dNOPAUSE");
            args.Add("-dBATCH");
            if (_iFirstPageToConvert > 0)
                args.Add(string.Format("-dFirstPage={0}", _iFirstPageToConvert));
            if ((_iLastPageToConvert > 0) && (_iLastPageToConvert >= _iFirstPageToConvert))
                args.Add(string.Format("-dLastPage={0}", _iLastPageToConvert));
            args.Add(printParametersFile);
            args.Add(inputFile);
            bool result = false;
            if (mutex != null) mutex.WaitOne();
            try { result = ExecuteGhostscriptCommand(args.ToArray()); }
            finally { if (mutex != null) mutex.ReleaseMutex(); }
            return result;
        }

        int intReturn;
        private bool ExecuteGhostscriptCommand(string[] sArgs)
        {
            #region Variables
            int intCounter, intElementCount;
            //The pointer to the current istance of the dll
            
            object[] aAnsiArgs;
            
            //Process proc = Process.GetCurrentProcess();

            #endregion
            //PJsoft.WebServicesBase.Log.writeInfo(string.Format("Input parameters: {0}", string.Join(";",sArgs)));
            //PJsoft.WebServicesBase.Log.writeInfo(string.Format("Start of GhostScrip command - size of workingset: {0} kB", System.Environment.WorkingSet/1024));
            #region Convert Unicode strings to null terminated ANSI byte arrays
            // Convert the Unicode strings to null terminated ANSI byte arrays
            // then get pointers to the byte arrays.
            intElementCount = sArgs.Length;
            aAnsiArgs = new object[intElementCount];
            aPtrArgs = new IntPtr[intElementCount];
            aGCHandle = new GCHandle[intElementCount];
            //Convert the parameters
            for (intCounter = 0; intCounter < intElementCount; intCounter++)
            {
                aAnsiArgs[intCounter] = StringToAnsiZ(sArgs[intCounter]);
                aGCHandle[intCounter] = GCHandle.Alloc(aAnsiArgs[intCounter], GCHandleType.Pinned);
                aPtrArgs[intCounter] = aGCHandle[intCounter].AddrOfPinnedObject();
            }
            gchandleArgs = GCHandle.Alloc(aPtrArgs, GCHandleType.Pinned);
            intptrArgs = gchandleArgs.AddrOfPinnedObject();
            //PJsoft.WebServicesBase.Log.writeInfo(string.Format("GhostScrip command - size of workingset after convert parameters: {0} kB", System.Environment.WorkingSet/1024));
            #endregion
            #region Create a new istance of the library!
            intReturn = -1;
            try
            {
                intReturn = gsapi_new_instance(out intGSInstanceHandle, _objHandle);
                //Be sure that we create an istance!
                if (intReturn < 0)
                {
                    ClearParameters(ref aGCHandle, ref gchandleArgs);
                    throw new ApplicationException("I can't create a new istance of Ghostscript please verify no other istance are running!");
                }
            }
            catch (BadImageFormatException)//99.9% of time i'm just loading a 32bit dll in a 64bit enviroment!
            {
                ClearParameters(ref aGCHandle, ref gchandleArgs);
                //Check if i'm in a 64bit enviroment or a 32bit
                if (IntPtr.Size == 8) // 8 * 8 = 64
                {
                    throw new ApplicationException(string.Format("The gsdll32.dll you provide is not compatible with the current architecture that is 64bit," +
                    "Please download any version above version 8.64 from the original website in the 64bit or x64 or AMD64 version!"));
                }
                else if (IntPtr.Size == 4) // 4 * 8 = 32
                {
                    throw new ApplicationException(string.Format("The gsdll32.dll you provide is not compatible with the current architecture that is 32bit," +
                    "Please download any version above version 8.64 from the original website in the 32bit or x86 or i386 version!"));
                }
            }
            catch (DllNotFoundException)//in this case the dll we r using isn't the dll we expect
            {
                ClearParameters(ref aGCHandle, ref gchandleArgs);
                throw new ApplicationException("The gsdll32.dll wasn't found in default dlls search path" +
                    "or is not in correct version (doesn't expose the required methods). Please download " +
                    "at least the version 8.64 from the original website");
            }
            callerHandle = IntPtr.Zero;//remove unwanter handler
            #endregion
            //PJsoft.WebServicesBase.Log.writeInfo(string.Format("GhostScrip command - size of workingset after creating new instance: {0} kB", System.Environment.WorkingSet/1024));
            #region Capture the I/O
            //Not working
            if (_bRedirectIO)
            {
                StdioCallBack stdinCallback = new StdioCallBack(gsdll_stdin);
                StdioCallBack stdoutCallback = new StdioCallBack(gsdll_stdout);
                StdioCallBack stderrCallback = new StdioCallBack(gsdll_stderr);
                intReturn = gsapi_set_stdio(intGSInstanceHandle, stdinCallback, stdoutCallback, stderrCallback);
                if (output == null) output = new StringBuilder();
                else output.Remove(0, output.Length);
                myProcess = System.Diagnostics.Process.GetCurrentProcess();
                myProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(SaveOutputToImage);
            }
            #endregion
            intReturn = -1;//if nothing change it there is an error!
            //Ok now is time to call the interesting method
            try
            {
                intReturn = gsapi_init_with_args(intGSInstanceHandle, intElementCount, intptrArgs);
                //PJsoft.WebServicesBase.Log.writeInfo(string.Format("GhostScrip command - size of workingset after converting: {0} kB", System.Environment.WorkingSet/1024));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            finally//No matter what happen i MUST close the istance!
            {   //free all the memory
                ClearParameters(ref aGCHandle, ref gchandleArgs);
                gsapi_exit(intGSInstanceHandle);//Close the istance
                gsapi_delete_instance(intGSInstanceHandle);//delete it
                //In case i was looking for output now stop
                if ((myProcess != null) && (_bRedirectIO)) myProcess.OutputDataReceived -= new System.Diagnostics.DataReceivedEventHandler(SaveOutputToImage);
            }
            //Conversion was successfull if return code was 0 or e_Quit
            //PJsoft.WebServicesBase.Log.writeInfo(string.Format("GhostScrip command - size of workingset after freeing memory: {0} kB", System.Environment.WorkingSet/1024));            
            

            return (intReturn == 0) | (intReturn == e_Quit);//e_Quit = -101
        }
        private void ClearParameters(ref GCHandle[] aGCHandle, ref GCHandle gchandleArgs)
        {
            for (int intCounter = 0; intCounter < aGCHandle.Length; intCounter++)
                aGCHandle[intCounter].Free();
            gchandleArgs.Free();
        }
        void SaveOutputToImage(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            output.Append(e.Data);
        }
        private string[] GetGeneratedArgs(string inputFile, string outputFile, string otherParameters)
        {
            if (!string.IsNullOrEmpty(otherParameters))
                return GetGeneratedArgs(inputFile, outputFile, otherParameters.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            else
                return GetGeneratedArgs(inputFile, outputFile, (string[])null);
        }
        private string[] GetGeneratedArgs(string inputFile, string outputFile, string[] presetParameters)
        {
            string[] args;
            ArrayList lstExtraArgs = new ArrayList();
            //ok if i haven't been passed a list of parameters create my own
            if ((presetParameters == null) || (presetParameters.Length == 0))
            {
                #region Parameters
                //Ok now check argument per argument and compile them
                //If i want a jpeg i can set also quality
                if (_sDeviceFormat == "jpeg" && _iJPEGQuality > 0 && _iJPEGQuality < 101)
                    lstExtraArgs.Add(string.Format(GS_JpegQualityFormat, _iJPEGQuality));
                //if i provide size it will override the paper size
                if (_iWidth > 0 && _iHeight > 0)
                    lstExtraArgs.Add(string.Format(GS_PageSizeFormat, _iWidth, _iHeight));
                else//otherwise if aviable use the papersize
                {
                    if (!string.IsNullOrEmpty(_sDefaultPageSize))
                    {
                        lstExtraArgs.Add(string.Format(GS_DefaultPaperSize, _sDefaultPageSize));
                        //It have no meaning to set it if the default page is not set!
                        if (_bForcePageSize)
                            lstExtraArgs.Add(GS_FixedMedia);
                    }
                }

                //not set antialiasing settings
                if (_iGraphicsAlphaBit > 0)
                    lstExtraArgs.Add(string.Format(GS_GraphicsAlphaBits, _iGraphicsAlphaBit));
                if (_iTextAlphaBit > 0)
                    lstExtraArgs.Add(string.Format(GS_TextAlphaBits, _iTextAlphaBit));
                //Should i try to fit?
                if (_bFitPage) lstExtraArgs.Add(GS_FitPage);
                //Do i have a forced resolution?
                if (_iResolutionX > 0)
                {
                    if (_iResolutionY > 0)
                        lstExtraArgs.Add(String.Format(GS_ResolutionXYFormat, _iResolutionX, _iResolutionY));
                    else
                        lstExtraArgs.Add(String.Format(GS_ResolutionXFormat, _iResolutionX));
                }
                if (_iFirstPageToConvert > 0)
                    lstExtraArgs.Add(String.Format(GS_FirstPageFormat, _iFirstPageToConvert));
                if (_iLastPageToConvert > 0)
                {
                    if ((_iFirstPageToConvert > 0) && (_iFirstPageToConvert > _iLastPageToConvert))
                        throw new ArgumentOutOfRangeException(string.Format("The 1st page to convert ({0}) can't be after then the last one ({1})", _iFirstPageToConvert, _iLastPageToConvert));
                    lstExtraArgs.Add(String.Format(GS_LastPageFormat, _iLastPageToConvert));
                }
                //Set in how many threads i want to do the work
                if (_iRenderingThreads > 0)
                    lstExtraArgs.Add(String.Format(GS_RenderingThreads, _iRenderingThreads));

                //If i want to redirect write it to the standard output!
                if (_bRedirectIO)
                {
                    //In this case you must also use the -q switch to prevent Ghostscript
                    //from writing messages to standard output which become
                    //mixed with the intended output stream. 
                    //outputFile = GS_StandardOutputDevice;
                    //lstExtraArgs.Add(GS_QuiteOperation);
                }
                #region Fonts
                if ((_sFontPath != null) && (_sFontPath.Count > 0))
                    lstExtraArgs.Add(String.Format(GS_FontPath, String.Join(";", _sFontPath.ToArray())));
                if (_bDisablePlatformFonts)
                    lstExtraArgs.Add(GS_NoPlatformFonts);
                if (_bDisableFontMap)
                    lstExtraArgs.Add(GS_NoFontMap);
                if ((_sFontMap != null) && (_sFontMap.Count > 0))
                    lstExtraArgs.Add(String.Format(GS_FontMap, String.Join(";", _sFontMap.ToArray())));
                if (!string.IsNullOrEmpty(_sSubstitutionFont))
                    lstExtraArgs.Add(string.Format(GS_SubstitutionFont, _sSubstitutionFont));
                if (!string.IsNullOrEmpty(_sFCOFontFile))
                    lstExtraArgs.Add(string.Format(GS_FCOFontFile, _sFCOFontFile));
                if (!string.IsNullOrEmpty(_sFAPIFontMap))
                {
                    lstExtraArgs.Add(string.Format(GS_FAPIFontMap, _sFAPIFontMap));
                }
                if (_bDisablePrecompiledFonts)
                    lstExtraArgs.Add(GS_NoPrecompiledFonts);
                #endregion
                #endregion
                int iFixedCount = 7;//This are the mandatory options
                int iExtraArgsCount = lstExtraArgs.Count;
                args = new string[iFixedCount + lstExtraArgs.Count];
                args[1] = GS_Fixed1stParameter;//"-dNOPAUSE";//I don't want interruptions
                args[2] = GS_Fixed2ndParameter;//"-dBATCH";//stop after
                args[3] = GS_Fixed3rdParameter;//"-dSAFER";
                args[4] = string.Format(GS_DeviceFormat, _sDeviceFormat);//what kind of export format i should provide
                //For a complete list watch here:
                //http://pages.cs.wisc.edu/~ghost/doc/cvs/Devices.htm
                //Fill the remaining parameters
                for (int i = 0; i < iExtraArgsCount; i++)
                    args[5 + i] = (string)lstExtraArgs[i];
            }
            else
            {//3 arguments MUST be added 0 (meaningless) and at the end the output and the inputfile
                args = new string[presetParameters.Length + 3];
                //now use the parameters i receive (thanks CrucialBT to point this out!)
                //and thanks to Barbara who pointout that i was skipping the last parameter
                for (int i = 1; i <= presetParameters.Length; i++)
                    args[i] = presetParameters[i - 1];
            }
            args[0] = GS_FirstParameter;//this parameter have little real use
            //Now check if i want to update to 1 file per page i have to be sure do add % to the output filename
            if ((_didOutputToMultipleFile) && (!outputFile.Contains(GS_MultiplePageCharacter)))
            {// Thanks to Spillie to show me the error!
                int lastDotIndex = outputFile.LastIndexOf('.');
                if (lastDotIndex > 0)
                    outputFile = outputFile.Insert(lastDotIndex, "%d");
            }
            //Ok now save them to be shown 4 debug use
            _sParametersUsed = string.Empty;
            //Copy all the args except the 1st that is useless and the last 2
            for (int i = 1; i < args.Length - 2; i++)
                _sParametersUsed += " " + args[i];
            //Fill outputfile and inputfile as last 2 arguments!
            args[args.Length - 2] = string.Format(GS_OutputFileFormat, outputFile);
            args[args.Length - 1] = string.Format("{0}", inputFile);

            _sParametersUsed += " " + string.Format(GS_OutputFileFormat, string.Format("\"{0}\"", outputFile))
            + " " + string.Format("\"{0}\"", inputFile);
            return args;
        }
        private static byte[] StringToAnsiZ(string str)
        {   //This with Encoding.Default should work also with Chineese Japaneese
            //Thanks to tchu_2000 I18N related patch
            if (str == null) str = String.Empty;
            return Encoding.Default.GetBytes(str);
        }
        public static string AnsiZtoString(IntPtr strz)
        {
            if (strz != IntPtr.Zero)
                return Marshal.PtrToStringAnsi(strz);
            else
                return string.Empty;
        }
        public static bool CheckDll()
        {
            return File.Exists(GhostScriptDLLName);
        }
        public int gsdll_stdin(IntPtr intGSInstanceHandle, IntPtr strz, int intBytes)
        {
            // This is dumb code that reads one byte at a time
            // Ghostscript doesn't mind this, it is just very slow
            if (intBytes == 0)
                return 0;
            else
            {
                int ich = Console.Read();
                if (ich == -1)
                    return 0; // EOF
                else
                {
                    byte bch = (byte)ich;
                    GCHandle gcByte = GCHandle.Alloc(bch, GCHandleType.Pinned);
                    ptrByte = gcByte.AddrOfPinnedObject();
                    CopyMemory(strz, ptrByte, 1);
                    ptrByte = IntPtr.Zero;
                    gcByte.Free();
                    return 1;
                }
            }
        }
        public int gsdll_stdout(IntPtr intGSInstanceHandle, IntPtr strz, int intBytes)
        {
            // If you can think of a more efficient method, please tell me!
            // We need to convert from a byte buffer to a string
            // First we create a byte array of the appropriate size
            byte[] aByte = new byte[intBytes];
            // Then we get the address of the byte array
            GCHandle gcByte = GCHandle.Alloc(aByte, GCHandleType.Pinned);
            ptrByte = gcByte.AddrOfPinnedObject();
            // Then we copy the buffer to the byte array
            CopyMemory(ptrByte, strz, (uint)intBytes);
            // Release the address locking
            ptrByte = IntPtr.Zero;
            gcByte.Free();
            // Then we copy the byte array to a string, character by character
            string str = "";
            for (int i = 0; i < intBytes; i++)
            {
                str += (char)aByte[i];
            }
            // Finally we output the message
            //Console.Write(str);
            output.Append(str);
            return intBytes;
            //if (intBytes > 0)
            //{
            //    Console.Write(Marshal.PtrToStringAnsi(strz));
            //}
            //return 0;
        }
        public int gsdll_stderr(IntPtr intGSInstanceHandle, IntPtr strz, int intBytes)
        {
            return gsdll_stdout(intGSInstanceHandle, strz, intBytes);
        }
        public GhostScriptRevision GetRevision()
        {
            // Check revision number of Ghostscript
            int intReturn;
            GS_Revision udtGSRevInfo = new GS_Revision();
            GhostScriptRevision output;
            GCHandle gcRevision;
            gcRevision = GCHandle.Alloc(udtGSRevInfo, GCHandleType.Pinned);
            intReturn = gsapi_revision(ref udtGSRevInfo, 16);
            output.intRevision = udtGSRevInfo.intRevision;
            output.intRevisionDate = udtGSRevInfo.intRevisionDate;
            output.ProductInformation = AnsiZtoString(udtGSRevInfo.strProduct);
            output.CopyrightInformations = AnsiZtoString(udtGSRevInfo.strCopyright);
            gcRevision.Free();
            return output;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        public void Dispose()
        {
            //mutex = null;
            //_sDeviceFormat = null;
            //_sParametersUsed = null;
            //_iWidth = 0;
            //_iHeight = 0;
            //_iResolutionX = 0;
            //_iResolutionY = 0;
            //_iJPEGQuality = 0;
            //_iFirstPageToConvert = -1;
            //_iLastPageToConvert = -1;
            //_iGraphicsAlphaBit = -1;
            //_iTextAlphaBit = -1;
            //_iRenderingThreads = -1;
            //_sDefaultPageSize = string.Empty;
            //_objHandle = IntPtr.Zero;
            //_sSubstitutionFont = string.Empty;
            //_sFCOFontFile = string.Empty;
            //_sFAPIFontMap = string.Empty;

            //if (output != null) { output.Clear(); }
            //if (_sFontPath != null) { _sFontPath.Clear(); }
            //if (_sFontMap != null) { _sFontMap.Clear(); }
            //if (myProcess != null) { myProcess = null; }
        }
    }

    


    public delegate int StdioCallBack(IntPtr handle, IntPtr strptr, int count);
    [StructLayout(LayoutKind.Sequential)]
    struct GS_Revision
    {
        public IntPtr strProduct;
        public IntPtr strCopyright;
        public int intRevision;
        public int intRevisionDate;
    }
    public struct GhostScriptRevision
    {
        public string ProductInformation;
        public string CopyrightInformations;
        public int intRevision;
        public int intRevisionDate;
    }
}
