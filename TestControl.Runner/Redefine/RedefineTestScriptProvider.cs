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
using System.IO;
using System.Text;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Runner.Redefine;

namespace TestControl.Runner.Redefine
{
    [InstanceBehaviour(typeof (IRedefineTestScriptProvider), InstanceBehaviourType.AlwaysCreate)]
    public class RedefineTestScriptProvider : IRedefineTestScriptProvider
    {
        private readonly IList<ITestListner> _testListners = new List<ITestListner>();
        private readonly string _testPathRelativeToTestRootPath;
        private readonly string _testSessionName = "Root";
        private Hashtable _allNodes = new Hashtable();
        private IUtilService _utilService;
        private ISuite _rootSuite;

        public RedefineTestScriptProvider(string testSessionName,
                                          string testOrSuitePathRelativeToTestRootPath)
        {
            _testSessionName = testSessionName;

            _testPathRelativeToTestRootPath = testOrSuitePathRelativeToTestRootPath;
        }

        public IServices Services { get; set; }

        private IUtilService UtilityService
        {
            get
            {
                if (_utilService == null)
                    _utilService = Services.Get<IUtilService>();
                return _utilService;
            }
        }

        #region ILocalTestScriptProvider Members

        public NotifyEventDelegate OnAfterCreateNode { get; set; }

        public NotifyEventDelegate OnErrorProcessingNodes { get; set; }

        public NotifyErrors OnNotifyLoadErrors { get; set; }

        public ISuite RootSuite
        {
            get
            {
                if (_rootSuite == null)
                    _rootSuite = Services.Get<ISuite>(_testSessionName);

                return _rootSuite;
            }
        }

        public Hashtable LookupNodes
        {
            get { return _allNodes; }
        }

        public void AddListner(ITestListner listner)
        {
            _testListners.Add(listner);
        }

        public void LoadData(ITestNode node)
        {
            var localNode = node as IRedefineTestNodeProperties;
            if (localNode != null)
            {
                Services.Get<IUtilService>().ReadFromFile(
                    localNode.FullFileName, localNode.ContentList);
            }
            Parse(localNode);
        }

        public void LoadScripts()
        {
            string pathToScan = UtilityService.GetParentSuitePath(_testPathRelativeToTestRootPath);

            if (UtilityService.IsTestDir(pathToScan))
            {
                LoadTestCase(new DirectoryInfo(pathToScan), RootSuite);
            }
            else
            {
                ISuite parentSuite = LoadSuite(new DirectoryInfo(pathToScan), RootSuite);
                FolderSearch(pathToScan, parentSuite);
            }
            _allNodes = RootSuite.GetTreeChildEnumerator().ToHashtableByPath();
            RootSuite.GetTreeChildEnumerator().ApplyLevelNumbers();
            UtilityService.ResolveIncludes(RootSuite, _allNodes, OnErrorProcessingNodes);
        }

        #endregion

        private void CreateTextTable(int tableId, ITestNode testNode, ref StringBuilder sb)
        {
            var textDataTable = Services.Get<IFixtureTextData>(tableId + "");
            textDataTable.ContentText = sb.ToString();
            sb.Length = 0;
            NotifyOnAfterCreateNode(textDataTable);
            testNode.AddChildNode(textDataTable);
        }

        public void Parse(IRedefineTestNodeProperties testNode)
        {
            ITestNode aTestNode;
            if ((testNode as ITestCase) != null)
                aTestNode = testNode as ITestCase;
            else if ((testNode as ISuite) != null)
                aTestNode = testNode as ISuite;
            else
                return;


            var utilSvc = Services.Get<IUtilService>();
            var sb = new StringBuilder();
            int tableCnt = 1;
            IFixtureTable fixtureTable = null;
            bool loadingFixtureTable = false;
            foreach (string line in testNode.ContentList)
            {
                if (line.StartsWith("#"))
                    continue;
                if (utilSvc.IsScenarioStart(line))
                {
                    loadingFixtureTable = true;
                    if (sb.Length != 0)
                    {
                        CreateTextTable(tableCnt, aTestNode, ref sb);
                        tableCnt++;
                    }
                    fixtureTable = Services.Get<IFixtureTable>(tableCnt + "");
                    tableCnt++;
                    aTestNode.AddChildNode(fixtureTable);
                }

                if (!line.Contains("|"))
                {
                    if (loadingFixtureTable)
                        NotifyOnAfterCreateNode(fixtureTable);
                    loadingFixtureTable = false;
                    if (line.ToLower().StartsWith("!include"))
                    {
                        var linkItem = Services.Get<ITestLink>(tableCnt + "");
                        tableCnt++;
                        linkItem.ContentText = line;
                        aTestNode.AddChildNode(linkItem);
                        continue;
                    }

                    sb.AppendLine(line);
                    continue;
                }

                if (fixtureTable != null)
                {
                    if (line.Contains("!") && (!line.Contains("!|")))
                        continue;
                    fixtureTable.FixtureData.Add(utilSvc.GetColumns(line));
                }
            }
            if (sb.Length != 0)
            {
                CreateTextTable(tableCnt, aTestNode, ref sb);
            }
        }


        private void NotifyOnAfterCreateNode(ITestNode node)
        {
            if (OnAfterCreateNode != null)
                OnAfterCreateNode(node);
        }


        private void FolderSearch(string sDir, ISuite parentSuite)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                var info = new DirectoryInfo(d);
                if ((info.Attributes & FileAttributes.Hidden) != 0)
                    continue;

                if (UtilityService.IsTestDir(d))
                {
                    LoadTestCase(info, parentSuite);
                }
                else if (UtilityService.IsSuiteDir(d))
                {
                    ISuite suite = LoadSuite(info, parentSuite);
                    FolderSearch(d, suite);
                }
                else
                {
                    if (OnNotifyLoadErrors != null)
                    {
                        OnNotifyLoadErrors("\n" + info.Name +
                                           " error loading script. skipped!");
                    }
                }
            }
        }

        private void LoadTestCase(FileSystemInfo info, ISuite parentSuite)
        {
            ITestCase testcase = parentSuite.CreateTestCase(info.Name);
            (testcase as IRedefineTestNodeProperties).FullFileName = info.FullName + "\\content.txt";
            testcase.Load(this);
            testcase.IsRunnable = true;
            NotifyOnAfterCreateNode(testcase);
        }

        private ISuite LoadSuite(FileSystemInfo info, ISuite parentSuite)
        {
            ISuite suite = parentSuite.CreateSuite(info.Name);
            (suite as IRedefineTestNodeProperties).FullFileName = info.FullName + "\\content.txt";
            suite.Load(this);
            suite.IsRunnable = UtilityService.IsRunnable(info.FullName);
            NotifyOnAfterCreateNode(suite);
            return suite;
        }
    }
}