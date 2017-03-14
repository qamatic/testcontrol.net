using System;
using System.Text;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;

namespace TestControl.Net.StdControls
{
    public class ComboBoxControl : TestControl, IComboBoxControl, IWin32MarkerExtension
    {
        private static ILogger _logger = LoggerMgr.GetNewLoggerInstance();
        public virtual string SelectedItem
        {
            get
            {
                return Text;
            }
        }

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


        public virtual  string Text
        {
            get
            {
                return this.GetSelectedItem();
            }
            set
            {
                if (!CanAssert)
                    return;
                Select(value);
            }
        }

        public virtual string[] Items
        {
            get { return this.GetItems(); }
        }

        public virtual void Select(string item)
        {
            
            _logger.Info(string.Format("setting dropdown value:{0} for {1}", item, ActiveControlLocatorDef==null?string.Empty:ActiveControlLocatorDef.ToString()));
            this.SelectItem(item);
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
