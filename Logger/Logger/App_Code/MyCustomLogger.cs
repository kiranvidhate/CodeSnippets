using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net; 
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
/// <summary>
/// Summary description for MyCustomLogger
/// </summary>
public class MyCustomLogger
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
   // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MyCustomLogger));

	public MyCustomLogger()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void LogMessage()
    {
        log4net.Config.XmlConfigurator.Configure();
        log.Debug("log Debug");
        log.Warn("log Warn");
        log.Error("log Error");
        log.Fatal("log Fatal");
        log.Info("log Info");
    }
}