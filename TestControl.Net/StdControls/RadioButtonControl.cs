using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;

namespace TestControl.Net.StdControls
{
    public class RadioButtonControl : TestControl, IRadioButtonControl, IWin32MarkerExtension
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        public bool Selected
        {
            set
            {                
                _logger.Info(string.Format("setting radio button value:{0} for {1}", value, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
                this.SetSelected(value);

            }
            get
            {
                return this.IsSelected();

            }
        }
 
       

        public void UnSelect()
        {
            _logger.Info(string.Format("unchecking radio box value:{0} for {1}", "false", ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SetSelected(false);
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
