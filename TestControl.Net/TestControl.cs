// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Natives;
using TestControl.Net.StdControls;
using ContextMenu = TestControl.Net.StdControls.ContextMenu;

namespace TestControl.Net
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Test control. </summary>
    ///
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public  class TestControl : ITestControl
    {
        private IControlLocatorDef _controlLocatorDef;
        private bool _ifExists;
        protected IElementUnderTest controlUnderTestInstance;

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region ITestControl Members

        /// <summary>   Gets or sets the name of the window class. </summary>
        ///
        /// <value> The name of the window class. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string WndClassName { get; set; }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests system under. </summary>
        ///
        /// <param name="controlLocatorDef">   The control def. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SystemUnderTest(IControlLocatorDef controlLocatorDef)
        {
            _controlLocatorDef = controlLocatorDef;
            controlUnderTestInstance = GetAutomationObject(controlLocatorDef, false);
            _ifExists = false;
        }


        public virtual void SystemUnderTestFromRepo(string fromRepo)
        {
            var cdef = ControlLocatorDefRepo.Find(fromRepo);
            SystemUnderTest(cdef);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   System under test if exists timeout. </summary>
        ///
        ///<param name="controlLocatorDef"></param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SystemUnderTestIfExistsTimeout(IControlLocatorDef controlLocatorDef)
        {
            _controlLocatorDef = controlLocatorDef;
            controlUnderTestInstance = GetAutomationObject(controlLocatorDef);
            _ifExists = true;
        }

        public IControlLocatorDef ActiveControlLocatorDef
        {
            get { return _controlLocatorDef; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets handle of the system under test window. </summary>
        ///
        /// <value> The system under test window handle. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual IntPtr SystemUnderTestHandle
        {
            get
            {
                if (!CanAssert)
                    return IntPtr.Zero;
                return controlUnderTestInstance.GetHandle();
            }
        }

        public virtual bool IsExists
        {
            get { return (SystemUnderTestHandle != IntPtr.Zero); }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether we can assert. </summary>
        ///
        /// <value> true if we can assert, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool CanAssert
        {
            get { return (!_ifExists) || ((_ifExists) && (controlUnderTestInstance != null)); }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets a property value. </summary>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="value">    The value. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SetPropertyValue(string name, string value)
        {
            if (!CanAssert)
                return;
            controlUnderTestInstance.SetAsString(name, value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Invoke method. </summary>
        ///
        /// <param name="methodName">   Name of the method. </param>
        /// <param name="parameters">   Options for controlling the operation. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void InvokeMethod(string methodName, params object[] parameters)
        {
            if (!CanAssert)
                return;
            controlUnderTestInstance.ExecuteMethod(methodName, parameters);

            AssertException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Invoke event. </summary>
        ///
        /// <param name="eventName">    Name of the event. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void InvokeEvent(string eventName)
        {
            if (!CanAssert)
                return;
            controlUnderTestInstance.ExecuteEvent(eventName);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Assert exception. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void AssertException()
        {
            if (LastErrorMessage.Length != 0)
                throw new Exception(LastErrorMessage);
        }

         public virtual bool IsReadOnly
        {
            get
            {
                return !IsEditable;
            }

        }

        public virtual bool IsEditable
        {
            get
            {
                return SystemUnderTestInstance.IsEditable;
            }

        }

        public virtual bool IsVisible
        {
            get
            {
                return SystemUnderTestInstance.IsVisible;
            }

        }

        public virtual bool IsEnabled
        {
            get { return SystemUnderTestInstance.IsEnabled; }
    
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a message describing the last error. </summary>
        ///
        /// <value> A message describing the last error. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string LastErrorMessage
        {
            get { return controlUnderTestInstance.GetLastError() ?? string.Empty; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets the focus. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SetFocus()
        {
            if (SystemUnderTestHandle == IntPtr.Zero)
                return;
            controlUnderTestInstance.SetFocus();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sends the keys. </summary>
        ///
        /// <param name="keys"> The keys. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SendKeys(string keys)
        {
            SetFocus();
            System.Windows.Forms.SendKeys.SendWait(keys);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Waits. </summary>
        ///
        /// <param name="sec">  The security. </param>
        ///<param name="millSec"></param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Wait(int sec, int millSec = 0)
        {
            Thread.Sleep((sec * 1000) + millSec);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>    Gets an automation object. </summary>
        ///
        /// <param name="controlLocatorDef">    The control def. </param>
        ///
        /// <returns>    The automation object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual IElementUnderTest GetAutomationObject(IControlLocatorDef controlLocatorDef, bool bCreateHandle = true)
        {
            return WinControlUnderTest.FromControlDef(controlLocatorDef);
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the system under test instance. </summary>
        ///
        /// <value> The system under test instance. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual IElementUnderTest SystemUnderTestInstance
        {
            get { return controlUnderTestInstance; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Mouse click. </summary>
        ///
        /// <param name="rightButton">  true to right button. </param>
        /// <param name="relativeX">    The relative x coordinate. </param>
        /// <param name="relativeY">    The relative y coordinate. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {
            MouseInput.MoveToAndClick(SystemUnderTestHandle);
        }


        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests system under. </summary>
        ///
        /// <param name="sut">  The sut. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void SystemUnderTest(IElementUnderTest sut)
        {
            controlUnderTestInstance = sut;
        }

        public virtual ContextMenu GetContextMenu()
        {
            return new ContextMenu(this);
        }

        public virtual void ContextMenuClick(string captionsDelimitedbySlash)
        {
            var splits = Regex.Split(captionsDelimitedbySlash, "/");
            GetContextMenu().Select(splits);
        }

        public virtual string[] GetClipboardData()
        {

            if (Clipboard.GetDataObject().GetDataPresent(System.Windows.Forms.DataFormats.Text))
            {
                var clipBoardText = Clipboard.GetDataObject().GetData(System.Windows.Forms.DataFormats.Text).ToString();
                if (!string.IsNullOrEmpty(clipBoardText))
                    return Regex.Split(clipBoardText, "\r\n");
            }

            return new string[] { };
        }

        public virtual IWin32MarkerExtension LastElement { get; set; }



        

        public ControlProperties AutomationProperties
        {
            get
            {
                var sut = controlUnderTestInstance as WinControlUnderTest;
                AutomationElement ae = sut.AutomationElement;
                return WinControlUnderTest.GetProperties(ae);
            }
        }


        public string Name
        {
            get
            {
                return AutomationProperties.Name;
            }
        }

        public void CaptureScreen(string fileName, ImageFormat format)
        {
            NativeMethods.ScreenShot(SystemUnderTestHandle, fileName, format);
        }

        public String getChildValuesByRole(uint roleid)
        {
            var sb = new StringBuilder();
            var w = new WindowItem() {Handle = SystemUnderTestHandle};
            w.WindowAccessibleObjects.ShowIntendedName = false;
            foreach (var item in w.WindowAccessibleObjects.Items)
            {
                if (item.RoleId == roleid)
                {
                    sb.AppendLine(item.Value);
                }
            }
            return sb.ToString();
        }

       
    }
}