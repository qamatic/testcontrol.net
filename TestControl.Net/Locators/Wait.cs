// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Threading;
using TestControl.Net.Interfaces;

namespace TestControl.Net.Locators
{
    public class Wait : FindControl, IFindControl
    {
        private readonly int _milliSec;

        public Wait(int milliSec)
        {
            _milliSec = milliSec;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get
            {
                Thread.Sleep(_milliSec);
                return ParentHandle;
            }
        }

        #endregion
    }
}