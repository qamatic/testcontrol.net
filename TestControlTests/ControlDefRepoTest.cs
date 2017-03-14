using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class ControlDefRepoTest
    {
              
        private ApplicationUnderTest _testApp;

        [TestInitialize]
        public void BaseState()
        {
            _testApp = new ApplicationUnderTest("", "TestControlTests.exe");
            _testApp.Terminate();
            _testApp.ShowDesktop();            
            _testApp.Run();

            var repo = new ControlLocatorDefRepo("app");
            repo.FindByCaption("Demo Form");

            repo = new ControlLocatorDefRepo("ok button");
            repo.FindUsing("app");
            repo.FindByAutomationId("buttonOk");

            repo = new ControlLocatorDefRepo("text box1");
            repo.FindUsing("app");
            repo.FindByAutomationId("textBox1");

            repo = new ControlLocatorDefRepo("text box2");
            repo.FindUsing("app");
            repo.FindByAutomationId("textBox2");

            repo = new ControlLocatorDefRepo("ok button using root automation id");
            repo.FindByAutomationId("Form1", true);
            repo.FindByAutomationId("buttonOk");

            repo = new ControlLocatorDefRepo("ok button using button name");
            repo.FindUsing("app");
            repo.FindByName("Ok");
 

        }

       
        [TestMethod]
        public void TestFindButtonControlUsingRepo()
        {            
            var button = new ButtonControl();
            button.SystemUnderTestFromRepo("ok button");
            button.Click();
            Assert.AreEqual("clicked", TextBoxOne.Text);
        }

        [TestMethod]
        public void TestRightClickContextMenu_Copy_PasteUsingRepo()
        {            
            TextBoxOne.Text = "right click copy";
            var textBox2 = new TextBoxControl();
            textBox2.SystemUnderTestFromRepo("text box2");
            textBox2.Text = "box2";
            TextBoxOne.Wait(2);
            TextBoxOne.ContextMenuClick("Select All");
            TextBoxOne.ContextMenuClick("Copy");
            textBox2.ContextMenuClick("Select All");
            textBox2.ContextMenuClick("Paste");
            Assert.AreEqual(TextBoxOne.Text, textBox2.Text);
        }



        [TestMethod]
        public void TestFindByRootUsingRepo()
        {
            var button = new ButtonControl();
            button.SystemUnderTestFromRepo("ok button using root automation id");
            button.Click();
            Assert.AreEqual("clicked", TextBoxOne.Text);
        }


        [TestMethod]
        public void TestFindByNameUsingRepo()
        {
            var button = new ButtonControl();           
            button.SystemUnderTestFromRepo("ok button using button name");
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
                    _textBoxOne.SystemUnderTestFromRepo("text box1");
                }
                return _textBoxOne;
            }
        }

    }
}
