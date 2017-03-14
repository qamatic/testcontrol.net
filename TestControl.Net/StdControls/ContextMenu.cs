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
using TestControl.Net.Locators;
using TestControl.Natives;

namespace TestControl.Net.StdControls
{
    public class ContextMenu
    {
        private readonly ITestControl _testControl;

        public ContextMenu(ITestControl testControl)
        {
            _testControl = testControl;
        }

        public virtual void Select(string[] captions)
        {
            var cdef = GetControlDef(captions);
            cdef.Play();
        }

        public IControlLocatorDef GetControlDef(string[] captions)
        {
            var cdef = new ControlLocatorDef<IFindControl>(() => new FromHandle(_testControl.SystemUnderTestHandle),
                                                () => new RightClickMouse(),
                                                () => new Wait(100),
                                                () => new SendKeyStrokes("{DOWN}", true),
                                                () => new Wait(100),
                                                () => new FindWindowByMousePosition(),
                                                () => new Wait(100),
                                                () => new ClickNonWindowControlByCaption(captions)
                );
            return cdef;
        }
    }
}