// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.Locators
{
    public class FindByCaption : FindControl, IFindControl
    {
        private readonly string _caption;
        private readonly string _className;

        public FindByCaption(string caption, string className = null)
        {
            _caption = caption;
            _className = className;
        }

        #region IFindControl Members

        public override IntPtr Handle
        {
            get { return NativeMethods.FindWindow(_className, _caption); }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("FindByCaption({0}, {1})", _caption, _className);
        }
    }
}