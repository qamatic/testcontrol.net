using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestControl.Net;
using TestControl.Net.Interfaces;

namespace TestControl.Extension.Selenium
{
    public class ChromeWebDriverExtension : WebDriverExtension
    {
        public override DesiredCapabilities Capabilities
        {
            get
            {
                return DesiredCapabilities.Chrome();
            }
        }

        public override string DriverBinary
        {
            get
            {
                //this is a custom built chrome driver , made to work with test control.
                return @"TCChromeDriver.exe";
            }
        }

        public override string ConfigurationName
        {
            get
            {
                return "chrome";
            }
        }

        public override string getRemoteDriverAddress()
        {
            return "http://localhost:9515";
        }

        public override string getRemoteDriverArguments()
        {
            return "";
        }
    }
}
