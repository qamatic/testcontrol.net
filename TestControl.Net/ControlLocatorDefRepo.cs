// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Natives;

namespace TestControl.Net
{
    public class ControlLocatorDefRepo
    {
        private static IDictionary<String, IControlLocatorDef> _repoEntries = new Dictionary<string, IControlLocatorDef>();
        private IList<Func<FindControl>> _locators = new List<Func<FindControl>>();
        private ControlLocatorDef<FindControl> _cdef;
        public ControlLocatorDefRepo(String repoName)
        {      
            if (_repoEntries.ContainsKey(repoName))
            {
                //looks like ugly code here, in fact the class itself...
                _cdef = (ControlLocatorDef<FindControl>)_repoEntries[repoName];
                _locators = _cdef.FindControlDelegates.ToList();
                
            }
            else
            {
                _cdef = new ControlLocatorDef<FindControl>();                
                _repoEntries.Add(repoName, _cdef);
            }
            _cdef.FindControlDelegates = _locators;
        }

        public virtual void FindByCaption(String caption)
        {
            _locators.Add(() => new FindWindow(caption));
        }

        public virtual void FindByName(String name, bool bSearchFromRoot = false)
        {
            _locators.Add(() => new FindByName(name, bSearchFromRoot));
        }

        public virtual void FindDesktopWindow()
        {
            _locators.Add(() => new FindDesktopWindow());
        }

        public virtual void FindWindowByMousePosition()
        {
            _locators.Add(() => new FindWindowByMousePosition());
        }

        public virtual void FindChildByCaption(String caption)
        {
            _locators.Add(() => new FindChildByCaption(caption));
        }

        public virtual void ClickNonWindowControlByCaption(String caption, bool bDblClick = false)
        {
            _locators.Add(() => new ClickNonWindowControlByCaption(caption.Split('/'), bDblClick));
        }

        public virtual void FindByAutomationId(String automationId, bool bSearchFromRoot = false)
        {
            _locators.Add(() => new FindByAutomationId(automationId, bSearchFromRoot));
        }

        public void ClickMouse(int x, int y)
        {
            _locators.Add(() => new MouseClick(new System.Drawing.Point(x, y)));
        }

        public void RightClickMouse()
        {
            _locators.Add(() => new RightClickMouse());
        }

        public void SendKeyStrokes(string keys, bool bWait = false)
        {
            _locators.Add(() => new SendKeyStrokes(keys, bWait));
        }


        public virtual void Wait(int milliSec)
        {
            _locators.Add(() => new Wait(milliSec));
        }

        public virtual void FromHandle(IntPtr handle)
        {
            _locators.Add(() => new FromHandle(handle));
        }

        public virtual void FindFromDesktop()
        {
            _locators.Add(() => new FromHandle(NativeMethods.GetDesktopWindow()));
        }

  
        public virtual void FindUsing(String repoName)
        {
            var cdef = Find(repoName);
            foreach (Func<FindControl> item in cdef.FindControls)
            {
                _locators.Add(item);
            }
        }

        public static IControlLocatorDef Find(string locatorName)
        {
            if (_repoEntries.ContainsKey(locatorName))
                return _repoEntries[locatorName];
            return null;
        }

        public virtual  void SetRetryTime(int retryCount, int waitMilliSecPerRetry)
        {
            _cdef.SetRetryTime(retryCount, waitMilliSecPerRetry);
        }

        public virtual void Clear()
        {
            _locators.Clear();
        }

    }
}
