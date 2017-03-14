using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestControl.Net.Interfaces;

namespace TestControl.Extension.Example
{
    public class ObjectInspectorExtension : IAppInject
    {
        private static ObjectInspector tcInspector = new ObjectInspector();
        public void LoadExtension(IntPtr ptr, uint processId)
        {             
            tcInspector.Text = "Test Control: " + Process.GetCurrentProcess().MainWindowTitle;
            tcInspector.Show();
  
            tcInspector.propertyGrid.SelectedObject = Control.FromHandle(ptr);
        }
    }
}
