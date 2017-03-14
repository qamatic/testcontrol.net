// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcommander.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Net.BDD.Scanners;
using TestControl.Runner.Redefine;

namespace TestControl.Runner.Redefine
{
    [InstanceBehaviour(typeof (ITestReportWriter), InstanceBehaviourType.AlwaysCreate)]
    public class RedefineHtmlReportWriter : ITestReportWriter
    {
        private readonly string _outputDir;
        private readonly string _runPath;
        private readonly IList<string> _testFileLinks = new List<string>();
        private readonly IList<ITestNode> _testList;
        private IUtilService _utilService;
        private string _resultOutDir;
        private string _runTestName;
        private int _runningNumber;

        public RedefineHtmlReportWriter(IList<ITestNode> testList, string outputDir, string runPath)
        {
            _testList = testList;
            _outputDir = outputDir;
            _runPath = runPath;
        }

        public IServices Services { get; set; }

        private string RunTestName
        {
            get
            {
                if (string.IsNullOrEmpty(_runTestName))
                    _runTestName = _runPath.Substring(_runPath.LastIndexOf('\\') + 1);
                return _runTestName;
            }
        }

        private IUtilService FitUtilityService
        {
            get
            {
                if (_utilService == null)
                    _utilService = Services.Get<IUtilService>();
                return _utilService;
            }
        }

        #region ITestReportWriter Members

        public string OutputDir
        {
            get
            {
                if (string.IsNullOrEmpty(_resultOutDir))
                {
                    _resultOutDir = _outputDir + "\\" + RunTestName;
                    if (!Directory.Exists(_resultOutDir))
                    {
                        Directory.CreateDirectory(_resultOutDir);
                    }
                }

                return _resultOutDir;
            }
        }

        public void GenerateReport()
        {
            if (!CanGenerateReport)
                return;

            GenerateHtmlTestReport();
            GenerateIndexFile();
        }

        public bool CanGenerateReport
        {
            get { return (OutputDir != string.Empty); }
        }

        #endregion

        private void WriteSummaryTestResult(StreamWriter writer, IEnumerable<ITestNode> testList)
        {
            Console.WriteLine();
            var totalResult = Services.Get<IFixtureResult>();
            var totalTestTimeSpan = new TimeSpan();
            foreach (ITestNode node in testList)
            {
                if (totalResult.StartTime == null)
                    totalResult.StartTime = node.TestResult.StartTime;
                totalResult.Right += node.TestResult.Right;
                totalResult.Wrong += node.TestResult.Wrong;
                totalResult.Exceptions += node.TestResult.Exceptions;
                TimeSpan timeSpanToAdd = node.TestResult.GetTimeSpan();
                totalTestTimeSpan = totalTestTimeSpan.Add(timeSpanToAdd);
            }

            totalResult.EndTime = totalResult.StartTime + totalTestTimeSpan;
            string status = "pass";
            if ((totalResult.Wrong + totalResult.Exceptions) != 0)
                status = "fail";
            writer.Write(string.Format("<div style=\"border:1px solid black;padding:5px \" class=\"{0}\">", status));
            writer.WriteLine("<h3>Summary:</h3>");
            writer.WriteLine(FitUtilityService.GetResultHeader(totalResult));

            writer.WriteLine();
            writer.WriteLine(" Total of {0} Test Case(s) run.", testList.Count());

            writer.Write("</div>");
        }


        private void GenerateIndexFile()
        {
            using (var writer = new StreamWriter(OutputDir + "\\index.html"))
            {
                var sb = new StringBuilder();
                WriteSummaryTestResult(writer, _testList);
                foreach (string aLink in _testFileLinks)
                {
                    sb.Append("<li>");
                    sb.Append(aLink);
                    sb.Append("</li>");
                }

                string bodyText = sb.ToString();
                writer.Write(FitUtilityService.GetHtmlTestResultBody("Test Result", FitUtilityService.GetSomeCSS(),
                                                                     _runPath, bodyText));
            }
        }

        private void GenerateHtmlTestReport()
        {
            _testFileLinks.Clear();
            foreach (ITestCase testCase in _testList)
            {
                GenerateHtmlTestReport(testCase);
            }
        }

        private void GenerateHtmlTestReport(ITestCase testCase)
        {
            string fileLink = GetOutputFile(testCase);
            string status = (testCase.TestResult.Pass) ? "pass" : "fail";
            string parentPath = string.Empty;
            TreeChildEnumerator.FindParentPath(testCase, ref parentPath);
            parentPath = parentPath.Substring(parentPath.IndexOf('\\') + 1);
            string testReportName = _runningNumber + ". " + testCase.Name;
            _testFileLinks.Add(
                string.Format("<a class=\"{2}\" href=\"{0}\">{1}</a> <span style=\"padding-left:3px\">( {3} )</span>",
                              Path.GetFileName(fileLink), testReportName,
                              status, parentPath));
            using (var writer = new StreamWriter(fileLink))
            {
                FitUtilityService.WriteHtmlResult(testCase, writer);
            }
        }


        private string GetOutputFile(ITestNode node)
        {
            if (Directory.Exists(OutputDir))
                Directory.CreateDirectory(OutputDir);
            _runningNumber++;
            string runningFileName = _runningNumber.ToString();
            runningFileName = runningFileName.PadLeft(6, '0');
            string status = (node.TestResult.Pass) ? "Pass" : "Fail";
            return OutputDir + "\\" + runningFileName + "-" + status + ".html";
        }
    }
}