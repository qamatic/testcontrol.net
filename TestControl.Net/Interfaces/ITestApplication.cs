// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Drawing.Imaging;

namespace TestControl.Net.Interfaces
{
    public delegate string OnGetClassNameDelegate(string className);

    public interface IApplicationUnderTest
    {
        string WorkingDir { get; }
        string ExeName { get; }
        bool IsRunning { get; }
        IntPtr ApplicationHandle { get; }
        IntPtr ProcessHandle { get; }
        IntPtr ActiveFormHandle { get; }
        int ProcessId { get; }
        OnGetClassNameDelegate OnGetCustomWindowClassName { get; set; }                
        void SetFocus();
        void Run(bool bTerminateIfRunning = false);
        void RunOnce();
        void Terminate();
        void Connect(String waitForCaption, int timeoutSec);
        void ShowDesktop(int waitSec = 1);
        bool WaitForCaption(string caption, int timeoutSec);
        bool WaitForCaptionIfExists(string caption, int timeoutSec);
        bool WaitForCaption(string caption, int timeoutSec, bool throwError);
        void RegisterTestControl<T>(string wndClassName) where T : class, ITestControl;
        bool IsTestControlRegistered(string wndClassName);
        string GetWindowClassName(IntPtr handle);
        void SendKeys(string keys);        
        T GetTestControl<T>(IControlLocatorDef controlLocatorDef) where T : ITestControl;        
        T GetAutomationObject<T>(IControlLocatorDef controlLocatorDef) where T : ITestControl;
        void CaptureScreen(String fileName, ImageFormat format);
    }
}