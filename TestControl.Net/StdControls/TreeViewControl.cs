using System;
using System.Text;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;

namespace TestControl.Net.StdControls
{
    public class TreeViewControl : TestControl, ITreeViewControl, ITreeViewUiaMarker
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


        public virtual string Text
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
            _logger.Info(string.Format("selecting value:{0} for {1}", item, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SelectItem(item);
        }
 
        public override void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {
            var cdef = new ControlLocatorDef<FindControl>(0, 0,
                                                () => new FromHandle(SystemUnderTestHandle),
                                                () => new ClickNonWindowControlByCaption(new[] { this.SelectedItem })
                    );
            cdef.Play();
        }

        public void DblClick()
        {
            Click();
            Click();
        }

 
        public IApplicationUnderTest AppUnderTest { get; set; }

        public void Expand()
        {
            LastElement.GetExpandCollapsePattern().Expand();
        }

        public void Collapse()
        {
            LastElement.GetExpandCollapsePattern().Collapse();
        }

        public virtual void SelectNext(string item)
        {
            _logger.Info(string.Format("continue selecting from previous: value:{0} for {1}", item, ActiveControlLocatorDef == null ? string.Empty : ActiveControlLocatorDef.ToString()));
            this.SelectContinueWith(item);
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
