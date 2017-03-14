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
using System.IO;
using System.Text;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
using TestControl.Runner.Redefine;

namespace TestControl.Runner.Redefine
{
    [InstanceBehaviour(typeof (ITestEngine), InstanceBehaviourType.AlwaysCreate)]
    public class RedefineTestEngine : ITestEngine
    {
        private IUtilService _utilService;

        public IServices Services { get; set; }

        private IUtilService FitUtilityService
        {
            get
            {
                if (_utilService == null)
                    _utilService = Services.Get<IUtilService>();
                return _utilService;
            }
        }

        #region ITestEngine Members

        public NotifyErrors OnNotifyTestRunErrors { get; set; }

        public NotifyEventDelegate OnTestComplete { get; set; }

        public void Run(ITestNode node)
        {
             
        }

        #endregion
        
    }
}