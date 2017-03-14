using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestControl.Net.Interfaces;

namespace TestControl.Extension.Example
{
    public partial class ObjectInspector : Form
    {
        public ObjectInspector()
        {
            InitializeComponent();
        }

        public void LoadExtension(IntPtr ptr, uint processId)
        {
            throw new NotImplementedException();
        }
    }
}
