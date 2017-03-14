using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class ListBoxTest
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
        public void TestSetFocusOnScrollableWindw()
        {           
            ListBoxOne.SetFocus();
            Assert.AreEqual("listBox1 got focus", TextBoxOne.Text);
        }

        [TestMethod]
        public void TestSelectItem()
        {
            ListBoxOne.SetFocus();
            ListBoxOne.Select("item9");
            ListBoxOne.Click();
            Assert.AreEqual("Item9", TextBoxOne.Text);
        }

        [TestMethod]
        public void TestMultiSelectable()
        {
            ListBoxTwo.SetFocus();
            Assert.IsTrue(ListBoxTwo.IsMultiSelectable);
        }

        [TestMethod]
        public void TestUnSelectItem()
        {
            ListBoxTwo.SetFocus();  
            ListBoxTwo.Select("item9");
            Assert.AreEqual("Item9", ListBoxTwo.SelectedItems[0]);
            ListBoxTwo.UnSelect("item9");
            Assert.AreEqual(0, ListBoxTwo.SelectedItems.Length);            
        }

        public ListBoxControl ListBoxOne
        {
            get
            {
                var listBox = new ListBoxControl();
                listBox.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                     () => new FindWindow("Demo Form"),
                                                     () => new FindByAutomationId("listBox1")
                         ));
                return listBox;
            }
        }

        public ListBoxControl ListBoxTwo
        {
            get
            {
                var listBox = new ListBoxControl();
                listBox.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                     () => new FindWindow("Demo Form"),
                                                     () => new FindByAutomationId("listBox2")
                         ));
                return listBox;
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
