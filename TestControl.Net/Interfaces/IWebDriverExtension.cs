using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestControl.Net.Interfaces
{
    public interface IWebDriverExtension : IElementUnderTest
    {
     
        String ConfigurationName { get; }
        String GotoUrl(String url);
        bool Start();
        void Stop();
    }
}
