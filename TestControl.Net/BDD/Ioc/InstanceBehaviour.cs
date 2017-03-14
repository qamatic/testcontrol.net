using System;
 
// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

 

namespace TestControl.Net.BDD.Ioc
{
    public enum InstanceBehaviourType
    {
        Singleton,
        AlwaysCreate
    }

    public class InstanceBehaviour : Attribute
    {
        private readonly InstanceBehaviourType _instanceBehaviourType;

        public Type ServiceInterfaceType
        {
            get;
            set;
        }

        public InstanceBehaviourType InstanceBehaviourType
        {
            get
            {
                return this._instanceBehaviourType;
            }
        }

        public InstanceBehaviour(Type interfaceType, InstanceBehaviourType btype)
        {
            this._instanceBehaviourType = btype;
            this.ServiceInterfaceType = interfaceType;
        }
    }
}
