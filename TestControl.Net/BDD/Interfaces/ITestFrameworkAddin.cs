// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections.Generic;
using TestControl.Net.BDD.Ioc;

namespace TestControl.Net.BDD.Interfaces
{
    public interface ITestFrameworkAddin
    {
        string AddinName { get; }
        string CommandLineOptionName { get; }
        IList<ITestNode> TestedItems { get; }
        bool ExitOnTestFail { get; }
        bool ValidateAddinParameters(string param, ref string validationResult);
        void RegisterServices(IServiceContext serviceContext);
        ITestScriptProvider GetScriptProvider();
        ITestEngine GetTestEngine();
        void Run();
        ITestReportWriter GetReportWriter();
    }
}