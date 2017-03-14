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
    public interface ITestControl
    {
        IntPtr SystemUnderTestHandle { get; }
        bool CanAssert { get; }
        bool IsEnabled { get; }
        bool IsVisible { get; }
        bool IsEditable { get; }
        bool IsReadOnly { get; }
        string LastErrorMessage { get; }
        IElementUnderTest SystemUnderTestInstance { get; }        
        string WndClassName { get; set; }
        void SetPropertyValue(string name, string value);
        void InvokeMethod(string methodName, params object[] parameters);
        void InvokeEvent(string eventName);
        void AssertException();
        void SetFocus();
        void SendKeys(string keys);
        void Wait(int sec, int millSec = 0);
        void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0);
        void SystemUnderTestIfExistsTimeout(IControlLocatorDef controlLocatorDef);
        void SystemUnderTest(IControlLocatorDef controlLocatorDef);
        void SystemUnderTestFromRepo(string fromRepo);
        IElementUnderTest GetAutomationObject(IControlLocatorDef controlLocatorDef, bool bCreateHandle = true);
        IControlLocatorDef ActiveControlLocatorDef { get; }
        IWin32MarkerExtension LastElement { set; get; }
        ControlProperties AutomationProperties { get; }
        String Name { get; }
        bool IsExists { get; }
        void CaptureScreen(string fileName, ImageFormat format);
        String getChildValuesByRole(uint roleid);
        

    }
}