// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Drawing;
using System.Windows.Forms;
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.Locators
{
    public class MouseClick : FindControl, IFindControl
    {
        private readonly Point _point;
        
        public MouseClick()
        {  }

        public MouseClick(Point pt)
        {
            _point = pt;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get
            {
                if (_point == null)
                {
                    if (ParentHandle == IntPtr.Zero)
                        MouseInput.MoveToAndClick(Control.MousePosition);
                    else
                        MouseInput.Click(ParentHandle);
                }
                else
                {
                    MouseInput.MoveToAndClick(_point);
                }
                return ParentHandle;
            }
        }

        #endregion
    }
}