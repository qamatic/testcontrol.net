// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Runner.Redefine;

namespace TestControl.Runner
{
    internal class Program
    {
        private const string APP_VERSION = "TestControl.Runner 1.0";
        private static bool _exitOnFail;
        private static DateTime _launchTime;
        private static IServiceContext _svcContext;


        private static void Main(string[] args)
        {
            //Console.ReadKey();
            try
            {
                _launchTime = DateTime.Now;
                _svcContext = new ServiceContext();
                CheckForHelp(args);
                string addinName = GetAddinName(args);
                ITestFrameworkAddin addIn = (addinName== null)?new RedefineFrameworkAddin() : TestAddinManager.GetAddIn(ConfigurationManager.AppSettings[addinName]);
                addIn.RegisterServices(_svcContext);

                ValidateAddInParameters(args, addIn);
                _exitOnFail = addIn.ExitOnTestFail;

                ITestScriptProvider scriptProvider = addIn.GetScriptProvider();
                WriteHeader();
                scriptProvider.OnAfterCreateNode += OnAfterCreateNode;
                scriptProvider.OnNotifyLoadErrors += OnLoadErrors;
                scriptProvider.OnErrorProcessingNodes += OnErrorProcessingNodes;


                scriptProvider.LoadScripts();

                Console.WriteLine();

                WriteTestCount(scriptProvider);
                Console.WriteLine();

                Console.WriteLine("Runnning test..");
                Console.WriteLine("Add-in loaded: " + addIn.AddinName);
                Console.WriteLine();
                addIn.GetTestEngine().OnTestComplete += OnTestComplete;
                addIn.GetTestEngine().OnNotifyTestRunErrors += OnNotifyTestRunErrors;
                addIn.Run();

                ITestReportWriter reportWriter = addIn.GetReportWriter();
                GenerateReport(reportWriter);
                Console.WriteLine();
                WriteSummaryTestResult(addIn.TestedItems);
            }
            finally
            {
                ConsoleText.SetColor(ConsoleText.DEFAULT_COLOR);
            }
        }

        private static void OnNotifyTestRunErrors(string errortext)
        {
            try
            {
                ConsoleText.SetColor(ConsoleText.ColorCodes.Yellow);
                Console.Write(errortext);
                if (_exitOnFail)
                {
                    Console.WriteLine("test run stopped on failure.");
                    ConsoleText.SetColor(ConsoleText.DEFAULT_COLOR);
                    Environment.Exit(11);
                }
            }
            finally
            {
                ConsoleText.SetColor(ConsoleText.DEFAULT_COLOR);
            }
        }

        private static void GenerateReport(ITestReportWriter reportWriter)
        {
            if (reportWriter.CanGenerateReport)
            {
                Console.WriteLine("Generating report..");
                reportWriter.GenerateReport();
            }
        }

        private static void OnTestComplete(ITestNode node)
        {
            WriteTestResult(node, node.TestResult);
            Console.WriteLine();
            if ((_exitOnFail) && (node.TestResult.Wrong + node.TestResult.Exceptions != 0))
            {
                var addIn = node.Services.Get<ITestFrameworkAddin>();
                GenerateReport(addIn.GetReportWriter());
                Console.WriteLine("test run stopped on failure.");
                Environment.Exit(11);
            }
        }

        private static void WriteSummaryTestResult(IEnumerable<ITestNode> testList)
        {
            Console.WriteLine();
            var totalResult = _svcContext.Services.Get<IFixtureResult>();
            var totalTestTimeSpan = new TimeSpan();
            foreach (ITestNode node in testList)
            {
                totalResult.Right += node.TestResult.Right;
                totalResult.Wrong += node.TestResult.Wrong;
                totalResult.Exceptions += node.TestResult.Exceptions;
                TimeSpan timeSpanToAdd = node.TestResult.GetTimeSpan();
                totalTestTimeSpan = totalTestTimeSpan.Add(timeSpanToAdd);
            }
            totalResult.StartTime = _launchTime;
            totalResult.EndTime = _launchTime + totalTestTimeSpan;
            Console.WriteLine("Summary:");
            WriteTestResult(null, totalResult);
            Console.WriteLine();
            Console.WriteLine(" Total of {0} Test Case(s) run.", testList.Count());
            TimeSpan launchToEndTimeSpan = DateTime.Now - _launchTime;
            Console.WriteLine(" Total time taken : {0}sec", launchToEndTimeSpan.TotalSeconds);
            Console.WriteLine();
        }

        private static void WriteTestResult(ITestNode testNode, IFixtureResult result)
        {
            ConsoleText.ColorCodes cColor = ConsoleText.ColorCodes.Red;
            string status = " FAIL! ";
            if (result.Right == (result.Right + result.Wrong + result.Exceptions))
            {
                cColor = ConsoleText.ColorCodes.Green;
                status = " PASS! ";
            }
            else if ((result.Exceptions != 0) && (result.Wrong == 0))
            {
                cColor = ConsoleText.ColorCodes.Yellow;
                status = " FAIL! ";
            }
            else
            {
                cColor = ConsoleText.ColorCodes.Red;
                status = " FAIL! ";
            }

            ConsoleText.SetColor(cColor);
            Console.Write(status);
            ConsoleText.SetColor(ConsoleText.DEFAULT_COLOR);
            if (testNode != null)
                Console.Write(testNode.Name + "\t");
            Console.Write("Assertions:\t Pass:{0} \t Fail:{1} \t Exception:{2} \t {3}", result.Right, result.Wrong,
                          result.Exceptions, result.GetTimeSpan().TotalSeconds + "sec");
        }

