using System;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;

namespace TestControl.Net.StdControls
{
    public class ListBoxControl : TestControl, IListBoxControl, IListBoxUiaMarker
    {


        public bool IsMultiSelectable
        {
            get { return this.IsMultiSelectAllowed(); }
        }

        public string[] Items
        {
            get { throw new NotImplementedException(); }
        }

        public void Select(string itemCaption)
        {
            this.SelectItem(itemCaption, IsMultiSelectable);

        }

        public string[] SelectedItems
        {
            get { return this.GetSelectedItems(); }
        }

        public void UnSelect(string itemCaption)
        {
            this.UnSelectItem(itemCaption);
        }

       

        public override void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {
            var cdef = new ControlLocatorDef<FindControl>(
                                                () => new FromHandle(SystemUnderTestHandle),
                                                () => new ClickNonWindowControlByCaption(new[] { SelectedItems[0] })
                    );
            cdef.Play();
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
