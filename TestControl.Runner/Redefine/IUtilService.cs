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
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TestControl.Net.BDD.Interfaces;

namespace TestControl.Runner.Redefine
{
    public interface IUtilService
    {
        string GetTableStartTag();
        string GetTableEndTag();
        string GetRowTag(string rowStr);
        string GetColumnTag(string colstr, int spanValue);
        string[] GetColumns(string text);
        StringCollection ReadFromFile(string fileName);      
        string FixPath(string fpath);
        bool IsTestDir(string dir);
        bool IsSuiteDir(string dir);
        bool IsScenarioStart(string line);
        string FormatFixtureTableTitle(string line);
        void LoadData(IFixtureData ifixtureData, string[] fixture);
        void GetAsHtmlTable(IFixtureData list, ref StringBuilder result);
        void ReadFromFile(string fileName, IList<string> list);
        string ListToString(IList<string> list);
        bool IsRunnable(string dir);
        bool IsValidFolder(string dir);
        string GetTestRootPath(string testPath);
        bool IsArgumentsValid(string testPath);
        string GetParentSuitePath(string testPath);
        void ResolveIncludes(ISuite rootSuite, Hashtable lookupTable, NotifyEventDelegate onErrorIncluding);
        void GetVariables(ITestNode node, ref IList<string> varList);
        IList<ITestNode> ToList(Hashtable table);
        string[] GetAsmPaths(IList<string> varList);
        string DoWikiToHtml(string inputStr);
        string GetSomeCSS();
        string GetHtmlTestResultBody(params object[] parameters);
        string GetResultHeader(IFixtureResult result);
        void WriteHtmlResult(ITestCase testCase, TextWriter writer);
        IList<ITestNode> GetRunnableTest(string testName, ITestScriptProvider scriptProvider);
    }
}