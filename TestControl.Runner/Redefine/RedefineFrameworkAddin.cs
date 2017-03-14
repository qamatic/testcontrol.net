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
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Runner.Redefine;

namespace TestControl.Runner.Redefine
{
    public class RedefineFrameworkAddin : ITestFrameworkAddin
    {
        private readonly IList<ITestNode> _runItems = new List<ITestNode>();
        private bool _exitOnTestFail;
        private ITestEngine _testEngine;
        private IUtilService _utilService;
        private string _outputDir = string.Empty;
        private string _runPath;
        private ITestScriptProvider _scriptProvider;
        private IServiceContext _serviceContext;

        private IUtilService UtilityService
        {
            get
            {
                if (_utilService == null)
                    _utilService = _serviceContext.Services.Get<IUtilService>();
                return _utilService;
            }
        }

        #region ITestFrameworkAddin Members

        public string AddinName
        {
            get { return "Redefine BDD Runner Add-In 1.0"; }
        }

        public string CommandLineOptionName
        {
            get { return "redefine"; }
        }

        public bool ExitOnTestFail
        {
            get { return _exitOnTestFail; }
        }

        public virtual bool ValidateAddinParameters(string param, ref string validationResult)
        {
            param = param?? "run=TESTROOT";//hard code for now
            if (string.IsNullOrEmpty(param) || (param.Split('=').Length < 2))
            {
                validationResult = "not enough parameters";
                return false;
            }

            string[] allParameters = param.Split(';');
            bool validRunParam = false;
            foreach (string parameter in allParameters)
            {
                string[] splits = parameter.Split('=');
                if (splits[0].ToLower().Equals("run"))
                {
                    if ((!splits[0].ToLower().Equals("run")) || string.IsNullOrEmpty(splits[1]))
                    {
                        validationResult = "invalid switch / parameters";
                        return false;
                    }
                    _runPath = splits[1];
                    var utilSvc = _serviceContext.Services.Get<IUtilService>();
                    if (!utilSvc.IsArgumentsValid(_runPath))
                    {
                        validationResult = _runPath + " not a valid folder path";
                        return false;
                    }
                    validRunParam = true;
                }
                else if (splits[0].ToLower().Equals("outputdir"))
                {
                    _outputDir = splits[1];
                }
                else if (splits[0].ToLower().Equals("exitontestfail"))
                {
                    Boolean.TryParse(splits[1], out _exitOnTestFail);
                }
            }
            return validRunParam;
        }

        public IList<ITestNode> TestedItems
        {
            get { return _runItems; }
        }

        public virtual ITestReportWriter GetReportWriter()
        {
            return _serviceContext.Services.Get<ITestReportWriter>(TestedItems, _outputDir, _runPath);
        }

        public virtual void RegisterServices(IServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
            serviceContext.Services.AddService<ITestEngine>(typeof (RedefineTestEngine));
            serviceContext.Services.AddService<IUtilService>(typeof (UtilService));
            serviceContext.Services.AddService<IRedefineTestScriptProvider>(typeof (RedefineTestScriptProvider));
            serviceContext.Services.AddService<ISuite>(typeof (RedefineSuite));
            serviceContext.Services.AddService<ITestCase>(typeof (RedefineTestPage));
            //serviceContext.Services.AddService<ITestLink>(typeof (FitIncludePage));
            //serviceContext.Services.AddService<IFixtureTable>(typeof (FitFixtureTable));
            //serviceContext.Services.AddService<IFixtureTextData>(typeof (FitFixtureTextData));
            serviceContext.Services.AddService<ITestReportWriter>(typeof (RedefineHtmlReportWriter));
            serviceContext.Services.AddService<IFixtureResult>(typeof (RedefineFixtureResult));
            serviceContext.Services.AddService<ITestFrameworkAddin>(this);
        }

        public ITestScriptProvider GetScriptProvider()
        {
            if (_scriptProvider == null)
                _scriptProvider = _serviceContext.Services.Get<IRedefineTestScriptProvider>("A Test Session", _runPath);
            return _scriptProvider;
        }

        public ITestEngine GetTestEngine()
        {
            if (_testEngine == null)
                _testEngine = _serviceContext.Services.Get<ITestEngine>();

            return _testEngine;
        }

        public virtual void Run()
        {
            _runItems.Clear();
            IList<ITestNode> testableItems = UtilityService.GetRunnableTest(_runPath, GetScriptProvider());
            if (testableItems.Count != 0)
            {
                foreach (ITestNode testableItem in testableItems)
                {
                    _runItems.Add(testableItem);
                    GetTestEngine().Run(testableItem);
                }
            }
        }

        #endregion
    }
}