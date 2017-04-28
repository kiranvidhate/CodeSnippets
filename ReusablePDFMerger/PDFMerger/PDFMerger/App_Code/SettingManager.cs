using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// Summary description for SettingManager
/// </summary>
public class SettingManager
{
    #region -- Class Variables --
    private string filePath;
    #endregion

    #region -- Private Methods --
    /// <summary>
    /// Writes the private profile string.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="key">The key.</param>
    /// <param name="val">The val.</param>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    /// 
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    /// <summary>
    /// Gets the private profile string.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="key">The key.</param>
    /// <param name="def">The def.</param>
    /// <param name="retVal">The ret val.</param>
    /// <param name="size">The size.</param>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    #endregion

    #region -- Public Methods --
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingManager"/> class.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    public SettingManager(string filePath)
    {
        this.filePath = filePath;
    }

    /// <summary>
    /// Writes the specified section.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void Write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value.ToLower(), this.filePath);
    }

    /// <summary>
    /// Reads the specified section.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public string Read(string section, string key)
    {
        StringBuilder SB = new StringBuilder(255);
        int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
        return SB.ToString();
    }

    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    /// <value>
    /// The file path.
    /// </value>
    public string FilePath
    {
        get { return this.filePath; }
        set { this.filePath = value; }
    }
    #endregion
}