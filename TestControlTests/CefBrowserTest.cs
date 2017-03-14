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
using TestControl.Net.WebControls;
using System.Globalization;
using OpenQA.Selenium;

namespace TestControlTests
{
    [TestClass]
    public class CefBrowserTest
    {
        private static IApplicationUnderTest app = new ApplicationUnderTest(@"..\TestApps\", "ceftest.exe");
        private static WebBrowserControl aBrowser;

        //use Class Keys from Selenium Library.. 
        public static readonly string Enter = Convert.ToString(Convert.ToChar(0xE007, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {

            app.RunOnce();
            if (app.WaitForCaptionIfExists("Google", 20))
            {
                Assert.IsTrue(true);
            }
            else Assert.Fail("unable to get window caption");

            //locate the control as like any other .NET Control
            aBrowser = new WebBrowserControl("chrome");
            aBrowser.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                                () => new FindByAutomationId("Form1", true),
                                                () => new FindByName("Chrome Legacy Window")
                    ));
            Assert.IsTrue(aBrowser.IsVisible);

        }

        [ClassCleanup]
        public static void TearDown()
        {
            //app.Terminate();
        }

     
        [TestMethod]
        public void TestGetUrl()
        {
 
            var url = aBrowser.InternalDriver.GotoUrl("http://msn.com");
            Assert.AreEqual("http://msn.com", url);
            url = aBrowser.InternalDriver.GotoUrl("http://yahoo.com");
            Assert.AreEqual("http://yahoo.com", url);

        }

        [TestMethod]
        public void TestBasicFindChild()
        {
            aBrowser.InternalDriver.GotoUrl("http://google.com");
            var element = aBrowser.InternalDriver.FindChild("name:q");
            Assert.IsNotNull(element);
            element.Text = "TDD"+ Enter;
            element = aBrowser.InternalDriver.FindChild("name:q");
            Assert.AreEqual("lst-ib", element.AsString("Attribute:id"));
        }


        [TestMethod]
        public void TestBasicFindChildren()
        {
            aBrowser.InternalDriver.GotoUrl("http://google.com");
            var elements = aBrowser.InternalDriver.FindChildren("name:q");
            Assert.AreEqual(1, elements.Count);
       
            Assert.AreEqual("lst-ib", elements[0].AsString("Attribute:id"));
        }

        [TestMethod]
        public void TestExectueMethodWay()
        {
            aBrowser.InternalDriver.GotoUrl("http://www.littlewebhut.com/articles/html_iframe_example/");
            var frame = aBrowser.InternalDriver.FindChild("imgbox");

            Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", frame.AsString("attribute:src"));

            //switch frame
            aBrowser.InternalDriver.ExecuteMethod("SwitchTo").ExecuteMethod("Frame", new[] { frame.UnderlyingObject });

            var img = aBrowser.InternalDriver.FindChild("tagname:img");
            Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", img.AsString("attribute:src"));


        }

        [TestMethod]
        public void TestDirectSeleniumWay()
        {
            //reference TestControl.Extension.Selenium to get Extension methods.

            aBrowser.GetWebDriver().Url = "http://www.littlewebhut.com/articles/html_iframe_example/";
                
            var frame = aBrowser.GetWebDriver().FindElement(By.Id("imgbox"));

            Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", frame.GetAttribute("src"));

            //switch frame
            aBrowser.GetWebDriver().SwitchTo().Frame(frame);

            var img = aBrowser.GetWebDriver().FindElement(By.TagName("img"));
            Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", img.GetAttribute("src"));

        }

        [TestMethod]
        public void TestDirectSeleniumWayExtensionMethods()
        {
            //reference TestControl.Extension.Selenium to get Extension methods.

            aBrowser.GetWebDriver().Url = "http://www.littlewebhut.com/articles/html_iframe_example/";

            var frame = aBrowser.FindElement("imgbox");

            Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", frame.GetAttribute("src"));

            //switch frame
            aBrowser.GetWebDriver().SwitchTo().Frame(frame);

            var img = aBrowser.FindElement("tagname:img");
            Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", img.GetAttribute("src"));

        }

    }
}
