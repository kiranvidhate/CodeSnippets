using System;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace PDFLibrary
{
    public class PDFManager
    {
        #region Fields
        private string destinationfile;
        private IList fileList = new ArrayList();
        SettingManager setting;
        #endregion

        #region "Properties"
        //Gets or Sets the DestinationFile
        public string DestinationFile
        {
            get { return destinationfile; }
            set { destinationfile = value; }
        }

        //Processing or current File Index,File Name and Total Pages and Process Page Index.
        private int processfileindex = 0;
        public int ProcessFileIndex
        {
            get { return processfileindex; }
            set { processfileindex = value; }
        }

        private string processfilename = "";
        public string ProcessFileName
        {
            get { return processfilename; }
            set { processfilename = value; }
        }

        private int processfilepages = 0;
        public int ProcessFilePages
        {
            get { return processfilepages; }
            set { processfilepages = value; }
        }

        private int processfilepageindex = 0;
        public int ProcessFilePageIndex
        {
            get { return processfilepageindex; }
            set { processfilepageindex = value; }
        }

        //Total Files and Total Pages to be merge in a single.
        private int totalfiles = 0;
        public int TotalFiles
        {
            get { return totalfiles; }
            set { totalfiles = value; }
        }

        private int totalpages = 0;
        public int TotalPages
        {
            get { return totalpages; }
            set { totalpages = value; }
        }

        private int pageindex = 0;
        public int PageIndex
        {
            get { return pageindex; }
            set { pageindex = value; }
        }
        #endregion

        #region "Constructors"
        //this class requires that a "synchronizing object" be passed to it - this tells the class what context notify delegates should run in...
        public PDFManager(IList _fileList)
        {

            this.fileList = _fileList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFManager"/> class.
        /// </summary>
        public PDFManager()
        {
            // Code goes here...
        }
        #endregion

        #region Public Methods
        ///
        /// Add a new file, together with a given docname to the fileList and namelist collection
        ///
        public void AddFile(string pathnname)
        {
            fileList.Add(pathnname);
        }

        ///
        /// Generate the merged PDF
        ///
        public void Execute(Label lblMessages)
        {
            MergeDocs(lblMessages);
        }
        #endregion

        #region Private Methods
        ///
        /// Merges the Docs and renders the destinationFile
        ///
        private void MergeDocs(Label lblMessages)
        {
            string value = "";
            setting = new SettingManager(HttpContext.Current.Server.MapPath("settings.ini"));
            //destinationfile = setting.Read("Merege PDF Location", "PDFLocation");
            //------------------------------------------------------------------------------------
            //Step 1: Create a Docuement-Object
            //------------------------------------------------------------------------------------
            Document document = new Document();
            try
            {
                //------------------------------------------------------------------------------------
                //Step 2: we create a writer that listens to the document
                //------------------------------------------------------------------------------------
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationfile, FileMode.Create));

                //------------------------------------------------------------------------------------
                //Step 3: Set Password Protection if you are set for it
                //------------------------------------------------------------------------------------
                SetPasswordProtection(writer, lblMessages);

                //------------------------------------------------------------------------------------
                //Step 4: Open the document
                //------------------------------------------------------------------------------------
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                PdfReader reader;
                int rotation = 0;

                ProcessFileIndex = 0;
                TotalFiles = fileList.Count;
                TotalPages = 0;
                PageIndex = 0;
                foreach (string filename in fileList)
                {
                    reader = new PdfReader(filename);
                    //Gets the number of pages to process
                    TotalPages += reader.NumberOfPages;
                }

                //Loops for each file that has been listed
                foreach (string filename in fileList)
                {
                    //Create a reader for the document
                    reader = new PdfReader(filename);
                    ProcessFileIndex++;
                    ProcessFileName = filename;
                    //Gets the number of pages to process
                    ProcessFilePages = reader.NumberOfPages;
                    ProcessFilePageIndex = 0;

                    while (ProcessFilePageIndex < ProcessFilePages)
                    {
                        ProcessFilePageIndex++;
                        PageIndex++;

                        document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        document.NewPage();
                        //Insert to Destination on the first page
                        if (ProcessFilePageIndex == 1)
                        {
                            Chunk fileRef = new Chunk(" ");
                            fileRef.SetLocalDestination(filename);
                            document.Add(fileRef);
                        }

                        page = writer.GetImportedPage(reader, ProcessFilePageIndex);
                        rotation = reader.GetPageRotation(ProcessFilePageIndex);
                        if (rotation == 90 || rotation == 270)
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(ProcessFilePageIndex).Height);
                        else
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);

                        //------------------------------------------------------------------------------------
                        //Step 4: Set Page Number and Formatting if you are set for it
                        //------------------------------------------------------------------------------------
                       // value = "false"; 
                        setting.Read("Page Number and Formatting", "AollowPageFormatting");
                        if (!string.IsNullOrEmpty(value) && Convert.ToBoolean(value) == true)
                        {
                            cb.BeginText();

                            // we draw some text on a perticular position
                            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                            cb.SetFontAndSize(bf, 12);

                            value = "page"; // setting.Read("Page Number and Formatting", "PageText");
                            string PageNumberText = "";
                            if (!string.IsNullOrEmpty(value)) PageNumberText = value;

                            //string isPageNumber = "true";//
                            string isPageNumber = setting.Read("Page Number and Formatting", "PageNumber");
                           // string isTotalPageNumber = "true";//
                            string isTotalPageNumber = setting.Read("Page Number and Formatting", "TotalPage");
                            if (!string.IsNullOrEmpty(isPageNumber) && Convert.ToBoolean(isPageNumber) == true && !string.IsNullOrEmpty(isTotalPageNumber) && Convert.ToBoolean(isTotalPageNumber) == true)
                            {
                                PageNumberText = string.Format("{0} {1} of {2}", PageNumberText, PageIndex, TotalPages);
                            }
                            else if (!string.IsNullOrEmpty(isPageNumber) && Convert.ToBoolean(isPageNumber) == true)
                            {
                                PageNumberText = string.Format("{0} {1}", PageNumberText, PageIndex);
                            }
                            else if (!string.IsNullOrEmpty(isTotalPageNumber) && Convert.ToBoolean(isTotalPageNumber) == true)
                            {
                                PageNumberText = string.Format("{0} {1}", PageNumberText, TotalPages);
                            }

                            int pageN = writer.PageNumber;
                            String text = PageNumberText;
                            float len = bf.GetWidthPoint(text, 8);

                            iTextSharp.text.Rectangle pageSize = document.PageSize;

                            cb.SetRGBColorFill(100, 100, 100);

                          //  cb.BeginText();
                            cb.SetFontAndSize(bf, 8);
                            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
                            cb.ShowText(text);
                            cb.EndText();

                            PdfTemplate template;
                            template = cb.CreateTemplate(50, 50);
                            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

                            cb.BeginText();
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                                "Printed On " + DateTime.Now.ToString(),
                                pageSize.GetRight(40),
                                pageSize.GetBottom(30), 0);
                            cb.EndText();

                        }
                    }
                }
            }
            catch (Exception e)
            {
               // HttpContext.Current.Response.Write("---------------------------<br/>MergeDocs: " + e.Message);
                lblMessages.Text = "MergeDocs: " + e.Message;
            }
            finally
            {
                //------------------------------------------------------------------------------------
                //Step 5: Close the merged pdf document
                //------------------------------------------------------------------------------------
                document.Close();


                //------------------------------------------------------------------------------------
                //Step 6: Applying watermark Image of Text on PDF 
                //------------------------------------------------------------------------------------
                // bool isWatermarkEnabled = false; 
                bool isWatermarkEnabled = Convert.ToBoolean(setting.Read("Watermark Image or Write Text", "IsWatermarkEnabled"));
                if (isWatermarkEnabled)
                {
                    //bool isImagePath = true;  
                    bool isImagePath = Convert.ToBoolean(setting.Read("Watermark Image or Write Text", "IsImagePath"));
                    string filePath = HttpContext.Current.Server.MapPath("MergedFiles"); //setting.Read("Merege PDF Location", "PDFLocation");
                    if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath))
                    {
                        string newFilePath = Path.Combine(HttpContext.Current.Server.MapPath("MergedFiles"));
                        // setting.Write("Merege PDF Location", "PDFLocation", newFilePath);

                        //HttpContext.Current.Response.Write("---------------------------<br/>isWatermarkEnabled: " + string.Format("Directory does not exists at {0}\n Default path is {1}", filePath, newFilePath));
                        lblMessages.Text = "isWatermarkEnabled: " + string.Format("Directory does not exists at {0}\n Default path is {1}", filePath, newFilePath);
                        filePath = newFilePath;
                    }

                    filePath = Path.Combine(filePath, DateTime.Now.Ticks + ".pdf");
                    if (isImagePath)
                    {
                        //string imagePath = ""; // 
                        string imagePath =  Path.Combine(HttpContext.Current.Server.MapPath("images"), setting.Read("Watermark Image or Write Text", "ImagePath"));
                        //string imagePath = Path.Combine(HttpContext.Current.Server.MapPath("images"), "wm.png"); 
                        if (imagePath != null && imagePath != "")
                        {
                            if (File.Exists(imagePath))
                            {
                                AddWatermarkImage(DestinationFile, filePath, imagePath, lblMessages);
                                File.Delete(DestinationFile);
                                File.Copy(filePath, DestinationFile);
                                File.Delete(filePath);
                            }
                            else
                            {
                                //HttpContext.Current.Response.Write("---------------------------<br/>isImagePath: " + string.Format("File could not be found at {0}", imagePath));
                                lblMessages.Text = "isImagePath: " + string.Format("File could not be found at {0}", imagePath);
                            }
                        }
                    }
                    else
                    {
                        //string watermarkText = "Kiran's"; // setting.Read("Watermark Image or Write Text", "WatermarkText");
                        //string watermarkFont = "helvetica"; // setting.Read("Watermark Image or Write Text", "WatermarkFont");
                        //string watermarkColor = "red"; //setting.Read("Watermark Image or Write Text", "WatermarkColor");
                        string watermarkText =  setting.Read("Watermark Image or Write Text", "WatermarkText");
                        string watermarkFont =  setting.Read("Watermark Image or Write Text", "WatermarkFont");
                        string watermarkColor =setting.Read("Watermark Image or Write Text", "WatermarkColor");

                        iTextSharp.text.pdf.BaseFont font = ConvertStringToFont(watermarkFont, lblMessages);
                        BaseColor color = ConvertStringToColor(watermarkColor);

                        AddWatermarkText(sourceFile: DestinationFile, outputFile: filePath, watermarkText: watermarkText, watermarkFont: font, watermarkFontColor: color);
                        File.Delete(DestinationFile);
                        File.Copy(filePath, DestinationFile);
                        File.Delete(filePath);
                    }
                }

                fileList.Clear();
            }
        }

        /// <summary>
        /// Sets the password protection.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="lblMessages">The LBL messages.</param>
        /// <returns></returns>
        private PdfWriter SetPasswordProtection(PdfWriter writer, Label lblMessages)
        {
            try
            {
               // string value = "false"; 
                string value = setting.Read("File Protection", "AllowFileProtection");
                if (!string.IsNullOrEmpty(value) && Convert.ToBoolean(value) == true)
                {
                    //string password = "test"; // 
                    string password = setting.Read("File Protection", "Password");
                    string ownerPassword = "imdadhusen";

                    if (!string.IsNullOrEmpty(password))
                    {
                        int Permission = 0;
                        bool Strength = false;
                        //value = "false"; // 
                        value =  setting.Read("File Protection", "AllowCopy");
                        if (!string.IsNullOrEmpty(value)) Permission = 16;
                        //value = "false"; //
                        value = setting.Read("File Protection", "AllowPrinting");
                        if (!string.IsNullOrEmpty(value)) Permission += 2052;

                        //value = "strength128bits"; // 
                        value = setting.Read("File Protection", "EncryptionStrength");
                        if (!string.IsNullOrEmpty(value) && value == "strength128bits") Strength = true;

                        writer.SetEncryption(Strength, password, ownerPassword, Permission);
                    }
                }

            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write("---------------------------<br/>SetPasswordProtection: " + ex.Message);
                lblMessages.Text = "SetPasswordProtection: " + ex.Message;
            }
            return writer;
        }

        /// <summary>
        /// Converts the string to font.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="lblMessages">The LBL messages.</param>
        /// <returns></returns>
        private BaseFont ConvertStringToFont(string fontName, Label lblMessages)
        {
            iTextSharp.text.pdf.BaseFont font = null;
            try
            {

                if (string.IsNullOrEmpty(fontName) == false)
                {
                    switch (fontName.ToUpper())
                    {
                        case "COURIER":
                        case "HELVETICA":
                            font = BaseFont.CreateFont(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fontName), BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            break;
                        case "TIMES":
                            font = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write("---------------------------<br/>ConvertStringToFont: " + ex.Message);
                  lblMessages.Text = "ConvertStringToFont: " + ex.Message;
            }
            return font;
        }

        /// <summary>
        /// Converts the color of the string to.
        /// </summary>
        /// <param name="colorName">Name of the color.</param>
        /// <returns></returns>
        private BaseColor ConvertStringToColor(string colorName)
        {
            BaseColor color = null;
            if (string.IsNullOrEmpty(colorName.ToLower()) == false)
            {
                Color c = Color.FromName(colorName.ToLower());
                color = new BaseColor(c.R, c.G, c.B);
            }
            return color;
        }

        /// <summary>
        /// Adds the watermark image.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="outputFile">The output file.</param>
        /// <param name="watermarkImage">The watermark image.</param>
        /// <param name="lblMessages">The LBL messages.</param>
        private void AddWatermarkImage(string sourceFile, string outputFile, string watermarkImage, Label  lblMessages)
        {
            try
            {
                using (Stream inputPdfStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream inputImageStream = new FileStream(watermarkImage, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputPdfStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader reader;
                    string userPassword = "imdadhusen"; //Imdadhusen is a master Password but you can unlock with User Password also.
                    string ownerPassword = "imdadhusen";
                    try
                    {
                        reader = new PdfReader(sourceFile);
                    }
                    catch (BadPasswordException)
                    {

                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        Byte[] myByteArray = enc.GetBytes(userPassword);
                        reader = new PdfReader(sourceFile, myByteArray);
                    }
                    var stamper = new PdfStamper(reader, outputPdfStream);

                    //string value = "false"; //
                    string value = setting.Read("File Protection", "AllowFileProtection");
                    if (!string.IsNullOrEmpty(value) && Convert.ToBoolean(value) == true)
                    {
                        //userPassword = "test"; // 
                        userPassword =setting.Read("File Protection", "Password");
                        if (!string.IsNullOrEmpty(userPassword))
                        {
                            int Permission = 0;
                            bool Strength = false;
                            //value = "false"; //
                            value = setting.Read("File Protection", "AllowCopy");
                            if (!string.IsNullOrEmpty(value)) Permission = 16;
                            //value = "false"; //
                            value = setting.Read("File Protection", "AllowPrinting");
                            if (!string.IsNullOrEmpty(value)) Permission += 2052;

                            //value = "strength128bits"; //
                            value = setting.Read("File Protection", "EncryptionStrength");
                            if (!string.IsNullOrEmpty(value) && value == "strength128bits") Strength = true;

                            stamper.SetEncryption(Strength, userPassword, ownerPassword, Permission);
                        }
                    }

                    iTextSharp.text.Rectangle rect = null;
                    float X = 0;
                    float Y = 0;
                    int pageCount = 0;
                    PdfContentByte underContent = null;

                    rect = reader.GetPageSizeWithRotation(1);
                    pageCount = reader.NumberOfPages;
                    reader.Close();

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(watermarkImage);
                    if (img.Width > rect.Width || img.Height > rect.Height)
                    {
                        img.ScaleToFit(rect.Width, rect.Height);
                        X = (rect.Width - img.ScaledWidth) / 2;
                        Y = (rect.Height - img.ScaledHeight) / 2;
                    }
                    else
                    {
                        X = (rect.Width - img.Width) / 2;
                        Y = (rect.Height - img.Height) / 2;
                    }
                    img.SetAbsolutePosition(X, Y);

                    for (int i = 1; i <= pageCount; i++)
                    {
                        underContent = stamper.GetUnderContent(i);
                        underContent.AddImage(img);
                    }

                    stamper.Close();
                    inputPdfStream.Close();
                }
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write("---------------------------<br/>AddWatermarkImage: " + ex.Message);
                lblMessages.Text = "AddWatermarkImage: " + ex.Message;
            }
        }

        /// <summary>
        /// Adds the watermark text.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="outputFile">The output file.</param>
        /// <param name="watermarkText">The watermark text.</param>
        /// <param name="watermarkFont">The watermark font.</param>
        /// <param name="watermarkFontSize">Size of the watermark font.</param>
        /// <param name="watermarkFontColor">Color of the watermark font.</param>
        /// <param name="watermarkFontOpacity">The watermark font opacity.</param>
        /// <param name="watermarkRotation">The watermark rotation.</param>
        private void AddWatermarkText(string sourceFile, string outputFile, string watermarkText, iTextSharp.text.pdf.BaseFont watermarkFont = null, float watermarkFontSize = 48, BaseColor watermarkFontColor = null, float watermarkFontOpacity = 0.3f, float watermarkRotation = 45f)
        {
            using (Stream inputPdfStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfReader reader;
                string userPassword = "imdadhusen"; //Imdadhusen is a master Password but you can unlock with User Password also.
                string ownerPassword = "imdadhusen";
                try
                {
                    reader = new PdfReader(sourceFile);
                }
                catch (BadPasswordException)
                {
                    System.Text.Encoding enc = System.Text.Encoding.ASCII;
                    Byte[] myByteArray = enc.GetBytes(userPassword);
                    reader = new PdfReader(sourceFile, myByteArray);
                }
                var stamper = new PdfStamper(reader, outputPdfStream);

               // string value = "false"; // 
                string value = setting.Read("File Protection", "AllowFileProtection");
                if (!string.IsNullOrEmpty(value) && Convert.ToBoolean(value) == true)
                {
                    //userPassword = "test123"; // 
                    userPassword = setting.Read("File Protection", "Password");
                    if (!string.IsNullOrEmpty(userPassword))
                    {
                        int Permission = 0;
                        bool Strength = false;
                        //value = "true";  //
                        value = setting.Read("File Protection", "AllowCopy");
                        if (!string.IsNullOrEmpty(value)) Permission = 16;
                        //value = "true";// 
                        value = setting.Read("File Protection", "AllowPrinting");
                        if (!string.IsNullOrEmpty(value)) Permission += 2052;

                        //value = "strength128bits"; //
                        value = setting.Read("File Protection", "EncryptionStrength");
                        if (!string.IsNullOrEmpty(value) && value == "strength128bits") Strength = true;

                        stamper.SetEncryption(Strength, userPassword, ownerPassword, Permission);
                    }
                }
                iTextSharp.text.Rectangle rect = null;
                PdfGState gstate = null;
                int pageCount = 0;
                PdfContentByte underContent = null;
                rect = reader.GetPageSizeWithRotation(1);
                if (watermarkFont == null) watermarkFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                if (watermarkFontColor == null) watermarkFontColor = BaseColor.BLUE;

                gstate = new PdfGState();
                gstate.FillOpacity = watermarkFontOpacity;
                gstate.StrokeOpacity = watermarkFontOpacity;
                pageCount = reader.NumberOfPages;
                for (int i = 1; i <= pageCount; i++)
                {
                    underContent = stamper.GetUnderContent(i);
                    var _with1 = underContent;
                    _with1.SaveState();
                    _with1.SetGState(gstate);
                    _with1.SetColorFill(watermarkFontColor);
                    _with1.BeginText();
                    _with1.SetFontAndSize(watermarkFont, watermarkFontSize);
                    _with1.SetTextMatrix(30, 30);
                    _with1.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, rect.Width / 2, rect.Height / 2, watermarkRotation);
                    _with1.EndText();
                    _with1.RestoreState();
                }
                stamper.Close();
                inputPdfStream.Close();
            }
        }
        #endregion
    }

    /// <summary>
    /// Class Coordinates
    /// </summary>
    public class Coordinates
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    /// <summary>
    /// Class PageNumberSize
    /// </summary>
    public class PageNumberSize
    {
        public float Height { get; set; }
        public float Width { get; set; }
    }
}