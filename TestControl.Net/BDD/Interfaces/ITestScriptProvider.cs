// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections;

namespace TestControl.Net.BDD.Interfaces
{
    public interface ITestScriptProvider
    {
        ISuite RootSuite { get; }
        Hashtable LookupNodes { get; }
        NotifyEventDelegate OnAfterCreateNode { get; set; }
        NotifyErrors OnNotifyLoadErrors { get; set; }
        NotifyEventDelegate OnErrorProcessingNodes { get; set; }
        void AddListner(ITestListner listner);
        void LoadData(ITestNode node);
        void LoadScripts();
    }
}