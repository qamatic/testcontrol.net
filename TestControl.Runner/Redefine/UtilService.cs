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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;

namespace TestControl.Runner.Redefine
{
    [InstanceBehaviour(typeof (IUtilService), InstanceBehaviourType.Singleton)]
    public class UtilService : IUtilService
    {
        #region IUtilService Members

        public string GetTableStartTag()
        {
            return "<table border=\"1\" cellspacing=\"0\"><tbody>";
        }

        public string GetTableEndTag()
        {
            return "</tbody></table>";
        }


        public string GetRowTag(string rowStr)
        {
            return "<tr>" + rowStr + "</tr>";
        }

        public string GetColumnTag(string colstr, int spanValue)
        {
            if (spanValue == 0)
                return "<td>" + colstr + "</td>";

            return "<td colspan=\"" + spanValue + "\">" + colstr + "</td>";
        }

        public string ListToString(IList<string> list)
        {
            var sb = new StringBuilder();
            foreach (string st in list)
            {
                sb.AppendLine(st);
            }
            return sb.ToString();
        }

        public string[] GetColumns(string text)
        {
            if (text.Length <= 0)
                return new string[0];
            text = text.Replace("!|", "");
            if (text[0].Equals('|'))
                text = text.Remove(0, 1);
            if (text.Length <= 0)
                return new string[0];
            if (text[text.Length - 1].Equals('|'))
                text = text.Remove(text.Length - 1);
            if (text.Length <= 0)
                return new string[0];
            string[] result = text.Split('|');
            for (int idx = 0; idx < result.GetLength(0); idx++)
            {
                result[idx] = result[idx].Trim();
            }
            return result;
        }

        public StringCollection ReadFromFile(string fileName)
        {
            var strCollection = new StringCollection();
            if (!File.Exists(fileName))
                return strCollection;
            TextReader rdr = new StreamReader(fileName);

            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                strCollection.Add(line);
            }
            rdr.Close();
            return strCollection;
        }

        public void ReadFromFile(string fileName, IList<string> list)
        {
            ReadFileRecursive(fileName, ref list);
        }

     

