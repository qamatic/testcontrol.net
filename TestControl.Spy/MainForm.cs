
// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;
using TestControl.Net;
using TestControl.Net.Interfaces;
using TestControl.Natives;
using AccessibleObject = TestControl.Natives.AccessibleObject;
using ContextMenu = TestControl.Net.StdControls.ContextMenu;

namespace TestControl.Spy
{
    public partial class MainForm : Form
    {
        //private readonly ToolStripMenuItem _addHistory = new ToolStripMenuItem("Add to history");
         
        private static IntPtr hHook;
        private static WinTypes.HookProc _mouseHookProcedure;
        private static WinTypes.MouseHookStruct _currentSelection;
        private readonly ToolStripMenuItem _cancelMenu = new ToolStripMenuItem("Cancel");
        private readonly Cursor _dragCursor;

        private readonly IList<ITestControlSelection> _historyUpdate = new List<ITestControlSelection>();
        private readonly ContextMenuStrip _popupMenu = new ContextMenuStrip();
        private bool _dragInProgress;
        private IntPtr _hWnd;
        private AccessibleObject _oldAccObj;
        private IntPtr _oldWnd;        
        private DisplayProperties _props = new DisplayProperties();
        private bool DisableInternal;

        public MainForm()
        {
            InitializeComponent();
            LoadHistoryUpdateAsms();
            _dragCursor = GetCursor();
            objectGrid.SelectedObject = _props;
            foreach (ITestControlSelection history in _historyUpdate)
            {
                foreach (string mi in history.CustomMenuItems)
                {
                    var menu = new ToolStripMenuItem(mi);
                    _popupMenu.Items.Add(menu);
                    menu.Click += MenuClick;
                }
            }
            architectureLabel.Text = System.Environment.Is64BitProcess ? "x64" : "x32";
            if (!System.Environment.Is64BitProcess)
                this.Text = this.Text + String.Format("-({0})", architectureLabel.Text);
            MouseHook();
            _popupMenu.Items.Add(_cancelMenu);
            objectGrid.ViewForeColor = Color.FromArgb(1, 0, 0);
            if (ConfigurationManager.AppSettings["DisableInternal"] != null)
            {
                Boolean.TryParse(ConfigurationManager.AppSettings["DisableInternal"], out DisableInternal);
            }
        }

        //right now one but can be more..
        private void LoadHistoryUpdateAsms()
        {
            string asmFileName = ConfigurationManager.AppSettings["Addins"];
            if (!File.Exists(asmFileName))
                return;

            Assembly asm = Assembly.LoadFrom(asmFileName);
            foreach (Type t in asm.GetTypes())
            {
                Type intf = t.GetInterfaces().SingleOrDefault(p => p == typeof(ITestControlSelection));
                if (intf != null)
                {
                    _historyUpdate.Add(Activator.CreateInstance(t) as ITestControlSelection);
                }
            }
        }

        private void UpdateBack(string item)
        {
            richTextBox1.AppendText(item);
        }

        private void MenuClick(object sender, EventArgs e)
        {
            foreach (ITestControlSelection history in _historyUpdate)
            {
                history.OnCustomMenuItemClick(sender.ToString(), _props, UpdateBack);
            }
        }


        private static Cursor GetCursor()
        {
            // ReSharper disable AssignNullToNotNullAttribute

            return
                new Cursor(
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("TestControl.Spy.Resources.bullseye.cur"));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        private void DragHandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _dragInProgress = true;
            Cursor = _dragCursor;
        }


        private void DragHandleImageMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragInProgress) return;

            var txtObjectHandleText = String.Empty;
            var txtCaptionText = String.Empty;
            var txtClassNameText = String.Empty;
            var textBoxAutomationIdText = string.Empty;
            var textBoxNameText = string.Empty;

            _hWnd = DisableInternal?NativeMethods.WindowFromPoint(MousePosition):NativeMethods.WindowFromPointEx(MousePosition);

            if (_hWnd != _oldWnd)
            {
                if (_oldWnd != null)
                    NativeMethods.DrawSelectRect(_oldWnd);

                _oldWnd = _hWnd;
                NativeMethods.DrawSelectRect(_hWnd);
            }


            if (_hWnd != IntPtr.Zero)
            {
                txtObjectHandleText = _hWnd.ToString();                
                txtCaptionText = NativeMethods.GetWindowText(_hWnd);
                txtClassNameText = NativeMethods.GetClassName(_hWnd);
                
                AutomationElement ae = AutomationElement.FromHandle(_hWnd);
                object aeId = ae.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty, true);
                if (aeId != AutomationElement.NotSupported)
                {                                        
                    if (txtObjectHandleText != ae.Current.AutomationId)
                    {
                        textBoxAutomationIdText = ae.Current.AutomationId;                        
                    }

                    textBoxNameText = ae.Current.Name;
                }
                 
