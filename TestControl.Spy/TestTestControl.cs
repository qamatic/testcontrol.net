// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using TestControl.Net;
using TestControl.Net.Interfaces;

namespace TestControl.Spy
{
    public class TestTestControl : TestControl.Net.TestControl
    {
        #region Overrides of TestControl

 

        public override IElementUnderTest GetAutomationObject(IControlLocatorDef controlLocatorDef, bool bCreateHandle = true)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override void Click(bool rightButton = false, int relativeX = 0, int relativeY = 0)
        {
            throw new NotImplementedException();
        }
    }


    public class TestSystemUnderTest : ElementUnderTest
    {
        private readonly IntPtr _handle;

        public TestSystemUnderTest(IntPtr handle)
        {
            _handle = handle;
        }

        public override IntPtr GetHandle()
        {
            return _handle;
        }
    }
}