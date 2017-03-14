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

namespace TestControl.Net.Locators
{
    public class SendKeyStrokes : FindControl, IFindControl
    {
        private readonly bool _bWait;
        private readonly string _keys;

        public SendKeyStrokes(string keys, bool bWait = false)
        {
            _keys = keys;
            _bWait = bWait;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get
            {
                if (_bWait)
                    SendKeys.SendWait(_keys);
                else
                    SendKeys.Send(_keys);
                return ParentHandle;
            }
        }

        #endregion
    }
}