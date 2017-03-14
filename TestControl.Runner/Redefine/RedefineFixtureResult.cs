// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcommander.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================



using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Net.BDD.Scanners;

namespace TestControl.Runner.Redefine
{
    public interface IRedfineResult : IFixtureResult
    {
        string ResultText { set; get; }
    }

    [InstanceBehaviour(typeof (IFixtureResult), InstanceBehaviourType.AlwaysCreate)]
    public class RedefineFixtureResult : FixtureResult, IRedfineResult
    {
        #region IRedfineResult Members

        public string ResultText { get; set; }

        #endregion
    }
}