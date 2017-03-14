// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections.Generic;

namespace TestControl.Net.Interfaces
{
    public interface IGridViewControl : ITestControl
    {
        IList<IGridRow> Rows { get;   }
        int RowCount { get; }
        string Text { get;}
        string Role { get; }
    }

    public interface IGridRow
    {
        IList<IGridColumn> Columns { get; set; }
        int ColumnCount { get; }
        string Role { get; set; }
        string Text { get; set; }
        void SetFocus();
        void Click();
    }

    public interface IGridColumn
    {
        void SetFocus();
        string Text { get; set; }
        uint RoleId { get; set; }
        void Click();
         
        string Name { get; }
    }


    public interface IGridViewUiaMarker : IWin32MarkerExtension
    {
    }
}