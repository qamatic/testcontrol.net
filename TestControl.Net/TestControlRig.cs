using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TestControl.Net.Interfaces;

namespace TestControl.Net
{
    public class TestControlRig
    {
        private static IList<IAppInject> _extensions;
        public static void Init(IntPtr ptr, uint processId)
        {
            if (_extensions == null)
            {
                _extensions = new List<IAppInject>(); 
                LoadExtensions(ptr, processId);
            }
            foreach (var testControlExtension in _extensions)
            {
                testControlExtension.LoadExtension(ptr, processId);
            }

        }

        private static void LoadExtensions(IntPtr ptr, uint processId)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(path);
            var appconfigValue = config.AppSettings.Settings["InjectExtensions"].Value ?? String.Empty;
            var extensions = appconfigValue.Split(',');
            foreach (var extensionAsm in extensions)
            {
                var asmFileName = extensionAsm.Trim();
                if (!File.Exists(asmFileName))
                    return;

                Assembly asm = Assembly.UnsafeLoadFrom(asmFileName);
                foreach (Type t in asm.GetTypes())
                {
                    Type intf = t.GetInterfaces().SingleOrDefault(p => p == typeof(IAppInject));
                    if (intf != null)
                    {
                        var extension = Activator.CreateInstance(t) as IAppInject;
                        if (extension != null)
                        {
                            _extensions.Add(extension);                           
                        }
                    }
                }
            }
        }

    }
}
