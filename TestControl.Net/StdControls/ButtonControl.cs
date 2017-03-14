using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.StdControls
{
    public class ButtonControl : TestControl, IButtonControl
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
       
        public virtual string Text
        {
            set
            {
            
                throw new NotImplementedException();
            }
            get { return NativeMethods.GetWindowText(ActiveControlLocatorDef.Handle); }
        }


       
        public override void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {            
            _logger.Info(string.Format("trying to click button:{0}",  ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            SystemUnderTestInstance.Click();           
        }

    }

}
