using System;
using System.IO;
using System.Web;
using log4net; 

public partial class _Default : System.Web.UI.Page
{
    #region -- Class Variables --
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(_Default));//(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region -- Page Events --
    /// <summary>
    /// Handles the Click event of the btnLog control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void btnLog_Click(object sender, EventArgs e)
    {
        
        log4net.GlobalContext.Properties["trailactivity"] = "This is my test property information";
        log4net.Config.XmlConfigurator.Configure();
        log.Debug("log Debug");
        log.Info("log Info");
        log.Warn("log Warn");
        log.Error("log Error");
        log.Fatal("log Fatal"); 
    }

    /// <summary>
    /// Handles the Click event of the btnLogAuditTrail control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    //private static readonly log4net.ILog log =log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void btnLogAuditTrail_Click(object sender, EventArgs e)
    {
        //MyCustomLogger objMyCustomLogger = new MyCustomLogger();
        //objMyCustomLogger.LogMessage();
        log4net.GlobalContext.Properties["trailactivity"] = "This is my test property information";
        log4net.Config.XmlConfigurator.Configure();
        log.Debug("log Debug");
        log.Info("log Info");
        log.Warn("log Warn");
        log.Error("log Error");
        log.Fatal("log Fatal"); 
    }
    #endregion
}