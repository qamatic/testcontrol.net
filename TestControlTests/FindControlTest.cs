using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class FindControlTest
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
        public void TestFindButtonControl()
        {
            var button = new ButtonControl();            
            button.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("buttonOk")
                    ));
            button.Click();


            Assert.AreEqual("clicked", TextBoxOne.Text);

        }

  
        [TestMethod]
        public void TestFindTextControl()
        {
            TextBoxOne.Text = "";
            Assert.AreEqual("", TextBoxOne.Text);
            TextBoxOne.Text = "all is well";
            Assert.AreEqual("all is well", TextBoxOne.Text);            
        }

        [TestMethod]
        public void TestFindByRoot()
        {
            var button = new ButtonControl();
            button.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindByAutomationId("Form1", true),
                                                () => new FindByName("Ok")
                    ));
            button.Click();
            Assert.AreEqual("clicked", TextBoxOne.Text);
        }

        [TestMethod]
        public void TestRightClickContextMenu_Copy_Paste()
        {

            
            TextBoxOne.Text = "right click copy";
            var textBox2 = new TextBoxControl();
            textBox2.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("textBox2")
                    ));
            textBox2.Text = "box2";

            TextBoxOne.Wait(3);


            TextBoxOne.ContextMenuClick("Select All");
            TextBoxOne.ContextMenuClick("Copy");

            textBox2.ContextMenuClick("Select All");
            textBox2.ContextMenuClick("Paste");
            Assert.AreEqual(TextBoxOne.Text, textBox2.Text);

        }

        [TestMethod]
        public void TestFindByName()
        {
            var button = new ButtonControl();
            button.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByName("Ok")
                    ));
            button.Click();


            Assert.AreEqual("clicked", TextBoxOne.Text);

        }

      

        private TextBoxControl _textBoxOne;
        public TextBoxControl TextBoxOne
        {
            get
            {
                if (_textBoxOne == null)
                {
                    _textBoxOne = new TextBoxControl();
                    _textBoxOne.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                        () => new FindWindow("Demo Form"),
                                                        () => new FindByAutomationId("textBox1")
                            ));
                }
                return _textBoxOne;
            }
        }

    }
}
