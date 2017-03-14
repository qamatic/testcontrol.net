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
    public interface IListBoxControl : ITestControl
    {
        string[] Items { get; }
        string[] SelectedItems { get; }
        bool IsMultiSelectable { get; }
        void Select(string itemCaption);
        void UnSelect(string itemCaption);
    }

    public interface IListBoxUiaMarker : IWin32MarkerExtension
    {
    }
}