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
    public abstract class WebDriverExtension : ElementUnderTest, IWebDriverExtension
    {

        private RemoteDriverEx _remoteDriver;

        public abstract string ConfigurationName { get; }

        public abstract string DriverBinary { get; }

        public virtual string DriverBinaryDirectory
        {
            get
            {
                return @"\.";
            }
        }

        public abstract DesiredCapabilities Capabilities { get; }

        public override IElementUnderTest FindChild(string locator)
        {
            return new SeleniumWebElementWrapper(_remoteDriver.FindElement(SeleniumWebElementWrapper.CreateLocator(locator)));
        }

        public override IList<IElementUnderTest> FindChildren(string locator)
        {
            var elements = _remoteDriver.FindElements(SeleniumWebElementWrapper.CreateLocator(locator));
            var list = new List<IElementUnderTest>();
            foreach (var item in elements)
            {
                list.Add(new SeleniumWebElementWrapper(item));
            }
            return list;
        }



        public virtual string GotoUrl(string url)
        {
            return _remoteDriver.Url = url;
        }

        public abstract string getRemoteDriverAddress();

        public abstract string getRemoteDriverArguments();

        public virtual bool Start()
        {
            if (_remoteDriver == null)
            {
                Stop();
                var aProcess = new Process { StartInfo = { FileName = DriverBinary, Arguments=getRemoteDriverArguments(), WorkingDirectory = DriverBinaryDirectory, CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true } };
                aProcess.Start();
                _remoteDriver = new RemoteDriverEx(new Uri(getRemoteDriverAddress()), Capabilities);
                SetUnderlyingObject(_remoteDriver);
            }
            return true;
        }


        public virtual void Stop()
        {
            var splits = DriverBinary.Split('.');
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.ToLower().StartsWith(splits[0].ToLower()))
                {
                    p.Kill();
                }
            }
        }
    }
}
