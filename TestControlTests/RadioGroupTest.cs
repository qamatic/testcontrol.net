using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class RadioGroupTest
    {
            
        private ApplicationUnderTest _testApp;

        [TestInitialize]
        public void BaseState()
        {
            _testApp = new ApplicationUnderTest("", "TestControlTests.exe");
            _testApp.Terminate();
            _testApp.ShowDesktop();            
            _testApp.Run();
        }

        [TestMethod]
        public void Test01_SelectRadioButton()
        {
            var radio = new RadioButtonControl();
            radio.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                    () => new FindWindow("Demo Form"),
                                                    () => new FindByAutomationId("radioButton1")
                        ));
            Assert.IsFalse(radio.Selected);
            radio.Selected = true;
            Assert.IsTrue(radio.Selected);
            
        }

        [TestMethod]
        public void Test02_SelectRadioButton()
        {
            var radio = new RadioButtonControl();
            radio.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                    () => new FindWindow("Demo Form"),
                                                    () => new FindByAutomationId("radioButton3")
                        ));            
            radio.Selected = true;
            Assert.IsTrue(radio.Selected);
            radio.UnSelect();

        }
 
    }
}
