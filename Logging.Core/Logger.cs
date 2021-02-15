using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nina.Logging
{
    public static class Logger
    {
        private static readonly ILogger _rvtLogger;
        static Logger()
        {
            _rvtLogger = new LoggerConfiguration()
                        .WriteTo.File(path: @"C:\ProgramData\Autodesk\ApplicationPlugins\Nina.bundle\Logs\logs.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
        }

        public static void Write(Log log)
        {
            if (log.Exception == null) _rvtLogger.Write(Serilog.Events.LogEventLevel.Information, "{@Log}", log);
            else _rvtLogger.Write(Serilog.Events.LogEventLevel.Error, "{@Log}", log);
        }
    }
}
