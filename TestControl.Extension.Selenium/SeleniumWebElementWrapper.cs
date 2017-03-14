using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestControl.Net;
using TestControl.Net.Interfaces;

namespace TestControl.Extension.Selenium
{
    public class SeleniumWebElementWrapper : ElementUnderTest
    {
        private IWebElement _element;
        public SeleniumWebElementWrapper(IWebElement element)
        {
            _element = element;
            SetUnderlyingObject(element);
        }

        public override IElementUnderTest FindChild(string locator)
        {
            //_element.fin
            return new SeleniumWebElementWrapper(_element.FindElement(CreateLocator(locator)));
        }

        public override IList<IElementUnderTest> FindChildren(string locator)
        {
            var elements = _element.FindElements(CreateLocator(locator));
            var list = new List<IElementUnderTest>();
            foreach(var item in elements)
            {
                list.Add(new SeleniumWebElementWrapper(item));
            }
            return list;
        }

     
        public override bool IsVisible
        {
            get
            {
                return _element.Displayed;
            }
        }

        public override bool IsEnabled
        {
            get
            {
                return _element.Enabled;
            }
        }

        public override string Text
        {
            get
            {
                return _element.Text;
            }

            set
            {
                _element.SendKeys(value);
            }
        }

        public override void Click()
        {
            _element.Click();
        }

        public override void Clear()
        {
            _element.Clear();
        }

        public override string AsString(string propertyName)
        {
            var splits = propertyName.Split(':');
            if (splits.Length != 2)
            {
                throw new Exception(String.Format("{0} - property name format is wrong. Usuage: Attribute:<name> or CssValue:<name> ", propertyName));
            }
            splits[0] = splits[0].ToLower();
            if (splits[0] == "css")
            {
                return _element.GetCssValue(splits[1]);
            }
            return _element.GetAttribute(splits[1]);
        }

        public static By CreateLocator(String locator)
        {
            if (locator.IndexOf(':') > 0)
            {
                var splits = locator.Split(':');
                var prefix = splits[0].ToLower();
                var selector = splits[1];
                if (prefix == "name")
                {
                    return By.Name(selector);
                }
                if ((prefix == "css") || (prefix == "cssselector"))
                {
                    return By.CssSelector(selector);
                }
                if ((prefix == "class") || (prefix == "classname"))
                {
                    return By.ClassName(selector);
                }
                if (prefix == "xpath")
                {
                    return By.XPath(selector);
                }
                if ((prefix == "link") || (prefix == "linktext"))
                {
                    return By.LinkText(selector);
                }
                if (prefix == "tagname")
                {
                    return By.TagName(selector);
                }
                if ((prefix == "partiallink") || (prefix == "partiallinktext"))
                {
                    return By.PartialLinkText(selector);
                }

            }
            return By.Id(locator);
        }
    }
}
