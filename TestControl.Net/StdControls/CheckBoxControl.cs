using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;

namespace TestControl.Net.StdControls
{
    public class CheckBoxControl : TestControl, ICheckBoxControl, IWin32MarkerExtension
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        public void Select()
        {            
            _logger.Info(string.Format("checking check box value:{0} for {1}", "true", ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SetChecked(true);
        }

       

        public void UnSelect()
        {            
            _logger.Info(string.Format("unchecking check box value:{0} for {1}", "false", ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SetChecked(false);
        }

        public bool Value
        {
            get
            {
                return IsChecked;
            }
        }

        public bool IsChecked
        {
            get
            {
                return (this.GetCheckStatus() == ToggleState.On);
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
