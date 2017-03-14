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
    [InstanceBehaviour(typeof (ITestCase), InstanceBehaviourType.AlwaysCreate)]
    public class TestCase : TestNode, ITestCase
    {
        public TestCase(string name)
            : base(name)
        {
        }

        #region ITestCase Members

        public virtual IFixtureTable CreateFixtureTable(string fixtureTableName)
        {
            var fixtureTable = Services.Get<IFixtureTable>(fixtureTableName);
            return AddFixtureTable(fixtureTable);
        }

        #endregion

        protected IFixtureTable AddFixtureTable(IFixtureTable fixturetable)
        {
            AddChildNode(fixturetable);
            return fixturetable;
        }
    }
}