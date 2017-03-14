using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class CalculatorTest
    {
              
        private  ApplicationUnderTest _testApp = new ApplicationUnderTest(@"C:\Windows\System32\", "calc.exe");

        [TestInitialize]
        public void setup()
        {
            var repo = new ControlLocatorDefRepo("calcapp");
            repo.Wait(500);
            repo.FindByName("Calculator", true);

            repo = new ControlLocatorDefRepo("result");            
            repo.FindUsing("calcapp");
            repo.FindByAutomationId("150");

            repo = new ControlLocatorDefRepo("4");
            repo.FindUsing("calcapp");
            repo.FindByName("4");
            
            _testApp.Terminate();
            _testApp.Run();
        }

        [TestMethod]
        public void Test01_Caption()
        {
            var w = new Window();
            w.SystemUnderTestFromRepo("calcapp");
            Assert.AreEqual("Calculator", w.Name);            
        }

        
        [TestMethod]
        public void Test02_CalcResult()
        {
            var b = new ButtonControl();
            b.SystemUnderTestFromRepo("4");
            b.Click();
            var textBox = new TextBoxControl();
            textBox.SystemUnderTestFromRepo("result");
         
            Assert.AreEqual("4", textBox.Name);
        }
 
 

    }
}
