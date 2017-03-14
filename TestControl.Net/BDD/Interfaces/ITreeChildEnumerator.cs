// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections;
using System.Collections.Generic;

namespace TestControl.Net.BDD.Interfaces
{
    public interface ITreeChildEnumerator : IEnumerable, IEnumerator
    {
        IList<ITestNode> ToList();
        Hashtable ToHashtable();
        Hashtable ToHashtableByPath();
        void ApplyLevelNumbers();
    }
}