// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

namespace TestControl.Net.BDD.Interfaces
{
    public interface IFixtureData
    {
        string FixtureName { get; }


        string[] this[int index] { get; set; }

        int MaxColumn { get; }
        int Count { get; }

        int Add(string[] entry);
        void Insert(int index, string[] entry);
        void Remove(string[] entry);
    }
}