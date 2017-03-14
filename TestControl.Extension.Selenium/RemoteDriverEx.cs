using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Remote;
using System.IO;
using OpenQA.Selenium;
using System.Configuration;


namespace TestControl.Extension.Selenium
{
    public class RemoteDriverEx : RemoteWebDriver
    {

        public RemoteDriverEx(Uri remoteAddress, DesiredCapabilities dd)
            : base(remoteAddress, dd)
        {


        }

        //public String LastSessionId { get; set; }

        public String CurrentSessionId { get; set; }

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                if ((CurrentSessionId != null))
                {
                    return new Response
                    {
                        SessionId = CurrentSessionId,
                    };
                }
                else
                {
                    //LastSessionId = CurrentSessionId;
                    var response = base.Execute(driverCommandToExecute, parameters);
                    CurrentSessionId = response.SessionId;
                    return response;
                }
            }
            else
            {
                var response = base.Execute(driverCommandToExecute, parameters);
                return response;
            }
        }
    }
}
