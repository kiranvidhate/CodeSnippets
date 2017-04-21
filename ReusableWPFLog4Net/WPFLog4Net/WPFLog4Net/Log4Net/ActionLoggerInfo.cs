using Newtonsoft.Json;
using System;

namespace WPFLog4Net
{
    class ActionLoggerInfo
    {
        public string Module { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string IPAddress { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
