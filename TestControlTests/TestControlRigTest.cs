using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Interfaces;
using TestControl.Net.StdControls;
using TestControl.Net.Locators;

namespace TestControlTests
{
    [TestClass]
    public class TestControlRigTest
    {
         
        private ApplicationUnderTest _testApp;

        [TestInitialize]
        public void BaseState()
        {
            _testApp = new ApplicationUnderTest("", "TestControlTests.exe");
            _testApp.ShowDesktop();
            _testApp.Run();
            _testApp.WaitForCaption("Demo Form", 2);
        }

       


    }
}
