// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Collections;
using TestControl.Net.BDD.Ioc;
 

namespace TestControl.Net.BDD.Interfaces
{
    public interface ITestNode : IEnumerable, IEnumerator
    {
        Guid ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IPersistanceTreeNode PersistanceNode { get; }
        ITestNode ParentNode { get; }
        ITestNode PreviousNode { get; }
        ITestNode NextNode { get; }
        int LevelNo { get; set; }
        int Count { get; }
        ITestNode this[int idx] { get; }
        bool IsRunnable { get; set; }
        IServices Services { get; set; }
        ITestScriptProvider ScriptProvider { get; set; }
        IFixtureResult TestResult { get; }
        ITestNode AddChildNode(ITestNode node);
        void Save();
        void Load();
        void Load(ITestScriptProvider scriptProvider);
        ITreeChildEnumerator GetTreeChildEnumerator();
        void Clear();
    }
}