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

namespace TestControl.Net.Extensions
{
    public static class UiaListBox
    {
        public static string[] GetItems(this IListBoxUiaMarker uiaListBox)
        {
            AutomationElementCollection allItems = uiaListBox.AutomationElement.FindAll(TreeScope.Subtree,
                                                                                           new PropertyCondition(
                                                                                               AutomationElement.
                                                                                                   ControlTypeProperty,
                                                                                               ControlType.ListItem));
            var list = new List<string>();
            for (int i = 0; i < allItems.Count; i++)
            {
                list.Add(allItems[i].Current.Name);
            }
            return list.ToArray();
        }


        public static bool IsMultiSelectAllowed(this IListBoxUiaMarker uiaListBox)
        {
            var itemToSelect = uiaListBox.AutomationElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
            if (itemToSelect != null)
            {
                var selectItemPattern = (SelectionItemPattern)itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);
                var selectionPattern = selectItemPattern.Current.SelectionContainer.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
                return selectionPattern.Current.CanSelectMultiple;
            }
            return true;
        }

        public static void UnSelectItem(this IListBoxUiaMarker uiaListBox, string item)
        {
            Condition cond = new PropertyCondition(
                                 AutomationElement.NameProperty, item, PropertyConditionFlags.IgnoreCase);

            var itemToSelect = uiaListBox.AutomationElement.FindFirst(TreeScope.Children, cond);

            if (itemToSelect != null)
            {
                var selectItemPattern = (SelectionItemPattern)itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);

                selectItemPattern.RemoveFromSelection();

            }
        }

        public static void SelectItem(this IListBoxUiaMarker uiaListBox, string item, bool bMultiSelect = true)
        {
            Condition cond = new PropertyCondition(
                                 AutomationElement.NameProperty, item, PropertyConditionFlags.IgnoreCase);

            var itemToSelect = uiaListBox.AutomationElement.FindFirst(TreeScope.Children, cond);

            if (itemToSelect != null)
            {
                var selectItemPattern = (SelectionItemPattern)itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);
                var selectionPattern = selectItemPattern.Current.SelectionContainer.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;

                if (bMultiSelect && selectionPattern.Current.CanSelectMultiple)
                    selectItemPattern.AddToSelection();
                else
                    selectItemPattern.Select();
            }
        }

        public static string[] GetSelectedItems(this IListBoxUiaMarker uiaListBox)
        {
            var list = new List<string>();
            var itemToSelect = uiaListBox.AutomationElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
            if (itemToSelect != null)
            {
                var selectItemPattern = (SelectionItemPattern)itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);
                var selectionPattern = selectItemPattern.Current.SelectionContainer.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
                list.AddRange(selectionPattern.Current.GetSelection().Select(ae => ae.Current.Name));
            }
            return list.ToArray();
        }
    }
}