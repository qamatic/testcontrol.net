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
    public abstract class FindControl : IFindControl
    {
        protected IntPtr ParentHandle;

        #region IFindControl Members

        public virtual void SetParent(IntPtr handle)
        {
            ParentHandle = handle;
        }

        public abstract IntPtr Handle { get; }

        #endregion
    }
}