                foreach (ITestControlSelection history in _historyUpdate)
                    label2.Text = history.GetHandleInfo(_hWnd);
               //objectGrid.SelectedObject = UiaObjectUnderTest.GetProperties(ae);
                _props.Handle = _hWnd.ToInt32();
                _props.AutomationId = textBoxAutomationIdText;
                _props.ClassName = txtClassNameText;
                _props.Name = textBoxNameText;
                _props.Caption = txtCaptionText;
                objectGrid.Refresh();
                UpdateUiPatterns();
            }
        }

        private void UpdateUiPatterns()
        {
            AutomationElement ae = AutomationElement.FromHandle(_hWnd);
            var uiPatternText = new StringBuilder();
            
            foreach (AutomationPattern p in ae.GetSupportedPatterns())
            {
                uiPatternText.Append(p.ProgrammaticName + "\n");

            }
            _props.UIPatterns = uiPatternText.ToString();
        }

        private void DragHandleImageMouseUp(object sender, MouseEventArgs e)
        {
            if (!_dragInProgress) return;
            _dragInProgress = false;
            Cursor = Cursors.Default;
            if (_oldWnd != IntPtr.Zero)
            {
                if (!DisableInternal)
                {
                    NativeMethods.AppBridge(_oldWnd);
                }
                _oldAccObj = null;
                LoadAccessibleObjects();
                NativeMethods.DrawSelectRect(_oldWnd);
                _popupMenu.Show(MousePosition);
                _oldWnd = IntPtr.Zero;
            }
        }

        private void LoadAccessibleObjects()
        {
            try
            {
                if (!checkBoxGetAccObjects.Checked)
                    return;
                var w = new WindowItem { Handle = _hWnd };
                w.WindowAccessibleObjects.ShowIntendedName = true;

                listBoxAccObjects.DataSource = w.WindowAccessibleObjects.Items;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private void listBoxAccObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAccObjects.SelectedItem == null)
                return;

            if (_oldAccObj != null)
            {
                NativeMethods.DrawSelectRect(_hWnd, _oldAccObj);
            }
            var accObj = listBoxAccObjects.SelectedItem as AccessibleObject;
            _oldAccObj = accObj;
            textBoxAccObjPath.Text = accObj.Name;
            NativeMethods.DrawSelectRect(_hWnd, accObj);
            propertyGrid1.SelectedObject = accObj;
            // accObj.Click();
        }


        public void DrawAccessibleObjectLocation(WinTypes.MouseHookStruct ml)
        {
            var accList = listBoxAccObjects.DataSource as IList<AccessibleObject>;
            if (accList == null)
                return;
            AccessibleObject q =
                accList.SingleOrDefault(p => (p.Location.Left >= ml.pt.x) && (p.Location.Top >= ml.pt.y) &&
                                             (p.Location.Right <= ml.pt.x) && (p.Location.Bottom <= ml.pt.y)
                    );
            if (q != null)
            {
                listBoxAccObjects.SelectedItem = q;
            }
        }

        private void MouseHook()
        {
            // Create an instance of HookProc.
            _mouseHookProcedure = new WinTypes.HookProc(MouseHookProc);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                hHook = NativeMethods.SetWindowsHookEx(WinTypes.HookType.WH_MOUSE_LL,
                                                       _mouseHookProcedure,
                                                       NativeMethods.GetModuleHandle(curModule.ModuleName),
                                                       0);
            }
        }

        public static int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            _currentSelection =
                (WinTypes.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(WinTypes.MouseHookStruct));

            if (nCode < 0)
                return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
            String strCaption = "x = " +
                                _currentSelection.pt.x.ToString("d") +
                                "  y = " +
                                _currentSelection.pt.y.ToString("d");

            var frm = ActiveForm as MainForm;
            //frm.DrawAccessibleObjectLocation(_currentSelection);
            if (frm != null)
            {
                frm.toolStripCoord.Text = strCaption;
            }
            return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hHook != IntPtr.Zero)
                NativeMethods.UnhookWindowsHookEx(hHook);
        }

        private void buttonRightCaptureClick(object sender, EventArgs e)
        {
            var testControl = new TestTestControl();
            var sut = new TestSystemUnderTest(_hWnd);
            testControl.SystemUnderTest(sut);
            var contextMenu = new ContextMenu(testControl);
            IntPtr handle = contextMenu.GetControlDef(null).Play();
            var wi = new WindowItem { Handle = handle };
            List<AccessibleObject> cachedList = wi.WindowAccessibleObjects.Items.ToList();
            listBoxAccObjects.DataSource = cachedList;
        }

        private void buttonSingleClick_Click(object sender, EventArgs e)
        {
            if (listBoxAccObjects.SelectedItem == null)
                return;
            var accObj = listBoxAccObjects.SelectedItem as AccessibleObject;
            _oldAccObj = accObj;
            accObj.Click();
        }


        private void buttonDblClick_Click(object sender, EventArgs e)
        {
            buttonSingleClick_Click(sender, e);
            buttonSingleClick_Click(sender, e);
        }

        private void objectGrid_Click(object sender, EventArgs e)
        {

        }
    }
}