using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using TestControl.Net.Interfaces;
using TestControl.Natives;

namespace TestControl.Spy
{
    public class DisplayProperties : ControlProperties
    {
        [DisplayName("UI Patterns")]
        [ReadOnly(true)]
        [Editor(typeof(StringEditor), typeof(UITypeEditor))]
        public string UIPatterns { get; set; }

        //[DisplayName("Process")]
        //[ReadOnly(true)]
        //public String Process64
        //{
        //    get
        //    {                
        //      return NativeMethods.Is32OrWowProcess(new IntPtr(Handle)) ? "x32" : "x64";
        //    }
        //}


    }


    class StringEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
            {
                using (var frm = new UIPatternList())
                {
                    frm.listUiPatterns.Items.AddRange(value.ToString().Split('\n'));
                    if (svc.ShowDialog(frm) == DialogResult.OK)
                    {

                    }
                }
            }
            return value;
        }
    }
}
