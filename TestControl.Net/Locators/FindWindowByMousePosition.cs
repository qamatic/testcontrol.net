// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Windows.Forms;
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.Locators
{
    public class FindWindowByMousePosition : FindControl, IFindControl
    {
        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get { return NativeMethods.WindowFromPoint(Control.MousePosition); }
        }

        #endregion
    }
}