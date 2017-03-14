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
    public class FindChildByCaption : FindControl, IFindControl
    {
        private readonly string _caption;
        private readonly string _className;

        public FindChildByCaption(string caption, string className = "")
        {
            _caption = caption;
            _className = className;
        }

        #region IFindControl Members

        public override IntPtr Handle
        {
            get
            {
                if (ParentHandle == IntPtr.Zero)
                    return IntPtr.Zero;

                WindowItem[] windows = EnumWindowsHelper.GetChildWindows(ParentHandle);
                if (windows.Length != 0)
                {
                    WindowItem w = windows.FirstOrDefault(p => p.Caption == _caption && p.WndClassName == _className);
                    if (w != null)
                        return w.Handle;

                    if ((!string.IsNullOrEmpty(_caption)) && string.IsNullOrEmpty(_className))
                    {
                        w = windows.FirstOrDefault(p => p.Caption == _caption);
                        if (w != null)
                            return w.Handle;
                    }

                    if (string.IsNullOrEmpty(_caption) && (!string.IsNullOrEmpty(_className)))
                    {
                        w = windows.FirstOrDefault(p => p.WndClassName == _className);
                        if (w != null)
                            return w.Handle;
                    }
                }
                return IntPtr.Zero;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("FindChildByCaption({0}, {1})", _caption, _className);
        }
    }
}