using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nina.Logging
{
    public class Log
    {
        public Log()
        {
            Timestamp = DateTime.Now;
            HostName = Environment.MachineName;
        }
        public DateTime Timestamp { get; private set; }
        public string Revit { get; set; }
        public string Document { get; set; }
        public string UserName { get; set; }
        public string Tool { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public Dictionary<string, object> AdditionalInfo { get; set; }
        public string Nina { get; set; }
        public string Location { get; set; }
        public string HostName { get; set; }





    }
}
