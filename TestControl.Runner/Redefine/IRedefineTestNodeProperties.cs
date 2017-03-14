// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections.Generic;
using TestControl.Net.BDD.Interfaces;

namespace TestControl.Runner.Redefine
{
    public interface IRedefineTestNodeProperties : ITestNode
    {
        string ContentText { get; }
        string FullFileName { get; set; }
        IList<string> ContentList { get; set; }
    }
}