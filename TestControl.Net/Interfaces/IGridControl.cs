// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
namespace TestControl.Net.Interfaces
{
    public interface IGridControl : ITestControl
    {
        string CellValue { get; }
        int RowCount { get; }
        int ColumnCount { get; }
        int VisibleColumnCount { get; }
        int VisibleRowCount { get; }
        int SelectedCount { get; }
        void SelectColumnRow(string columnName, int rowNum);
        void FindInColumnSearchOptions(string strToFind, string columnName, params object[] searchOptions);
        void FindNext();
        void Sort(string columnName);
    }
}