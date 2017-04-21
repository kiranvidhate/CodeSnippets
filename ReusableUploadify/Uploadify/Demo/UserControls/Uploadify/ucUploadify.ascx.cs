using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Uploadify_WebUserControl : System.Web.UI.UserControl
{
    #region -- Class Properties --

    private string _UploadFolder = string.Empty;
    /// <summary>
    /// Gets or sets the upload folder.
    /// </summary>
    /// <value>
    /// The upload folder.
    /// </value>
    public string UploadFolder
    {
        get { return _UploadFolder; }
        set { _UploadFolder = value; }
    }

    private string _FileExtension = string.Empty;
    /// <summary>
    /// Gets or sets the allowed file extensions.
    /// </summary>
    /// <value>
    /// The allowed file extensions.
    /// </value>
    public string AllowedFileExtensions
    {
        get { return _FileExtension; }
        set { _FileExtension = value; }
    }

    private bool _MultiUpload = false;
    /// <summary>
    /// Gets or sets a value indicating whether [multi upload].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [multi upload]; otherwise, <c>false</c>.
    /// </value>
    public bool MultiUpload
    {
        get { return _MultiUpload; }
        set { _MultiUpload = value; }
    }

    private string _MaxFileSize = string.Empty;
    /// <summary>
    /// Gets or sets the size of the max file.
    /// </summary>
    /// <value>
    /// The size of the max file.
    /// </value>
    public string MaxFileSize
    {
        get { return _MaxFileSize; }
        set { _MaxFileSize = value; }
    }


    private string _MaxQueueSize = string.Empty;
    /// <summary>
    /// Gets or sets the size of the max queue.
    /// </summary>
    /// <value>
    /// The size of the max queue.
    /// </value>
    public string MaxQueueSize
    {
        get { return _MaxQueueSize; }
        set { _MaxQueueSize = value; }
    }
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}