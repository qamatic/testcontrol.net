using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class MenuTest
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
        public void TestOpenMenuOption()
        {
            Menu1.Click(new[]{"File", "Open Recent", "File1"});
            Assert.AreEqual("file1ToolStripMenuItem", TextBoxOne.Text);
        }



        public MenuStrip Menu1
        {
            get
            {
                var menu1 = new MenuStrip();
                menu1.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                     () => new Wait(500),
                                                     () => new FindWindow("Demo Form"),
                                                     () => new FindByAutomationId("menuStrip1")
                         ));
                return menu1;
            }
        }

     
        public TextBoxControl TextBoxOne
        {
            get
            {
                var textBox = new TextBoxControl();
                textBox.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                    () => new FindWindow("Demo Form"),
                                                    () => new FindByAutomationId("textBox1")
                        ));
                return textBox;
            }
        }

        public TextBoxControl TextBoxTwo
        {
            get
            {
                var textBox = new TextBoxControl();
                textBox.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                    () => new FindWindow("Demo Form"),
                                                    () => new FindByAutomationId("textBox2")
                        ));
                return textBox;
            }
        }

    }
}
