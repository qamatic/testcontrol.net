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

namespace TestControl.Net.Locators
{
    public class FromHandle : FindControl, IFindControl
    {
        private readonly IntPtr _handle;

        public FromHandle(IntPtr handle)
        {
            _handle = handle;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get { return _handle; }
        }

        #endregion
    }
}