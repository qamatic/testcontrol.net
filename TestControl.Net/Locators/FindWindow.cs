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
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.Locators
{
    public class FindWindow : FindControl, IFindControl
    {
        private readonly string _caption;
        private readonly string _className;

        public FindWindow(string caption, string className = "")
        {
            _caption = caption;
            _className = className;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get
            {
                WindowItem handle = EnumWindowsHelper.GetWindows(_caption, _className).FirstOrDefault();
                if (handle != null)
                    return handle.Handle;

                return IntPtr.Zero;
            }
        }

        #endregion
    }
}