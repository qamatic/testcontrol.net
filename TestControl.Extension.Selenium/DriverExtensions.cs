using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TestControl.Extension.Selenium;
using TestControl.Net.WebControls;

namespace TestControl.Net.WebControls
{
    public static class DriverExtensions
    {
        public static IWebDriver GetWebDriver(this WebBrowserControl webcontrol)
        {
            return webcontrol.InternalDriver.UnderlyingObject as IWebDriver;
        }

        public static IWebElement FindElement(this WebBrowserControl webcontrol, string locator)
        {            
            return webcontrol.GetWebDriver().FindElement(SeleniumWebElementWrapper.CreateLocator(locator));
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this WebBrowserControl webcontrol, string locator)
        {
            return webcontrol.GetWebDriver().FindElements(webcontrol.CreateLocator(locator));
        }

        public static By CreateLocator(this WebBrowserControl webcontrol, String locator)
        {
            return SeleniumWebElementWrapper.CreateLocator(locator);
        }
    }
}
