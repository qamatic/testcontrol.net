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
using TestControl.Net.Interfaces;

namespace TestControlTests
{
    public class DemoPlugin : ITestControlSelection
    {
        #region Implementation of IHistory

        public string GetHandleInfo(IntPtr handle)
        {
            return "Status: Info";
        }


        public IList<string> CustomMenuItems
        {
            get
            {
                var list = new List<string> {"item1{0}", "item2{0}"};
                return list;
            }
        }
 
       
        public void OnCustomMenuItemClick(string menuItem, ControlProperties controlProperties, UpdateHistory updateBack)
        {
            if (updateBack != null)
                updateBack(string.Format(menuItem, " handle " + ":" + controlProperties.ClassName + ":" + controlProperties.Caption));
        }

        #endregion


    }
}