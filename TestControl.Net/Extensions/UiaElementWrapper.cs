using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using TestControl.Net.Interfaces;

namespace TestControl.Net.Extensions
{
    public class UiaElementWrapper : IWin32MarkerExtension
    {
        private AutomationElement _aElement;
        public UiaElementWrapper(AutomationElement aElement)
        {
            _aElement = aElement;
        }

        public System.Windows.Automation.AutomationElement AutomationElement
        {
            get { return _aElement; }
        }
    }
}
