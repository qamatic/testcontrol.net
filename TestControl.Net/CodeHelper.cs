// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TestControl.Net.Interfaces;

namespace TestControl.Net
{
    public abstract class CodeHelper : ICodeHelper
    {
        
        private readonly List<ICodeHelperItem> _codeHelperItems = new List<ICodeHelperItem>();
        private XDocument _xDoc;

        private XDocument HelpDoc
        {
            get
            {
                if (_xDoc == null)
                {
                    if (File.Exists(HelpXmlFile))
                    {
                        _xDoc = XDocument.Load(HelpXmlFile);
                    }
                }
                return _xDoc;
            }
        }

        public abstract string ConfigFile { get; }

        #region ICodeHelper Members

        public virtual void Load()
        {
            Configuration appConfig;
            try
            {
                appConfig = ConfigurationManager.OpenExeConfiguration(ConfigFile);
            }
            catch
            {
                appConfig = null;
            }

            if (appConfig == null)
                return;
            foreach (string item in appConfig.AppSettings.Settings.AllKeys)
            {
                var codeItem = new CodeHelperItem(this, item, appConfig.AppSettings.Settings[item].Value);
                _codeHelperItems.Add(codeItem);
            }
        }


        public virtual string GetHelp(object typeItem)
        {
            string itemToFind = string.Empty;
            if (typeItem is MethodInfo)
            {
                var methodInfo = typeItem as MethodInfo;
                itemToFind = GetHelpString("M", methodInfo.DeclaringType.Namespace, methodInfo.DeclaringType.Name,
                                           methodInfo.Name);
            }
            if (typeItem is PropertyInfo)
            {
                var propInfo = typeItem as PropertyInfo;
                itemToFind = GetHelpString("P", propInfo.DeclaringType.Namespace, propInfo.DeclaringType.Name,
                                           propInfo.Name);
            }
            if (itemToFind != string.Empty)
            {
// ReSharper disable PossibleNullReferenceException
                XElement item =
                    HelpDoc.Descendants("member").SingleOrDefault(p => p.Attribute("name").Value == itemToFind);
// ReSharper restore PossibleNullReferenceException
                if (item != null)
                {
                    XElement helpSummary = item.Element("summary");
                    if (helpSummary != null)
                        return helpSummary.Value;
                }
            }
            return "No help found  (or) check the file : " + HelpXmlFile;
        }


        public abstract string HelpXmlFile { get; }


        public virtual ICodeHelperItem GetItem(string wndClassName)
        {
            return CodeHelperItems.SingleOrDefault(x => x.WndClassName == wndClassName);
        }

        public IList<ICodeHelperItem> CodeHelperItems
        {
            get { return _codeHelperItems; }
        }

        #endregion

        protected virtual string GetHelpString(string infoType, string nameSpace, string className, string name)
        {
            return string.Format("{0}:{1}.{2}.{3}", infoType, nameSpace, className, name);
        }
    }
}