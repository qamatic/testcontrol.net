// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestControl.Net.Interfaces;

namespace TestControl.Net
{
    public class CodeHelperItem : ICodeHelperItem
    {
        private readonly string _asmFile;
        private readonly string _className;
        private readonly ICodeHelper _codeHelper;
        private readonly Type _type;
        private readonly string _wndClassName;

        public CodeHelperItem(ICodeHelper codeHelper, string wndClassName, string fullyQualifiedClassName)
        {
            _wndClassName = wndClassName;
            _codeHelper = codeHelper;
            int idx = fullyQualifiedClassName.LastIndexOf('.');
            _asmFile = fullyQualifiedClassName.Substring(0, idx) + ".dll";
            Assembly asm = Assembly.LoadFrom(_asmFile);
            _type = asm.GetType(fullyQualifiedClassName, false, true);
            if (_type != null)
                _className = _type.Name;
        }

        #region ICodeHelperItem Members

        public virtual string AssemblyFile
        {
            get { return _asmFile; }
        }


        public virtual string WndClassName
        {
            get { return _wndClassName; }
        }

        public virtual Type ClassType
        {
            get { return _type; }
        }

        public virtual string ClassName
        {
            get { return _className; }
        }

        public virtual object[] TypeItems
        {
            get
            {
                var items = new List<object>();
                if (_type == null)
                    return items.ToArray();

                MethodInfo[] methodInfos =
                    _type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

                Array.Sort(methodInfos,
                           (methodInfo1, methodInfo2) => methodInfo1.Name.CompareTo(methodInfo2.Name));


                items.AddRange(
                    methodInfos.Where(
                        methodInfo => !methodInfo.Name.StartsWith("get_") && !methodInfo.Name.StartsWith("set_")));

                PropertyInfo[] propInfos =
                    _type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                Array.Sort(propInfos,
                           (propInfo1, propInfo2) => propInfo1.Name.CompareTo(propInfo2.Name));


                items.AddRange(propInfos);
                return items.ToArray();
            }
        }


        public ICodeHelper CodeHelper
        {
            get { return _codeHelper; }
        }

        #endregion

        public override string ToString()
        {
            return _wndClassName;
        }
    }
}