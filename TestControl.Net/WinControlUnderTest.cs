// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Windows.Automation;
using TestControl.Natives;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Net.StdControls;

namespace TestControl.Net
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Uia object under test. </summary>
    ///
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class WinControlUnderTest : ElementUnderTest, IElementUnderTest, IWin32MarkerExtension
    {
        private IControlLocatorDef _controlLocatorDef;
        private IntPtr _handle = IntPtr.Zero;

        #region IUiaAutomation Members

        /// <summary>   Gets the automation element. </summary>
        ///
        /// <value> The automation element. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public AutomationElement AutomationElement
        {
            get
            {
                if (IsObjectNull)
                {
                    SetUnderlyingObject(null);
                    var handle = GetHandle();
                    if (handle != IntPtr.Zero)
                        SetUnderlyingObject(AutomationElement.FromHandle(handle));
                }
                return UnderlyingObject as AutomationElement;
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initializes this object from the given from handle. </summary>
        ///
        /// <param name="handle">   The handle. </param>
        ///
        /// <returns>   . </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static WinControlUnderTest FromHandle(IntPtr handle)
        {
            var uiaSut = new WinControlUnderTest
                             {
                                 _handle = handle,

                             };
            uiaSut.SetUnderlyingObject(null);
            if (handle != IntPtr.Zero)
                uiaSut.SetUnderlyingObject(AutomationElement.FromHandle(handle));
            return uiaSut;
        }

      
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initializes this object from the given from control def. </summary>
        ///
        /// <param name="controlLocatorDef">   The control def. </param>
        ///
        /// <returns>   . </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static WinControlUnderTest FromControlDef(IControlLocatorDef controlLocatorDef, bool bCreateHandle=true)
        {
            var uiaSut = new WinControlUnderTest
                             {
                                 _controlLocatorDef = controlLocatorDef,
                             };
            uiaSut.SetUnderlyingObject(null);
            if (bCreateHandle)
            {
                var handle = controlLocatorDef.Handle;
                if (handle != IntPtr.Zero)
                    uiaSut.SetUnderlyingObject(AutomationElement.FromHandle(handle));
            }
            return uiaSut;
        }

        #region Implementation of ISystemUnderTest

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the handle. </summary>
        ///
        /// <returns>   The handle. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public override IntPtr GetHandle()
        {
            return _handle != IntPtr.Zero ? _handle : _controlLocatorDef.Handle;
        }

        public override void SetFocus()
        {
            NativeMethods.SetFocusByHandle(GetHandle());
        }

        public override void Click()
        {
            var pattern = AutomationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            pattern.Invoke();
        }

        public override bool IsEditable
        {
            get
            {
                var pattern = AutomationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                return !pattern.Current.IsReadOnly;
            }
        }

        public override bool IsVisible
        {
            get
            {                
                return !AutomationElement.Current.IsOffscreen;
            }
        }

        public override bool IsEnabled
        {
            get
            {
                return AutomationElement.Current.IsEnabled;
            }
        }


        #endregion


        public static ControlProperties GetProperties(AutomationElement ae)
        {
            ControlProperties aprop = new ControlProperties();
            aprop.Name = ae.Current.Name;
            aprop.Handle = ae.Current.NativeWindowHandle;
            aprop.Caption = ae.Current.Name;
            return aprop;
        }


        private static String Invoke(int processId, String method, IntPtr handle)
        {
            var textBox = new TextBoxControl();
            textBox.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("TestControlRig" + processId),
                                                () => new FindByAutomationId("method")
                    ));
            textBox.Text = method;
            textBox.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("TestControlRig" + processId),
                                                () => new FindByAutomationId("handle")
                    ));
            textBox.Text = handle.ToString();
            var button = new ButtonControl();
            button.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("TestControlRig" + processId),
                                                () => new FindByAutomationId("call")
                    ));
            button.Click();
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}