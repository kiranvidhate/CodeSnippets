using System;
using System.IO;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    #region -- Class Variables --
    PDFLibrary.PDFManager merge;
    #endregion

    #region -- Page Events --
    /// <summary>
    /// Handles the Click event of the btnMerge control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void btnMerge_Click(object sender, EventArgs e)
    {
        MergerFiles();
    }

    /// <summary>
    /// Handles the Click event of the MultipleFileUpload1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="FileCollectionEventArgs"/> instance containing the event data.</param>
    protected void MultipleFileUpload1_Click(object sender, FileCollectionEventArgs e)
    {
        HttpFileCollection oHttpFileCollection = e.PostedFiles;
        HttpPostedFile oHttpPostedFile = null;
        if (e.HasFiles)
        {
            for (int n = 0; n < e.Count; n++)
            {
                oHttpPostedFile = oHttpFileCollection[n];
                if (oHttpPostedFile.ContentLength <= 0)
                    continue;
                else
                    oHttpPostedFile.SaveAs(Server.MapPath("Files") + "\\" + System.IO.Path.GetFileName(oHttpPostedFile.FileName));
            }

            lblMessage.Text = "Files uploaded successfully.";
        }
    }
    #endregion

    #region -- Private Methods --

    /// <summary>
    /// Mergers the files.
    /// </summary>
    private void MergerFiles()
    {
        try
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("Files"), "*.pdf");

            if (merge == null)
                merge = new PDFLibrary.PDFManager();


            if (filePaths.Length > 0)
            {
                for (int f = 0; f < filePaths.Length; f++)
                {
                    string PDFFile = filePaths[f];//lstFiles.Items[f].SubItems[0].Text;
                    merge.AddFile(PDFFile);
                }
               
                string savedDestinationPath = Server.MapPath("MergedFiles"); // _setting.Read("Merege PDF Location", "PDFLocation");
                string MergedFilesPath = "";
                string MergedFileName = string.Format("{0:dd-MMM-yy HH mm}.pdf", DateTime.Now);
                if (savedDestinationPath != null && savedDestinationPath != "")
                {
                    if (Directory.Exists(savedDestinationPath))
                        MergedFilesPath = savedDestinationPath;
                    else
                    {
                        MergedFilesPath = Server.MapPath("MergedFiles");
                        if (!Directory.Exists(MergedFilesPath))
                        {
                            Directory.CreateDirectory(MergedFilesPath);
                            //_setting.Write("Merege PDF Location", "PDFLocation", MergedFilesPath);
                        }
                    }
                }
                merge.DestinationFile = Path.Combine(MergedFilesPath, MergedFileName);
                merge.Execute(lblMessage);
                lblMessage.Text = "PDFs merged successfully.";
            }
            else
            {
                lblMessage.Text = "Please select files before proceed!";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    #endregion
}