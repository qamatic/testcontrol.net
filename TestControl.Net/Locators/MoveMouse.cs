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
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Net.Locators
{
    public class MoveMouse : FindControl, IFindControl
    {
        private readonly int _moveToX;
        private readonly int _moveToY;


        public MoveMouse(int moveToX, int moveToY)
        {
            _moveToX = moveToX;
            _moveToY = moveToY;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get
            {
                if (ParentHandle == IntPtr.Zero)
                    MouseInput.MoveTo(new Point(_moveToX, _moveToY));
                else
                {
                    MouseInput.MoveTo(ParentHandle, _moveToX, _moveToY);
                }
                return ParentHandle;
            }
        }

        #endregion
    }
}