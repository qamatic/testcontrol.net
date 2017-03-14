using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;

namespace TestControl.Net.StdControls
{

    public class EditorControl : TestControl, IEditorControl, IWin32MarkerExtension
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        

        public virtual string Text
        {
            set
            {
                _logger.Info(string.Format("setting text in TextBox:{0} for {1}", value, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
                this.SetContent(value);
            }
            get
            {
                return this.GetContent();
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


    }

}
