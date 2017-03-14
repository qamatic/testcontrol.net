// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================



using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;

namespace  TestControl.Net.BDD.Scanners
{
    [InstanceBehaviour(typeof (ISuite), InstanceBehaviourType.AlwaysCreate)]
    public class Suite : TestNode, ISuite
    {
        public Suite(string name)
            : base(name)
        {
        }

        #region ISuite Members

        public virtual ISuite CreateSuite(string suiteName)
        {
            var aNewSuite = Services.Get<ISuite>(suiteName);
            AddSuite(aNewSuite);
            return aNewSuite;
        }

        public virtual ITestCase CreateTestCase(string testCaseName)
        {
            var aTestCase = Services.Get<ITestCase>(testCaseName);
            AddTestCase(aTestCase);
            return aTestCase;
        }

        #endregion

        protected ISuite AddSuite(ISuite suite)
        {
            AddChildNode(suite);
            return suite;
        }

        protected ITestCase AddTestCase(ITestCase testcase)
        {
            AddChildNode(testcase);
            return testcase;
        }
    }
}