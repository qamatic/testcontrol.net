using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using TestControl.Net.Extensions;
using TestControl.Net.Interfaces;
using TestControl.Net.Locators;
using TestControl.Natives;

namespace TestControl.Net.StdControls
{
    public class GridViewControl : TestControl, IGridViewControl, IGridViewUiaMarker
    {
        private WinControlUnderTest _accObjectTest;
        private IList<AccessibleObject> _accessibleItems;
        private IList<IGridRow> _rows;

        protected virtual IGridRow CreateGridRowInstance()
        {
            return new GridRow();
        }

        protected virtual IGridColumn CreateGridColumnInstance()
        {
            return new GridColumn();
        }

        public virtual IList<IGridRow> Rows
        {
            get
            {
                if (_rows == null)
                {
                    _rows = new List<IGridRow>();

                    foreach (var item in getAccessibleItems())
                    {
                        var colItem = (GridColumn) CreateGridColumnInstance();
                        colItem.InternalAccessibleObject = item;

                        if (colItem.IsRowType) 
                        {
                            _rows.Add(CreateGridRowInstance());
                        }
                        if (colItem.IsColumnType) //cols
                        {
                            var row = (GridRow)_rows[_rows.Count - 1];
                            if (row.Columns == null)
                            {
                                row.Columns = new List<IGridColumn>();
                                row.InternalAccessibleObject = item;
                            }
                            row.Columns.Add(colItem);
                            
                            
                           
                        }
                    }

                }
                return _rows;

            }
        }

        public AutomationElement AutomationElement
        {
            get
            {
                var sut = controlUnderTestInstance as WinControlUnderTest;
                return sut.AutomationElement;
            }
        }


        public virtual int RowCount
        {
            get { return Rows.Count; }
        }

        protected virtual IList<AccessibleObject> getAccessibleItems()
        {
            if (_accessibleItems == null)
            {
                var w = new WindowItem { Handle = SystemUnderTestHandle };
                w.WindowAccessibleObjects.ShowIntendedName = true;
                _accessibleItems = w.WindowAccessibleObjects.Items;
            }
            return _accessibleItems;
        }

        public virtual string Role
        {
            get { return "table"; }
        }
 


        public string Text
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var gridRow in Rows)
            {
                sb.AppendLine(gridRow.ToString());
            }
            return sb.ToString();
        }
    }

    public class GridRow : IGridRow
    {
        public virtual IList<IGridColumn> Columns { get; set; }
        public AccessibleObject InternalAccessibleObject { get; set; }

        public virtual int ColumnCount
        {

            get { return Columns.Count; }
        }
        public virtual string Role
        {
            
            get { return InternalAccessibleObject.Role; }
            set
            {
                
            }
        }

        public virtual string Text
        {
            get { return ToString(); }
            set {  }
        }

        public virtual void SetFocus()
        {
            InternalAccessibleObject.Click();
        }

        public virtual void Click()
        {
            InternalAccessibleObject.Click();
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var gridColumn in Columns)
            {
                if (sb.Length != 0)
                {
                    sb.Append("\t");
                }
                sb.Append(gridColumn.Text);
               
            }
            return sb.ToString();
        }
    }

    public class GridColumn : IGridColumn
    {
        public AccessibleObject InternalAccessibleObject { get; set; }
        public virtual uint RoleId
        {

            get { return InternalAccessibleObject.RoleId; }
            set
            {

            }
        }

        public virtual bool IsRowType
        {
            get { return IsRoleList33 || IsRoleTableRow28; }
        }

        public virtual bool IsColumnType
        {
            get { return IsRoleRowHeader25 || IsRoleCell26 | IsRoleCell29; }
        }

        protected bool IsRoleList33
        {
            get { return (RoleId == 33); }
        }

        protected  bool IsRoleTableRow28
        {
            get { return (RoleId == 28); }
        }

        protected  bool IsRoleRowHeader25
        {
            get { return (RoleId == 25); }
        }

        protected bool IsRoleCell29
        {
            get { return (RoleId == 29); }
        }

        protected bool IsRoleCell26
        {
            get { return (RoleId == 26); }
        }

        public virtual string Text
        {
            get { return InternalAccessibleObject.Value??string.Empty; }
            set { InternalAccessibleObject.Value = value; }
        }

        public virtual string Name
        {
            get { return InternalAccessibleObject.Name ?? string.Empty; }
            
        }

        public virtual void SetFocus()
        {
            InternalAccessibleObject.Click();
        }

        public virtual void Click()
        {
            InternalAccessibleObject.Click();
        }
    }
}