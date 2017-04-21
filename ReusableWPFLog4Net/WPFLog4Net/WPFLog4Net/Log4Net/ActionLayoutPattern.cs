using log4net.Layout;
using log4net.Util;

namespace WPFLog4Net
{
    public class ActionLayoutPattern : PatternLayout
    {
        public ActionLayoutPattern()
        {
            AddConverter(new ConverterInfo
            {
                Name = "actionInfo",
                Type = typeof(ActionConverter)
            }
            );
        }
    }
}
