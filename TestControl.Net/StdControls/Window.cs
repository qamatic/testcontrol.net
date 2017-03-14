using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Natives;

namespace TestControl.Net.StdControls
{
    public class Window : TestControl, IWindowControl, IWindowUiaMarker
    {
        private WinControlUnderTest _accObjectTest;

        public Window()
        {

        }

        public Window(string caption)
        {
            var cdef = new ControlLocatorDef<IFindControl>(30, 100, () => new FindByCaption(caption)
                );
            SystemUnderTest(cdef);
        }


        ///// <summary>
        /////     find child control inside a window
        /////     for example, Infromation/OK  where Information is a caption of the window and OK is a button on that window
        /////     Window("Information/OK");
        ///// </summary>
        ///// <param name="caption"></param>
        //public Window(string windowCaption, string childCaption)
        //{
        //    var cdef = new ControlLocatorDef<IFindControl>(TestApplication, 30, 100, () => new FindByCaption(windowCaption),
        //                                            () => new FindChildByCaption(childCaption)
        //        );
        //    SystemUnderTest(cdef);
        //}


        public string Caption
        {
            get { return NativeMethods.GetWindowText(SystemUnderTestHandle); }
            set { throw new NotImplementedException(); }
        }

        public virtual bool IsWindowVisible
        {
            get
            {
                if (SystemUnderTestHandle == IntPtr.Zero)
                    return false;
                return NativeMethods.IsWindowVisible(SystemUnderTestHandle);
            }
        }

        public bool IsModal
        {
            get { return this.IsModelDialog(); }
        }

        public override void SetFocus()
        {
            if (SystemUnderTestHandle != IntPtr.Zero)
                MouseInput.Click(SystemUnderTestHandle, false, 100, 1);
        }

        public void Close()
        {
            this.CloseWindow();
        }

  
        public override IElementUnderTest GetAutomationObject(IControlLocatorDef controlLocatorDef, bool bCreateHandle = true)
        {
            controlLocatorDef.SetRetryTime(20, 50);
            _accObjectTest = WinControlUnderTest.FromHandle(controlLocatorDef.Handle);
            return _accObjectTest;
        }
 
 
        public AutomationElement AutomationElement
        {
            get { return _accObjectTest.AutomationElement; }
        }

       
    }
}