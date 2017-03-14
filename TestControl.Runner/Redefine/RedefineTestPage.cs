// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections.Generic;
using System.Text;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Net.BDD.Scanners;

namespace TestControl.Runner.Redefine
{
    [InstanceBehaviour(typeof (ITestCase), InstanceBehaviourType.AlwaysCreate)]
    public class RedefineTestPage : TestCase, IRedefineTestNodeProperties, IRunnableDoc, IResultDoc
    {
        private IUtilService _fitUtilService;
        private string _fullFileName;

        public RedefineTestPage(string name)
            : base(name)
        {
            ContentList = new List<string>();
        }

        private IUtilService FitUtilityService
        {
            get
            {
                if (_fitUtilService == null)
                    _fitUtilService = Services.Get<IUtilService>();
                return _fitUtilService;
            }
        }

        #region IFitLocalTestNodeProperties Members

        public IList<string> ContentList { get; set; }

        public string ContentText
        {
            get { return Services.Get<IUtilService>().ListToString(ContentList); }
        }

        public string FullFileName
        {
            get { return _fullFileName; }
            set
            {
                _fullFileName = value;
                Load();
            }
        }

        public override void Load()
        {
            ContentList.Clear();
            if (ScriptProvider != null)
                ScriptProvider.LoadData(this);
        }

        public override void Load(ITestScriptProvider scriptProvider)
        {
            ScriptProvider = scriptProvider;
            Load();
        }

        #endregion

        #region IFitResultDoc Members

        public string GetResultDocument()
        {
            var result = new StringBuilder();
            foreach (ITestNode fixtureNode in GetTreeChildEnumerator())
            {
                var runnableDoc = fixtureNode as IResultDoc;
                if (runnableDoc != null)
                {
                    result.AppendLine(runnableDoc.GetResultDocument());
                    result.AppendLine("<br \\>");
                }
            }
            return result.ToString();
        }

        #endregion

        #region IFitRunnableDoc Members

        public string GetTestDocument()
        {
            var result = new StringBuilder();
            foreach (ITestNode fixtureNode in GetTreeChildEnumerator())
            {
                var runnableDoc = fixtureNode as IRunnableDoc;
                if (runnableDoc != null)
                {
                    result.AppendLine(runnableDoc.GetTestDocument());
                    result.AppendLine("<br>");
                }
            }
            IList<string> vars = new List<string>();
            FitUtilityService.GetVariables(this, ref vars);

            foreach (string defineVar in vars)
            {
                if (!defineVar.ToLower().StartsWith("path"))
                {
                    string[] splits = defineVar.Split('=');
                    string searchPart = "${" + splits[0] + "}";
                    string replacePart = splits[1];
                    result.Replace(searchPart, replacePart);
                }
            }
            return result.ToString();
        }

        #endregion
    }
}