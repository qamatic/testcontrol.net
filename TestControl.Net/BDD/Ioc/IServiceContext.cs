// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
 
namespace TestControl.Net.BDD.Ioc
{
    public interface IServices
    {
        void AddService<T>(object svcObject);

        void AddService<T>(Type t);

        T CreateInstance<T>(params object[] parameters);

        object CreateInstance(string fullyQualifiedTypeName, params object[] parameters);

        T Get<T>(params object[] parameters);
    }

    public interface IServiceContext
    {
        IServices Services
        {
            get;
        }
    }
}
