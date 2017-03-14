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
    [InstanceBehaviour(typeof (IFixtureTextData), InstanceBehaviourType.AlwaysCreate)]
    public class FixtureTextData : TestNode, IFixtureTextData
    {
        public FixtureTextData(string name)
            : base(name)
        {
        }

        #region IFixtureTextData Members

        public string ContentText { get; set; }

        #endregion
    }
}