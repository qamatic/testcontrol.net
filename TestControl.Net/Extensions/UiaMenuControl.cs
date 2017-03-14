// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using TestControl.Net.Interfaces;
using TestControl.Net.Extensions;

namespace TestControl.Net.Extensions
{
    public static class UiaMenuControl
    {
        public static string[] GetItems(this IMenuControlUiaMarker uiaTreeView)
        {
            
            AutomationElementCollection items = uiaTreeView.AutomationElement.FindAll(TreeScope.Subtree,
                                                                                           new PropertyCondition(
                                                                                               AutomationElement.
                                                                                                   ControlTypeProperty,
                                                                                               ControlType.TreeItem));
            var list = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                list.Add(items[i].Current.Name);
            }
            return list.ToArray();
        }

        public static void SelectItem(this IMenuControlUiaMarker uiaTreeView, string item)
        {
            var treeItem = uiaTreeView.AutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, item));
            if (treeItem != null)
            {
                var uiaElement = new UiaElementWrapper(treeItem);
                ((IMenuControl)uiaTreeView).LastElement = uiaElement;             
            }
        }

       
        public static string GetSelectedItem(this IMenuControlUiaMarker uiaListBox)
        {
            var list = new List<string>();
            var treeItem = uiaListBox.AutomationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem));
            if (treeItem != null)
            {
                var uiaElement = new UiaElementWrapper(treeItem);
                var selectItemPattern = uiaElement.GetSelectionItemPattern();                
                var selectionPattern = selectItemPattern.Current.SelectionContainer.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;                
                list.AddRange(selectionPattern.Current.GetSelection().Select(ae => ae.Current.Name));
            }
            return list.Count == 0 ? string.Empty : list[0];
        }
    }
}