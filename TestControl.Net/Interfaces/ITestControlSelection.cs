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

namespace TestControl.Net.Interfaces
{
    public delegate void UpdateHistory(string item);

    public interface ITestControlSelection : ISpyAddin
    {
        IList<string> CustomMenuItems { get; }
        string GetHandleInfo(IntPtr handle);

        void OnCustomMenuItemClick(string menuItem, ControlProperties controlProperties,
                                   UpdateHistory updateBack);
    }
}