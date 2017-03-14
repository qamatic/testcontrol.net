using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Natives;

namespace TestControl.Net.StdControls
{
    public class MenuStrip : TestControl, IWin32MarkerExtension
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        

        public virtual void Click(String[] menuByCaption)
        {
            var f = new ControlLocatorDef<FindControl>(
                                () => new FromHandle(SystemUnderTestHandle),
                                () => new ClickNonWindowControlByCaption(menuByCaption)
                         );
            f.Play();
        }

        public override void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {
            _logger.Info(string.Format("trying to click button:{0}", ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            throw new NotImplementedException();
        }


        public static ButtonControl FromControlDef(IControlLocatorDef controlLocatorDef)
        {
            var sut = WinControlUnderTest.FromControlDef(controlLocatorDef);
            var button = new ButtonControl();
            button.SystemUnderTest(sut);
            return button;
        }
        public AutomationElement AutomationElement
        {
            get
            {
                var sut = controlUnderTestInstance as WinControlUnderTest;
                return sut.AutomationElement;
            }
        }

    }

}