        private static void OnErrorProcessingNodes(ITestNode node)
        {
            if (node is ITestLink)
            {
                ConsoleText.SetColor(ConsoleText.ColorCodes.Yellow);
                Console.WriteLine();
                Console.Write("Warning: processing include scripts: [{0}]:[{1}]", node.ParentNode, node);
            }
            else
            {
                ConsoleText.SetColor(ConsoleText.ColorCodes.Red);
                Console.WriteLine();
                Console.Write("Unidentified error while processing node " + node);
            }
            ConsoleText.SetColor(ConsoleText.DEFAULT_COLOR);
        }

        private static void OnLoadErrors(string errortext)
        {
            Console.Write(errortext);
        }

        private static void ValidateAddInParameters(IEnumerable<string> args, ITestFrameworkAddin addIn)
        {
            string validationResult = string.Empty;
            string addinParams = ExtractParam(args, "/addin-params:");
            if (!addIn.ValidateAddinParameters(addinParams, ref validationResult))
            {
                Console.WriteLine();
                Console.WriteLine(validationResult);
                Console.WriteLine();
                Console.WriteLine("for help: tcrunner.exe /?");
                Environment.Exit(10);
            }
        }

        private static string GetAddinName(IEnumerable<string> args)
        {
            string addinName = ExtractParam(args, "/addin:");
            if (string.IsNullOrEmpty(addinName))
                return null;
            return addinName;
        }

        private static void CheckForHelp(string[] args)
        {
            if ((args.Count() > 1) && args[0] == "/?")
            {
                WriteUsage();
                Environment.Exit(0);
            }
        }

        private static string ExtractParam(IEnumerable<string> strings, string paramToExtract)
        {
            foreach (string aArg in strings)
            {
                if (aArg.ToLower().StartsWith(paramToExtract))
                {
                    return aArg.Substring(paramToExtract.Length);
                }
            }
            return null;
        }

        private static void OnAfterCreateNode(ITestNode node)
        {
            if (node.GetType().GetInterfaces().Contains((typeof (ITestCase))))
                Console.Write(".");
        }

        private static void WriteTestCount(ITestScriptProvider scriptProvider)
        {
            var list = new List<ITestNode>();
            foreach (object item in scriptProvider.LookupNodes.Values)
            {
                list.Add(item as ITestNode);
            }
            int totalSuites =
                list.Count(
                    x => x.IsRunnable && x.GetType().GetInterfaces().Contains((typeof (ISuite))));
            int totalTestCases =
                list.Count(
                    x => x.IsRunnable && x.GetType().GetInterfaces().Contains((typeof (ITestCase))));
            Console.WriteLine(totalSuites + " runnable suites with " + totalTestCases +
                              " test cases found.");
        }

        private static void WriteUsage()
        {
            WriteHeader();
            Console.WriteLine("Syntax:                      TCRunner.exe [addin-name] [addin-parameters]");
            Console.WriteLine();
            Console.WriteLine("Description:                 Loads the specified test framework add-in and ");
            Console.WriteLine("                             runs the suite / test case");
            Console.WriteLine();
            Console.WriteLine("Switches:");
            Console.WriteLine("  /addin:<name>              Name of the add-in to be used for running ");
            Console.WriteLine("                             the test cases. Use TestControl.Runner.exe.config");
            Console.WriteLine("                             for configuring add-ins.");
            Console.WriteLine("                             example:  /addin:fitnesse");
            Console.WriteLine();
            Console.WriteLine("  /addin-params:<options>    neccessary parameters that are required by ");
            Console.WriteLine("                             the add-in to run the test cases. Check the");
            Console.WriteLine("                             add-in documentation for the parameters");
            Console.WriteLine("                             example:");
            Console.WriteLine("                             /addin-params:run=FitnesseRoot/ATestSuite");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine(
                "  TestControl.Runner /addin:fitnesse /addin-params:run=FitnesseRoot/MyTestSuite;outputdir=c:\\testresult;ExitOnTestFail=true");
            Console.WriteLine("  TestControl.Runner /addin:fitnesse /addin-params:run=FitnesseRoot/MySuite/ATest");
            Console.WriteLine(
                "  TestControl.Runner /addin:fitnesse /addin-params:run=c:\\fitnesse\\FitnesseRoot\\ExampleSuite");
            Environment.Exit(10);
        }

        private static void WriteHeader()
        {
            Console.WriteLine(APP_VERSION);
            Console.WriteLine("A Command Line Tool for running test cases");
            Console.WriteLine();
        }
    }
}