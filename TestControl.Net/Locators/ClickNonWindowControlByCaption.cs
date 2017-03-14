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
    public class ClickNonWindowControlByCaption : FindControl, IFindControl
    {
        private readonly String[] _captionPath;
        private readonly bool _dblClick;
        private int _offSetX = 2;
        private int _offSetY = 2;

        public ClickNonWindowControlByCaption(String[] captionPath, bool dblClick = false, int offsetX = 2, int offsetY = 2)
        {
            _captionPath = captionPath;
            _dblClick = dblClick;
            _offSetX = offsetX;
            _offSetY = offsetY;
        }

        #region Overrides of FindControl

        public override IntPtr Handle
        {
            get
            {
                if (_captionPath != null)
                {
                    int lastIndex = 0;
                    foreach (string caption in _captionPath)
                    {
                        AccessibleObject[] accessibleObjects = WindowItem.Find(ParentHandle, new[] { caption },
                                                                               ref lastIndex);
                        if (accessibleObjects.Length == 0)
                            break;
                        var rect = new Rectangle(accessibleObjects[0].Location.Left, accessibleObjects[0].Location.Top,
                            accessibleObjects[0].Location.Right - accessibleObjects[0].Location.Left,
                            accessibleObjects[0].Location.Bottom - accessibleObjects[0].Location.Top);
                        var pt = CalcLocation(rect);
                        var cdef =
                            new ControlLocatorDef<IFindControl>(0, 0,
                                () => new MouseClick(pt),
                                () => new Wait(100)
                                );
                        if (_dblClick)
                        {
                            cdef =
                                new ControlLocatorDef<IFindControl>(0, 0,
                                    () => new MouseClick(pt),
                                    () => new MouseClick(pt),
                                    () => new Wait(100)
                                    );
                        }
                        cdef.Play();
                    }
                }
                return ParentHandle;
            }
        }

        protected virtual Point CalcLocation(Rectangle current)
        {
            var pt = new Point();
            pt.X = current.Left + _offSetX;
            pt.Y = current.Top + _offSetY;
            return pt;
        }

        #endregion
    }
}