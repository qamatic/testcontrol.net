using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestControl.Net.Interfaces
{
    public interface IAppInject
    {
        void LoadExtension(IntPtr ptr, uint processId);
    }
}
