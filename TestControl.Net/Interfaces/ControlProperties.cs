using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
 
namespace TestControl.Net.Interfaces
{
    public class ControlProperties
    {
        [Category("General")]
        [ReadOnly(true)]
        public String Name { get; set; }

        [Category("General")]
        [ReadOnly(true)]
        public String Caption { get; set; }

        [Category("Window")]
        [ReadOnly(true)]
        public String ClassName { get; set; }

        [Category("Window")]
        [ReadOnly(true)]
        public int Handle { get; set; }

        [Category("Window")]
        [ReadOnly(true)]
        public String AutomationId { get; set; }

      
    }

    
}
