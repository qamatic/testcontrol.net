// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;

namespace TestControl.Net
{
    public class ControlLocatorDef<T> : IControlLocatorDef where T : IFindControl
    {
        private int _retryTimeoutCount=20;
        private int _waitRetryMilliSec=20;
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        private IntPtr _handle = IntPtr.Zero;

        public ControlLocatorDef(params Func<T>[] findControlDelegates)
        {
            FindControlDelegates = findControlDelegates;
        }

        public ControlLocatorDef(int retryTimeoutCount, int waitRetryMilliSec,
                          params Func<T>[] findControlDelegates)
        {
            _retryTimeoutCount = retryTimeoutCount;
            _waitRetryMilliSec = waitRetryMilliSec;
            FindControlDelegates = findControlDelegates;
        }

        public void SetRetryTime(int retryCount, int waitMilliSecPerRetry)
        {
            _retryTimeoutCount = retryCount;
            _waitRetryMilliSec = waitMilliSecPerRetry;
        }


        public IEnumerable<Func<T>> FindControlDelegates { get; set; }

        #region IControlLocatorDef Members

        public Object[] FindControls
        {
            get
            {
                var list = new List<Object>();
                foreach (var valueDelegate in FindControlDelegates)
                {
                    list.Add(valueDelegate);
                }
                return list.ToArray();
            }
        }

        public IntPtr Play()
        {
            try
            {
                return Handle;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                return IntPtr.Zero;
            }
        }
        

        public IntPtr Handle
        {
            get
            {
                if (_handle != IntPtr.Zero)
                {
                    return _handle;
                }

                int retryCnt = 0;
                while (retryCnt <= _retryTimeoutCount)
                {
                    _handle = GetHandle();
                    if (_handle != IntPtr.Zero)
                    {
                        string msg = string.Format("acquired the handle in  {0}x{1}millisec interval. ", retryCnt,
                                                   _waitRetryMilliSec);


                        _logger.Warn(msg);
                        return _handle;
                    }
                    if (_waitRetryMilliSec != 0)
                        Thread.Sleep(_waitRetryMilliSec);
                    retryCnt++;
                }

                _logger.Warn(ToString());
                return IntPtr.Zero;
            }
        }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (FindControlDelegates != null)
            {
                T value;

                foreach (var valueDelegate in FindControlDelegates)
                {
                    value = valueDelegate.Invoke();
                    sb.Append(value.ToString());
                    sb.Append(" ");
                }
            }

            return
                string.Format(
                    "retried {0}times to get the control in {1}millisec interval. suggestion: try increase your retry count. the search was on:{2} ",
                    _retryTimeoutCount, _waitRetryMilliSec, sb);
        }

        private IntPtr GetHandle()
        {
            if (FindControlDelegates != null)
            {
                T value = default(T);

                IntPtr lastHandle = IntPtr.Zero;
                foreach (var valueDelegate in FindControlDelegates)
                {
                    value = valueDelegate.Invoke();
                    value.SetParent(lastHandle);
                    lastHandle = value.Handle;
                }
                return lastHandle;
            }

            return IntPtr.Zero;
        }


        public void Clear()
        {
            this.FindControlDelegates = null;
        }


    }
}