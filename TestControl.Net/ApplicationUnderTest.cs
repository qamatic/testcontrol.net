// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using TestControl.Net.Interfaces;
using TestControl.Natives;
using System.Configuration;
using System.Reflection;

namespace TestControl.Net
{
    public class ApplicationUnderTest : IApplicationUnderTest
    {
        private readonly string _exeName;
        private readonly string _workingDir;
        protected readonly Hashtable testControls = new Hashtable();
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="workingDir">   The working dir. </param>
        /// <param name="exeName">      Name of the executable. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public ApplicationUnderTest(string workingDir, string exeName)
        {
            _workingDir = workingDir;
            _exeName = exeName;
        }

       
        #region Implementation of ITestApplication

        /// <summary>   Shows the desktop. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ShowDesktop(int waitSec = 1)
        {
            NativeMethods.ShowDesktop();
            Wait(waitSec);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets handle of the process. </summary>
        ///
        /// <value> The process handle. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IntPtr ProcessHandle
        {
            get
            {
                Process[] p = Process.GetProcessesByName(GetAppName());
                return p.Length == 0 ? IntPtr.Zero : p[0].MainWindowHandle;
            }
        }

        public int ProcessId
        {
            get
            {
                Process[] p = Process.GetProcessesByName(GetAppName());
                return p.Length == 0 ? -1 : p[0].Id;
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sends the keys. </summary>
        ///
        /// <param name="keys"> The keys. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SendKeys(string keys)
        {
            System.Windows.Forms.SendKeys.SendWait(keys);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets the focus. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SetFocus()
        {
            NativeMethods.SetTopMost(ApplicationHandle, true);
            Wait(1);
            NativeMethods.SetTopMost(ApplicationHandle, false);
        }

        public virtual void RunOnce()
        {
            Run(true);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Runs this object. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Run(bool bTerminateIfRunning = false)
        {
            if (!File.Exists(WorkingDir + ExeName))
            {
                _logger.Error(WorkingDir + ExeName + " not found!");
                throw new Exception(WorkingDir + ExeName + " not found!");
            }
            if (bTerminateIfRunning && IsRunning)
            {
                Terminate();
            }
            var aProcess = new Process { StartInfo = { FileName = ExeName, WorkingDirectory = WorkingDir, Arguments= AppArguments } };

            aProcess.Start();
     
            _logger.Info(ExeName + " invoked");
        }

        public void Connect(String waitForCaption, int timeoutSec)
        {
            WaitForCaption(waitForCaption, timeoutSec);
            NativeMethods.AppBridge(ApplicationHandle);
        }

        protected virtual String AppArguments
        {
            get
            {
                const string defaultArgs = " --remote-debugging-port=57575 "; 
                string path = Assembly.GetExecutingAssembly().Location;
                Configuration config = ConfigurationManager.OpenExeConfiguration(path);
                try
                { //anything could fail and its optional,.  lazy and tired of checking for null!
                    var argValue = config.AppSettings.Settings["AppArgs"].Value;
                    return defaultArgs+argValue;
                }
                catch
                {
                    return defaultArgs;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Terminates this object. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Terminate()
        {
            string appName = GetAppName();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.StartsWith(appName))
                {
                    p.Kill();
                    _logger.Info(appName + " terminated");
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the on get class. </summary>
        ///
        /// <value> The name of the on get class. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public OnGetClassNameDelegate OnGetCustomWindowClassName { get; set; }
       
        private void Wait(int sec)
        {
            Thread.Sleep(sec * 1000);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether this object is running. </summary>
        ///
        /// <value> true if this object is running, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool IsRunning
        {
            get
            {
                string appName = GetAppName();
                Process[] aProcessInfo = Process.GetProcessesByName(appName);
                if (aProcessInfo.Length == 0)
                    return false;
                return true;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets handle of the application. </summary>
        ///
        /// <value> The application handle. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IntPtr ApplicationHandle
        {
            get
            {
                return ProcessHandle;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the active form handle. </summary>
        ///
        /// <value> The active form handle. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public  IntPtr ActiveFormHandle
        {
            get
            {
                return ApplicationHandle;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the working dir. </summary>
        ///
        /// <value> The working dir. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string WorkingDir
        {
            get { return _workingDir; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the name of the executable. </summary>
        ///
        /// <value> The name of the executable. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string ExeName
        {
            get { return _exeName; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Wait for caption. </summary>
        ///
        /// <param name="caption">  The caption. </param>
        /// <param name="timeout">  The timeout. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool WaitForCaption(string caption, int timeoutInSec)
        {
            return WaitForCaption(caption, timeoutInSec, true);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Queries if a given wait for caption if exists. </summary>
        ///
        /// <param name="caption">  The caption. </param>
        /// <param name="timeout">  The timeout. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool WaitForCaptionIfExists(string caption, int timeoutInSec)
        {
            return WaitForCaption(caption, timeoutInSec, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Queries if a test control is registered. </summary>
        ///
        /// <param name="wndClassName"> Name of the window class. </param>
        ///
        /// <returns>   true if a test control is registered, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool IsTestControlRegistered(string wndClassName)
        {
            return testControls[wndClassName] != null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Registers the test control described by wndClassName. </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="wndClassName"> Name of the window class. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RegisterTestControl<T>(string wndClassName) where T : class, ITestControl
        {
            Type t = typeof(T);
            if (!IsTestControlRegistered(wndClassName))
                testControls.Add(wndClassName, t);
            else
            {
                testControls[wndClassName] = t;
            }
        }      

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a test control. </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="controlLocatorDef">   The control def. </param>
        ///
        /// <returns>   The test control< t> </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual T GetTestControl<T>(IControlLocatorDef controlLocatorDef) where T : ITestControl
        {
            ITestControl testControl = null;
            Type typeToCreate = typeof(T);
            IntPtr handle = controlLocatorDef.Handle; //waits and retries done at this level.
            string wndClassName = (handle == IntPtr.Zero) ? string.Empty : GetWindowClassName(handle);
            if (IsTestControlRegistered(wndClassName))
            {
                typeToCreate = testControls[wndClassName] as Type;
                if ((typeToCreate.IsClass) && typeof(T).IsClass)
                    //if registration has class in it then type use T as a override  
                    typeToCreate = typeof(T);
            }
            try
            {
                try
                {
                    testControl = Activator.CreateInstance(typeToCreate, this) as ITestControl;
                }
                catch
                {
                    testControl = null;
                }

                if (testControl == null)
                {
                    testControl = Activator.CreateInstance(typeToCreate) as ITestControl;
                }

                ITestContextAware appSut = testControl as ITestContextAware;
                if (appSut != null)
                {
                    appSut.ApplicationUnderTest = this;
                }                

                testControl.SystemUnderTest(controlLocatorDef);
                testControl.WndClassName = wndClassName;
            }

            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
            
            return (T)testControl;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a window class name. </summary>
        ///
        /// <param name="handle">   The handle. </param>
        ///
        /// <returns>   The window class name. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string GetWindowClassName(IntPtr handle)
        {
            string className = NativeMethods.GetClassName(handle);

            if (OnGetCustomWindowClassName != null)
                return OnGetCustomWindowClassName(className);
            return className;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an automation object. </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="controlLocatorDef">   The control def. </param>
        ///
        /// <returns>   The automation object< t> </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual T GetAutomationObject<T>(IControlLocatorDef controlLocatorDef) where T : ITestControl
        {
            return GetTestControl<T>(controlLocatorDef);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Wait for caption. </summary>
        ///
        /// <param name="caption">      The caption. </param>
        /// <param name="timeoutInSec">      The timeout in seconds </param>
        /// <param name="throwError">   true to throw error. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool WaitForCaption(string caption, int timeoutInSec, bool throwError)
        {
            int elapsed = 0;
            while (elapsed != timeoutInSec)
            {
                IntPtr handle = NativeMethods.FindWindow(null, caption);
                if (handle != IntPtr.Zero)
                {
                    if (NativeMethods.IsWindowVisible(handle))
                        return true;
                }
                elapsed++;
                Wait(1);
            }

            //if (throwError)
            //{
            //    throw new Exception("timedout: cannot find caption: " + caption);
            //}
            // 
            _logger.Error("timedout: cannot find caption: " + caption);

            return false;
        }

        /// <summary>   Gets the application name. </summary>
        ///
        /// <returns>   The application name. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private string GetAppName()
        {
            return Path.GetFileNameWithoutExtension(ExeName);
        }

        #endregion


        public void CaptureScreen(string fileName, ImageFormat format)
        {
            NativeMethods.ScreenShot(fileName, format);
        }
    }
}