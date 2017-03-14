using System;
using System.Text;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;

namespace TestControl.Net.StdControls
{
    public class MenuControl : TestControl, IMenuControl, IMenuControlUiaMarker
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
       
        

        public virtual string Values
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var st in Items)
                    sb.Append(sb.Length == 0 ? st : "," + st);
                return sb.ToString();
            }
        }
 
        public virtual string[] Items
        {
            get { return this.GetItems(); }
        }

        public virtual void Open(string menuOption)
        {
            _logger.Info(string.Format("selecting value:{0} for {1}", menuOption, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SelectItem(menuOption);
            LastElement.GetExpandCollapsePattern().Expand();
        }


        public virtual void Close(string menuOption)
        {
            _logger.Info(string.Format("selecting value:{0} for {1}", menuOption, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SelectItem(menuOption);
            LastElement.GetExpandCollapsePattern().Collapse();
        }
 
        public override void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {
            LastElement.GetInvokePattern().Invoke();
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
