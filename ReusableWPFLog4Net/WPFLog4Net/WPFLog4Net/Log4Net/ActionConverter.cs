using log4net.Core;
using log4net.Util;

namespace WPFLog4Net
{
    public class ActionConverter : PatternConverter
    {
        protected override void Convert(System.IO.TextWriter writer, object state)
        {
            if (state == null)
            {
                writer.Write(SystemInfo.NullText);
                return;
            }

            var loggingEvent = state as LoggingEvent;
            var actionInfo = loggingEvent.MessageObject as ActionLoggerInfo;

            if (actionInfo == null)
            {
                writer.Write(SystemInfo.NullText);
            }
            else
            {
                switch (this.Option.ToLower())
                {
                    case "module":
                        writer.Write(actionInfo.Module);
                        break;
                    case "datetime":
                        writer.Write(actionInfo.DateTime);
                        break;
                    case "status":
                        writer.Write(actionInfo.Status);
                        break;
                    case "message":
                        writer.Write(actionInfo.Message);
                        break;
                    case "userid":
                        writer.Write(actionInfo.UserId);
                        break;
                    case "ipaddress":
                        writer.Write(actionInfo.IPAddress);
                        break;
                    default:
                        writer.Write(SystemInfo.NullText);
                        break;
                }
            }
        }
    }
}
