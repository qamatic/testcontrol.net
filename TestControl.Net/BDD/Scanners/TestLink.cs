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
    [InstanceBehaviour(typeof (ITestLink), InstanceBehaviourType.AlwaysCreate)]
    public class TestLink : TestNode, ITestLink
    {
        public TestLink(string name)
            : base(name)
        {
        }

        #region ITestLink Members

        public string ContentText { get; set; }

        #endregion

        public override string ToString()
        {
            return ContentText;
        }
    }
}