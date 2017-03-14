using System;
using System.Text;
using TestControl.Net.Interfaces;

namespace TestControl.Net.Extras
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Simple text log. </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class NoLogger : ILogger
    {

        public virtual bool IsDebugEnabled
        {
            get { return false; }
        }

        public virtual bool IsInfoEnabled
        {
            get { return false; }
        }

        public virtual bool IsWarnEnabled
        {
            get { return false; }
        }

        public virtual bool IsErrorEnabled
        {
            get { return false; }
        }

        public virtual bool IsFatalEnabled
        {
            get { return false; }
        }

        public virtual void Debug(object message, Exception exception = null)
        {
             
        }

        public virtual void Info(object message, Exception exception = null)
        {
            
        }

        public virtual void Warn(object message, Exception exception = null)
        {
            
        }

        public virtual void Error(object message, Exception exception = null)
        {
            
        }

        public virtual void Fatal(object message, Exception exception = null)
        {
            
        }
    }
}