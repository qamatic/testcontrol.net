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
using System.Linq;
using System.Reflection;
 

namespace TestControl.Net.BDD.Ioc
{
    public class Services : IServices
    {
        private readonly Dictionary<Type, object> _svcInstances = new Dictionary<Type, object>();

        private readonly Dictionary<Type, Type> _svcTypes = new Dictionary<Type, Type>();

        public void AddService<T>(Type t)
        {
            IEnumerable<KeyValuePair<Type, Type>> source = from x in this._svcTypes
                                                           where x.Key == typeof(T)
                                                           select x;
            if (source.Count<KeyValuePair<Type, Type>>() != 0)
            {
                this._svcTypes[typeof(T)] = t;
            }
            else
            {
                this._svcTypes.Add(typeof(T), t);
            }
        }

        public object CreateInstance(string fullyQualifiedTypeName, params object[] parameters)
        {
            IEnumerable<KeyValuePair<Type, Type>> source = from x in this._svcTypes
                                                           where x.Key.FullName.Equals(fullyQualifiedTypeName)
                                                           select x;
            object result;
            if (source.Count<KeyValuePair<Type, Type>>() == 0)
            {
                result = null;
            }
            else
            {
                object obj = Activator.CreateInstance(source.First<KeyValuePair<Type, Type>>().Key, parameters);
                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty("Services", BindingFlags.Instance | BindingFlags.Public);
                if (property != null)
                {
                    property.SetValue(obj, this, null);
                }
                result = obj;
            }
            return result;
        }

        public T CreateInstance<T>(params object[] parameters)
        {
            T t = (T)((object)Activator.CreateInstance(this._svcTypes[typeof(T)], parameters));
            Type type = t.GetType();
            PropertyInfo property = type.GetProperty("Services", BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                property.SetValue(t, this, null);
            }
            return t;
        }

        public void AddService<T>(object svcObject)
        {
            IEnumerable<KeyValuePair<Type, object>> source = from x in this._svcInstances
                                                             where x.Key == typeof(T)
                                                             select x;
            if (source.Count<KeyValuePair<Type, object>>() == 0)
            {
                this._svcInstances.Add(typeof(T), svcObject);
            }
            else
            {
                this._svcInstances[typeof(T)] = svcObject;
            }
        }

        public T Get<T>(params object[] parameters)
        {
            Type typeFromHandle = typeof(T);
            object obj = null;
            T t = (T)((object)obj);
            IEnumerable<KeyValuePair<Type, object>> source = from x in this._svcInstances
                                                             where x.Key == typeof(T)
                                                             select x;
            if (source.Count<KeyValuePair<Type, object>>() != 0)
            {
                t = (T)((object)source.First<KeyValuePair<Type, object>>().Value);
            }
            T result;
            if (t == null)
            {
                object[] customAttributes = this._svcTypes[typeof(T)].GetCustomAttributes(typeof(InstanceBehaviour), true);
                if (customAttributes.Length != 0)
                {
                    InstanceBehaviour instanceBehaviour = customAttributes[0] as InstanceBehaviour;
                    if (instanceBehaviour.InstanceBehaviourType == InstanceBehaviourType.AlwaysCreate)
                    {
                        result = this.CreateInstance<T>(parameters);
                        return result;
                    }
                }
                t = this.CreateInstance<T>(parameters);
                this.AddService<T>(t);
            }
            result = t;
            return result;
        }
    }


    public class ServiceContextHelper
    {
        public static T GetInstance<T>(string addinName)
        {
            Type type = null;
            Type[] array = null;
            Assembly assembly = Assembly.LoadFrom(addinName);
            try
            {
                array = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                Exception[] loaderExceptions = ex.LoaderExceptions;
                for (int i = 0; i < loaderExceptions.Length; i++)
                {
                    Exception value = loaderExceptions[i];
                    Console.WriteLine(value);
                }
            }
            if (array == null)
            {
                throw new Exception("Unable to load types : " + typeof(T).Name);
            }
            Type[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                Type type2 = array2[i];
                if (type2.IsClass && type2.GetInterfaces().Contains(typeof(T)))
                {
                    type = type2;
                    break;
                }
            }
            if (type == null)
            {
                throw new Exception("ServiceContextHelper:GetInstance unable to find type to create an instance: " + typeof(T).Name);
            }
            return (T)((object)Activator.CreateInstance(type));
        }
    }

}
