// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections.Generic;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Net.BDD.Scanners;

namespace TestControl.Runner.Redefine
{
    [InstanceBehaviour(typeof (ISuite), InstanceBehaviourType.AlwaysCreate)]
    public class RedefineSuite : Suite, IRedefineTestNodeProperties
    {
        private string _fullFileName;


        public RedefineSuite(string name) : base(name)
        {
            ContentList = new List<string>();
        }

        #region IRedefineTestNodeProperties Members

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
    }
}