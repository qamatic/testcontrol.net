using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TestControl.Net.Interfaces;

namespace TestControl.Net.WebControls
{
    public class WebBrowserControl : TestControl, IWebBrowser
    {
        private String _browserConfigName;
        public WebBrowserControl(String browserConfigName)
        {
            _browserConfigName = browserConfigName;
        }

        protected virtual void OnBeforeLoadDriver(IWebDriverExtension webDriveExtension)
        {
            //do any capabilities change
            //for example:    (webDriveExtension as WebDriverExtension).capabilities
        }

        private IWebDriverExtension _webDriver;
        public virtual IWebDriverExtension InternalDriver
        {
            get
            {
                if (_webDriver == null)
                {
                    _webDriver = LoadWebDriverExtensionByName(_browserConfigName);
                    OnBeforeLoadDriver(_webDriver);
                    _webDriver.Start();
                }
                return _webDriver;
            }
        }

        protected virtual IWebDriverExtension LoadWebDriverExtensionByName(String name)
        {
            string asmFile = Assembly.GetExecutingAssembly().Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(asmFile);
            var appconfigValue = config.AppSettings.Settings["WebDriverExtensions"].Value ?? String.Empty;
            var extensions = appconfigValue.Split(',');
            foreach (var extensionAsm in extensions)
            {
                var asmFileName = extensionAsm.Trim();
                if (!File.Exists(asmFileName))
                    return null;
                var path = Path.GetDirectoryName(asmFile) + "\\";
                Assembly asm = Assembly.UnsafeLoadFrom(path+asmFileName);
                foreach (Type t in asm.GetTypes())
                {
                    Type intf = t.GetInterfaces().SingleOrDefault(p => p == typeof(IWebDriverExtension));
                    if (intf != null)
                    {
                        var extension = Activator.CreateInstance(t) as IWebDriverExtension;
                        if ((extension != null) && (extension.ConfigurationName == name))
                        {                          
                            return extension;
                        }
                    }
                }
            }
            return null;
        }
    }
}
