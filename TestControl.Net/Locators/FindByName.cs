// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Linq;
using System.Windows.Automation;
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.Locators
{
    public class FindByName : FindControl, IFindControl
    {
        private readonly string _name;
        private readonly bool _bSearchFromRoot;

        public FindByName(string name, bool bsearchFromRoot = false)
        {
            _name = name;
            _bSearchFromRoot = bsearchFromRoot;
        }

        #region IFindControl Members

        public override IntPtr Handle
        {
            get
            {
                AutomationElement ae = null;
                if (_bSearchFromRoot)
                {                    
                    ae = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, _name));
                }
                else
                {
                    if (ParentHandle == IntPtr.Zero)
                        return IntPtr.Zero;
                    var parent = AutomationElement.FromHandle(ParentHandle);
                    ae = parent.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, _name));
                }             
               
                if (ae != null)
                    return new IntPtr( ae.Current.NativeWindowHandle );
                return IntPtr.Zero;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("FindByName({0})", _name);
        }
    }
}