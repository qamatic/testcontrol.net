using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;

namespace TestControl.Net.StdControls
{
    public class TextBoxControl : TestControl, ITextBoxControl, IWin32MarkerExtension
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        

        public virtual string Text
        {
            set
            {
                
                _logger.Info(string.Format("setting text in TextBox:{0} for {1}", value, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
                this.SetText(value);
            }

            get
            {
                return this.GetText();
            }
        }

        public AutomationElement AutomationElement
        {
            get
            {
                var sut = controlUnderTestInstance as WinControlUnderTest;
                return sut.AutomationElement;
            }
        }


        public static TextBoxControl FromControlDef(IControlLocatorDef controlLocatorDef)
        {
            var sut = WinControlUnderTest.FromControlDef(controlLocatorDef);
            var button = new TextBoxControl();
            button.SystemUnderTest(sut);
            return button;
        }

    }

}
