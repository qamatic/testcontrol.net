using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestControl.Net.Interfaces
{
    public interface IWebBrowser : ITestControl
    {
        IWebDriverExtension InternalDriver { get; }
    }
}
