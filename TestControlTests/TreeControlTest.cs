using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestControl.Net;
using TestControl.Net.Locators;
using System;
using TestControl.Net.StdControls;
using System.Threading;

namespace TestControlTests
{
    [TestClass]
    public class TreeControlTest
    {
              
        private ApplicationUnderTest _testApp;

        [TestInitialize]
        public void BaseState()
        {
            _testApp = new ApplicationUnderTest("", "TestControlTests.exe");
            _testApp.ShowDesktop();
            _testApp.Terminate();
            _testApp.Run();
        }
 
        [TestMethod]
        public void TestTreeClick()
        {
           
            var treeView = new TreeViewControl();
            treeView.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new Wait(1000),
                                                () => new FindWindow("Demo Form"),                                              
                                                () => new FindByAutomationId("treeView1")
                    ));

            treeView.Select("Node14");
            Assert.AreEqual("Node14", treeView.SelectedItem);
            treeView.DblClick();
            Assert.AreEqual("double clicked node: Node14", TextBoxOne.Text);
            
            treeView.Select("Node17");
            Assert.AreEqual("Node17", treeView.SelectedItem);
            treeView.DblClick();
            
            Assert.AreEqual("double clicked node: Node17", TextBoxOne.Text);
          
        }


        [TestMethod]
        public void TestTreeExpand()
        {
             
            var treeView = new TreeViewControl();
            treeView.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new Wait(1000),
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("treeView1")
                    ));
            treeView.Select("Node14");
            Assert.AreEqual("Node14", treeView.SelectedItem);

            treeView.Expand();
            treeView.Select("Node17");
            Assert.AreEqual("Node17", treeView.SelectedItem);
            treeView.DblClick();
            Assert.AreEqual("double clicked node: Node17", TextBoxOne.Text);

        }

        [TestMethod]
        public void TestTreeViewItems()
        {            
            var treeView = new TreeViewControl();
            treeView.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new Wait(1000),
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("treeView1")
                    ));
            Assert.AreEqual("Node0,Node12,Node13,Node14,Node15,Duplicate,Duplicate,Node20,Node21,Node22,Node23,Node24,Node25,Node26,Node100,Node101,DisplayText", treeView.Values);
        }


        [TestMethod]
        public void TestTreeOnScrollableWindw()
        {

            var treeView = new TreeViewControl();
            treeView.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new Wait(1000),
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("treeView1")
                    ));


            treeView.Select("Node24");
            Assert.AreEqual("Node24", treeView.SelectedItem);
            treeView.DblClick();
            Assert.AreEqual("double clicked node: Node24", TextBoxOne.Text);


            treeView.Select("DisplayText");
            Assert.AreEqual("DisplayText", treeView.SelectedItem);
            treeView.DblClick();
            Assert.AreEqual("double clicked node: DisplayText", TextBoxOne.Text);

        }



        [TestMethod]
        public void TestTreeSelectFromCurrentPosition()
        {

            var treeView = new TreeViewControl();
            treeView.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new Wait(1000),
                                                () => new FindWindow("Demo Form"),
                                                () => new FindByAutomationId("treeView1")
                    ));

            treeView.Select("Duplicate");
            treeView.DblClick();
            Assert.AreEqual("Duplicate", treeView.SelectedItem);
            Assert.AreEqual("double clicked node: Duplicate", TextBoxOne.Text);

            treeView.SelectNext("Duplicate");
            treeView.DblClick();
            Assert.AreEqual("Duplicate", treeView.SelectedItem);

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
