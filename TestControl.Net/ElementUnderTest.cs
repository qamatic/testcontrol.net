// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections;
using System.Collections.Generic;
using TestControl.Net.Interfaces;
using System.Linq;

namespace TestControl.Net
{
    public class ElementUnderTest : IElementUnderTest
    {
        private readonly Hashtable _supportObjects = new Hashtable();
        private object _underlyingObject;

        #region Implementation of ISystemUnderTest

        public virtual object UnderlyingObject
        {
            get { return _underlyingObject; }
        }

        public virtual bool IsObjectNull
        {
            get { return (UnderlyingObject == null); }
        }

        public virtual string AsString(string propertyName)
        {
            throw new NotImplementedException();
        }

        public virtual bool AsBoolean(string propertyName)
        {
            throw new NotImplementedException();
        }

        public virtual int AsInt(string propertyName)
        {
            throw new NotImplementedException();
        }

        public virtual double AsDouble(string propertyName)
        {
            throw new NotImplementedException();
        }

        public virtual void SetAsString(string propertyName, string value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetAsBoolean(string propertyName, bool value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetAsInt(string propertyName, int value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetAsDouble(string propertyName, double value)
        {
            throw new NotImplementedException();
        }

        public virtual IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        public virtual void SetFocus()
        {
            throw new NotImplementedException();
        }

        public virtual string GetLastError()
        {
            throw new NotImplementedException();
        }

        public virtual void ExecuteEvent(string eventName, object[] parameters)
        {
            ExecuteMethod(eventName, parameters); 
        }

        public virtual IElementUnderTest ExecuteMethod(string methodName, object[] parameters)
        {
           
            parameters = parameters ?? new object[]{ };
            
            var types = new List<Type>();
            foreach (var o in parameters)
            {
                types.Add(o.GetType());
            }
            var aMethod = UnderlyingObject.GetType().GetMethod(methodName, types.ToArray());
            var eut = new ElementUnderTest();
            eut.SetUnderlyingObject(aMethod.Invoke(UnderlyingObject, parameters));
            return eut;
        }


        public virtual Hashtable SupportObjects
        {
            get { return _supportObjects; }
        }

        public virtual bool IsEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsVisible
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsEditable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string Text
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual void SetUnderlyingObject(object testObject)
        {
            _underlyingObject = testObject;
        }

        public virtual void Click()
        {
            throw new NotImplementedException();
        }

        public virtual IElementUnderTest FindChild(string locator)
        {
            throw new NotImplementedException();
        }

        public virtual IList<IElementUnderTest> FindChildren(string locator)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}