        public string FixPath(string fpath)
        {
            if ((fpath.EndsWith(@"\")) || (fpath.EndsWith(@"/")))
                return fpath;
            return fpath + "\\";
        }

        public bool IsValidFolder(string dir)
        {
            return Directory.Exists(dir);
        }

        public string GetTestRootPath(string testPath)
        {
            string path = Path.GetFullPath(testPath).ToLower();
             
            return path;
        }

        public string GetParentSuitePath(string testPath)
        {
            string path = Path.GetFullPath(testPath).ToLower();
            string rootpath = GetTestRootPath(testPath);
            string suitePath = path.Replace(rootpath, "");
            string[] splits = suitePath.Split('\\');


            return rootpath + "\\" + splits[0];
        }

        public bool IsArgumentsValid(string testPath)
        {
            try
            {
                string testRootpath = GetTestRootPath(testPath);
                string fullTestPath = Path.GetFullPath(testPath);
                return Directory.Exists(fullTestPath) && Directory.Exists(testRootpath);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool IsTestDir(string dir)
        {

            return true;
        }

        public bool IsSuiteDir(string dir)
        {
            var dirInfo = new DirectoryInfo(dir);
            bool isHidden = (dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            if (!isHidden)
            {
                //yet to implement
            
            }
            return false;
        }

        public bool IsRunnable(string dir)
        {
            var dirInfo = new DirectoryInfo(dir);
            bool isHidden = (dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            if (!isHidden)
            { 
                //yet to implement
            }
            return true;
        }

        public bool IsScenarioStart(string line)
        {
            return (line.Trim().StartsWith("!|"));
        }

        public string FormatFixtureTableTitle(string line)
        {
            string[] cols = GetColumns(line);
            var sb = new StringBuilder();
            for (int i = 0; i < cols.Length; i++)
            {
                if (i == 0)
                    sb.Append(cols[i]);
                else
                    sb.Append(" <" + cols[i] + ">");
            }

            return sb.ToString();
        }

        public void LoadData(IFixtureData ifixtureData, string[] fixture)
        {
            foreach (string st in fixture)
                ifixtureData.Add(GetColumns(st));
        }

        public void GetAsHtmlTable(IFixtureData list, ref StringBuilder result)
        {
            int maxCol = list.MaxColumn;

            for (int rowIdx = 0; rowIdx < list.Count; rowIdx++)
            {
                string[] cols = list[rowIdx];

                var sb = new StringBuilder();
                for (int colIdx = 0; colIdx < cols.GetLength(0); colIdx++)
                {
                    string col = cols[colIdx];
                    if (colIdx == cols.GetLength(0) - 1)
                    {
                        int elapsedCnt = maxCol - cols.GetLength(0) + 1;
                        if (cols.GetLength(0) == 1)
                            elapsedCnt = maxCol;
                        sb.Append(GetColumnTag(col, elapsedCnt));
                    }
                    else
                        sb.Append(GetColumnTag(col, 0));
                }


                if (rowIdx == 0)
                    result.Append(GetTableStartTag() + GetRowTag(sb.ToString()));
                else if (rowIdx == list.Count - 1)
                    result.Append(GetRowTag(sb.ToString()) + GetTableEndTag());
                else
                    result.Append(GetRowTag(sb.ToString()));
            }
            if (list.Count == 1)
                result.Append(GetTableEndTag());
        }


        public IList<ITestNode> ToList(Hashtable table)
        {
            var list = new List<ITestNode>();
            foreach (object item in table.Values)
            {
                list.Add(item as ITestNode);
            }
            return list;
        }


        public void ResolveIncludes(ISuite rootSuite, Hashtable lookupTable, NotifyEventDelegate onErrorIncluding)
        {
            foreach (ITestNode testNode in lookupTable.Values)
            {
                if (!testNode.IsRunnable)
                    continue;
                foreach (ITestNode childNode in testNode)
                {
                    var linkNode = childNode as ITestLink;
                    if (linkNode != null)
                    {
                        string searchNodeName = linkNode.ContentText;
                        searchNodeName = searchNodeName.Replace("/", "\\");
                        if (searchNodeName.ToLower().Contains("!include"))
                        {
                            searchNodeName = searchNodeName.Substring(9).Trim();
                        }
                        string rootSuitePath = rootSuite.Name + "\\";
                        if (searchNodeName.StartsWith("."))
                            searchNodeName = searchNodeName.Replace(".\\", rootSuitePath);
                        else
                        {
                            if (!searchNodeName.ToLower().StartsWith(rootSuitePath.ToLower()))
                                searchNodeName = rootSuitePath + searchNodeName;
                        }
                        searchNodeName = searchNodeName.Replace(".", "\\");
                        searchNodeName = searchNodeName.Replace("/", "\\");

                        var includeNode = lookupTable[searchNodeName.ToUpper()] as ITestNode;
                        bool containsRunnableNode = false;
                        ContainsRunnableItems(includeNode, ref containsRunnableNode);
                        if ((includeNode != null) && (!includeNode.IsRunnable) && (!containsRunnableNode))
                        {
                            linkNode.AddChildNode(includeNode);
                        }
                        else if (onErrorIncluding != null)
                            onErrorIncluding(linkNode);
                    }
                }
            }
        }

        public string[] GetAsmPaths(IList<string> varList)
        {
            string[] asmList = varList.Where(x => x.StartsWith("path=")).ToArray();
            var result = new string[asmList.Count()];
            for (int i = 0; i < asmList.Count(); i++)
                result[i] = asmList[i].Split('=')[1].Trim();

            return result;
        }

        public void GetVariables(ITestNode node, ref IList<string> varList)
        {
            if (node == null)
                return;
            foreach (ITestNode selectedNode in node)
            {
                if ((selectedNode as IFixtureTextData) != null)
                {
                    var fixtureTextdata = selectedNode as IFixtureTextData;
                    string[] lines = fixtureTextdata.ContentText.Split('\r');
                    foreach (string s in lines)
                    {
                        string line = s;
                        if (line.ToLower().Contains("!path"))
                        {
                            line = line.Replace("\n", "");
                            line = line.Replace("\r", "").Trim();
                            string valueToAdd = "path=" + line.Substring(6).Trim();
                            if (varList.IndexOf(valueToAdd) == -1)
                                varList.Add(valueToAdd);
                        }
                        if (line.ToLower().Contains("!define"))
                        {
                            line = line.Replace("\n", "");
                            line = line.Replace("\r", "");
                            line = line.Replace("{", "");
                            line = line.Replace("}", "");
                            line = line.Substring(8).Trim();
                            int firstSpace = line.IndexOf(' ');
                            string firstPart = line.Substring(0, firstSpace).Trim();
                            string secondPart = line.Replace(firstPart, "").Trim();
                            string valueToAdd = firstPart + "=" + secondPart;
                            if (varList.IndexOf(valueToAdd) == -1)
                                varList.Add(valueToAdd);
                        }
                    }
                }
            }
            GetVariables(node.ParentNode, ref varList);
        }

        public string DoWikiToHtml(string inputStr)
        {
            var sb = new StringBuilder();
            using (var reader = new StringReader(inputStr))
            {
                string wikiLine;
                while ((wikiLine = reader.ReadLine()) != null)
                {
                    string line = wikiLine.ToLower().Trim();
                    if ((line.StartsWith("!path")) ||
                        (line.StartsWith("!define"))
                        )
                        continue;
                    if ((line.StartsWith("!1")) ||
                        (line.StartsWith("!2")) ||
                        (line.StartsWith("!3"))
                        )
                    {
                        int headerNo = Convert.ToInt32(line.Substring(1, 1));
                        sb.AppendLine(GetHtmlTaggedString("h" + headerNo, wikiLine.Substring(2)));
                    }
                    else
                        sb.AppendLine(wikiLine);
                }
            }


            return sb.ToString();
        }


        public string GetSomeCSS()
        {
            var builder = new StringBuilder();

            builder.Append(".pass{background-color: #AAFFAA;}");
            builder.Append(".fail{background-color: #FFAAAA;}");
            builder.Append(".error{background-color: #FFFFAA;}");
            builder.Append(".ignore{background-color: #CCCCCC;}");
            builder.Append(".tc_stacktrace{font-size: 0.7em;}");
            builder.Append(".tc_label{font-style: italic;	color: #C08080;}");
            builder.Append(".tc_grey{color: #808080;}");

            return builder.ToString();
        }

        public string GetHtmlTestResultBody(params object[] parameters)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("    <title>{0}</title>");
            builder.Append("    <style type=\"text/css\">");
            builder.Append("        {1}</style>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("    <h1>{2}</h1>{3}");
            builder.Append("    <div style=\"padding-top: 40px; font-size:10px\">");
       
            builder.Append("    </div>");
            builder.Append("</body>");
            builder.Append("</html>");
            for (int i = 0; i < parameters.Length; i++)
                builder.Replace("{" + i + "}", parameters[i].ToString());

            return builder.ToString();
        }


        public string GetResultHeader(IFixtureResult result)
        {
            string assertionText = string.Format("Assertions:\t Pass:{0} \t Fail:{1} \t Exception:{2} \t {3}",
                                                 result.Right,
                                                 result.Wrong,
                                                 result.Exceptions, result.GetTimeSpan().TotalSeconds + "sec");
            assertionText = assertionText.Replace("\t", "&nbsp;&nbsp;");
            string status = "pass";
            if ((result.Wrong + result.Exceptions) != 0)
                status = "fail";
            return
                string.Format(
                    "<div style=\"border:1px solid silver;padding:5px \" class=\"{0}\"><strong>Assertions:</strong> {1}</div>",
                    status, assertionText);
        }


        public void WriteHtmlResult(ITestCase testCase, TextWriter writer)
        {
            string bodyText = (testCase as IResultDoc).GetResultDocument();
            bodyText = GetResultHeader(testCase.TestResult) + bodyText;
            writer.Write(GetHtmlTestResultBody(testCase.Name, GetSomeCSS(), testCase.Name, bodyText));
        }


        public IList<ITestNode> GetRunnableTest(string testName, ITestScriptProvider scriptProvider)
        {
            var runList = new List<ITestNode>();
        
            Hashtable lookupNodes = scriptProvider.LookupNodes;
            foreach (string key in lookupNodes.Keys)
            {
              
            }
            return runList;
        }

        #endregion

        private static void ContainsRunnableItems(ITestNode node, ref bool result)
        {
            if ((node == null) || result)
                return;

            foreach (ITestNode selectedNode in node)
            {
                if ((selectedNode as ISuite) != null)
                {
                    result |= (selectedNode as ISuite).IsRunnable;
                }
                else if ((selectedNode as ITestCase) != null)
                {
                    result = true;
                }

                ContainsRunnableItems(selectedNode, ref result);
            }
        }

        private static void ReadFileRecursive(string file, ref IList<string> list)
        {
            if (!File.Exists(file))
                return;

            TextReader rdr = new StreamReader(file);

            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                list.Add(line);
            }

            rdr.Close();
        }

        private string GetHtmlTaggedString(string tag, string value)
        {
            return "<" + tag + ">" + value + "</" + tag + ">";
        }
    }
}