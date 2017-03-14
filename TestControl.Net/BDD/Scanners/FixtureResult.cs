// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;
 

namespace  TestControl.Net.BDD.Scanners
{
    [InstanceBehaviour(typeof (IFixtureResult), InstanceBehaviourType.AlwaysCreate)]
    public class FixtureResult : IFixtureResult
    {
        #region IFixtureResult Members

        public int Right { get; set; }
        public int Wrong { get; set; }
        public int Exceptions { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan GetTimeSpan()
        {
            if ((StartTime == null) || (EndTime == null))
                return new TimeSpan();
            return EndTime - StartTime;
        }

        public bool Pass
        {
            get { return (Wrong + Exceptions) == 0; }
        }

        #endregion
    }
}