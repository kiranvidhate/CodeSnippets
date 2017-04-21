using System;
using System.IO;
using System.Web;
using Ionic.Zip;

public partial class _Default : System.Web.UI.Page
{
    #region "-- Class Variables--"
    string uploadPath = "~/Uploads/";
    string zipPath = "~/Zips/";
    string extractedZipPath = "~/Zips/Extracted/";
    #endregion

    #region "-- Class Methods--"
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (hdnDoUpload.Value != "0")
            {
                UploadFile(sender, e);
            }
        }
    }

    /// <summary>
    /// Uploads the file.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void UploadFile(object sender, EventArgs e)
    {
        string[] filePaths = Directory.GetFiles(Server.MapPath(uploadPath));

        foreach (string filePath in filePaths)
            File.Delete(filePath);

        HttpFileCollection fileCollection = Request.Files;
        //Request.Form.

        for (int i = 0; i < fileCollection.Count; i++)
        {
            HttpPostedFile upload = fileCollection[i];
            string filename = Server.MapPath(uploadPath + upload.FileName);//Path.GetRandomFileName());
            upload.SaveAs(filename);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnGenerateZip control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void btnGenerateZip_Click(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AddDirectory(Server.MapPath(uploadPath));//, "ProjectX");
            zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
            zip.Save(Server.MapPath(zipPath) + "MyFiles.zip");
        }

        hdnDoUpload.Value = "";
        SucessMessage.InnerHtml = "Zip file created successfully under /Zips/ folder.";
    }

    /// <summary>
    /// Handles the Click event of the btnExtractZip control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void btnExtractZip_Click(object sender, EventArgs e)
    {
        string[] filePaths = Directory.GetFiles(Server.MapPath(extractedZipPath));

        foreach (string filePath in filePaths)
            File.Delete(filePath);

        using (ZipFile zip = ZipFile.Read(Server.MapPath(zipPath + "MyFiles.zip")))
        {
            foreach (ZipEntry ze in zip)
            {
                ze.Extract(Server.MapPath(extractedZipPath), ExtractExistingFileAction.OverwriteSilently);
            }
        }

        hdnDoUpload.Value = "";
        SucessMessage.InnerHtml = "Zip file extracted successfully under /Zips/Extracted/ folder.";
    }
    #endregion
}