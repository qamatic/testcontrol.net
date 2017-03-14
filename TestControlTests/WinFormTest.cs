using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Interfaces;
using TestControl.Net.StdControls;

namespace TestControlTests
{
    [TestClass]
    public class WinFormTest
    { 

        [TestMethod]
        public void TestCheckAppExists()
        {

            var demoApp = new ApplicationUnderTest("", "");
            demoApp.ShowDesktop();
            try
            {
                demoApp.RunOnce();
                Assert.Fail("Test Application not there but test passing");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestLaunchWinFormDemoApp()
        {

            var demoApp = new ApplicationUnderTest("", "TestControlTests.exe");
            demoApp.ShowDesktop();
            demoApp.RunOnce();
            if (demoApp.WaitForCaptionIfExists("***Demo Form-aaaaa", 2))
            {
                Assert.Fail("this should not pass");
            }

            if (demoApp.WaitForCaptionIfExists("Demo Form", 2))
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail("unable to get demoapp window caption");

            demoApp.Terminate();


        }

    }
}
