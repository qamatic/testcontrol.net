using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestControl.Net.Extras;
using TestControl.Net.Interfaces;

namespace TestControl.Net
{
    public static class LoggerMgr
    {
        private static Type _loggerType = typeof(NoLogger);
        public static ILogger GetNewLoggerInstance(Type callingObjectType=null)
        {
            return (ILogger)Activator.CreateInstance(_loggerType);
        }

        public static void SetLoggerType<T>() where T : ILogger
        {
            _loggerType = typeof(T);
        }
    }
}
