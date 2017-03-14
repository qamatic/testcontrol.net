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
    [InstanceBehaviour(typeof (IFixtureTable), InstanceBehaviourType.AlwaysCreate)]
    public class FixtureTable : TestNode, IFixtureTable
    {
        private readonly IFixtureData _fixtureData;

        public FixtureTable(string name)
            : base(name)
        {
            _fixtureData = new FixtureData(name);
        }

        #region IFixtureTable Members

        public IFixtureData FixtureData
        {
            get { return _fixtureData; }
        }

        #endregion
    }
}