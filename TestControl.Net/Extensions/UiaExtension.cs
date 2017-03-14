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
using System.Windows.Automation;
using TestControl.Net.Interfaces;
using System.Linq;

namespace TestControl.Net.Extensions
{
    public static class UiaExtension
    {
        public static InvokePattern GetInvokePattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
        }


        public static WindowPattern GetWindowPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
        }


        public static SelectionPattern GetSelectionPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
        }


        public static SelectionItemPattern GetSelectionItemPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return
                uiaAutomation.AutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
        }

        public static ValuePattern GetValuePattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
        }


        public static TogglePattern GetTogglePattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
        }


        public static TextPattern GetTextPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
        }

        public static GridItemPattern GetGridItemPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(GridItemPattern.Pattern) as GridItemPattern;
        }


        public static GridPattern GetGridPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(GridPattern.Pattern) as GridPattern;
        }

        public static DockPattern GetDockPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(DockPattern.Pattern) as DockPattern;
        }

        public static ExpandCollapsePattern GetExpandCollapsePattern(this IWin32MarkerExtension uiaAutomation)
        {
            return
                uiaAutomation.AutomationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern) as
                ExpandCollapsePattern;
        }


        public static MultipleViewPattern GetMultipleViewPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(MultipleViewPattern.Pattern) as MultipleViewPattern;
        }

        public static RangeValuePattern GetRangeValuePattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(RangeValuePattern.Pattern) as RangeValuePattern;
        }

        public static ScrollPattern GetScrollPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
        }

        public static ScrollItemPattern GetScrollItemPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(ScrollItemPattern.Pattern) as ScrollItemPattern;
        }


        public static TableItemPattern GetTableItemPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(TableItemPattern.Pattern) as TableItemPattern;
        }

        public static TablePattern GetTablePattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(TablePattern.Pattern) as TablePattern;
        }

        public static TransformPattern GetTransformPattern(this IWin32MarkerExtension uiaAutomation)
        {
            return uiaAutomation.AutomationElement.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
        }
 
      
        public static void CloseWindow(this IWin32MarkerExtension uiaWindow)
        {
            uiaWindow.GetWindowPattern().Close();
        }

        public static bool IsModelDialog(this IWin32MarkerExtension uiaWindow)
        {
            return uiaWindow.GetValue<bool>(WindowPattern.IsModalProperty);
        }
 
        public static T GetValue<T>(this IWin32MarkerExtension uiaAutomation, AutomationProperty prop, bool ignoreDefaultValue = true)
        {            
            var result = Activator.CreateInstance(typeof(T));

            var obj  = uiaAutomation.AutomationElement.GetCurrentPropertyValue(prop, ignoreDefaultValue);

            if (obj != AutomationElement.NotSupported)
            {
                result = (T) obj;
            }
            return (T)result;
        }

        public static string[] GetItems(this IWin32MarkerExtension uiaCombo)
        {

            AutomationElementCollection comboboxItems = uiaCombo.AutomationElement.FindAll(TreeScope.Subtree,
                                                                                           new PropertyCondition(
                                                                                               AutomationElement.
                                                                                                   ControlTypeProperty,
                                                                                               ControlType.ListItem));
            var list = new List<string>();
            for (int i = 0; i < comboboxItems.Count; i++)
            {
                list.Add(comboboxItems[i].Current.Name);
            }
            return list.ToArray();
        }

        public static void SelectItem(this IWin32MarkerExtension uiaCombo, string item)
        {

            var allItems = uiaCombo.AutomationElement.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
            AutomationElement itemToSelect = null;
            for (int i = 0; i < allItems.Count; i++)
            {
                if ((allItems[i].Current.Name.Equals(item)))
                {
                    itemToSelect = allItems[i];
                    break;
                }
            }
            if (itemToSelect != null)
            {
                var selectPattern = (SelectionItemPattern)itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);
                selectPattern.Select();
            }

        }

        public static string GetSelectedItem(this IWin32MarkerExtension uiaListBox)
        {
            var list = new List<string>();
            var itemToSelect = uiaListBox.AutomationElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));
            if (itemToSelect != null)
            {
                var selectItemPattern = (SelectionItemPattern)itemToSelect.GetCurrentPattern(SelectionItemPattern.Pattern);
                var selectionPattern = selectItemPattern.Current.SelectionContainer.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
                list.AddRange(selectionPattern.Current.GetSelection().Select(ae => ae.Current.Name));
            }
            return list.Count == 0 ? string.Empty : list[0];
        }

        public static string GetContent(this IWin32MarkerExtension uiaWindow)
        {
            return uiaWindow.GetTextPattern().DocumentRange.GetText(-1);
        }

        public static void SetContent(this IWin32MarkerExtension uiaTextBox, String text)
        {
            uiaTextBox.GetValuePattern().SetValue(text);
        }

        public static void SetChecked(this IWin32MarkerExtension uiaRadioButton, bool bCheck)
        {
            if (bCheck)
            {
                if (uiaRadioButton.GetTogglePattern().Current.ToggleState != System.Windows.Automation.ToggleState.On)
                    uiaRadioButton.GetTogglePattern().Toggle();
            }
            else
            {
                if (uiaRadioButton.GetTogglePattern().Current.ToggleState != System.Windows.Automation.ToggleState.Off)
                    uiaRadioButton.GetTogglePattern().Toggle();
            }

        }

        public static System.Windows.Automation.ToggleState GetCheckStatus(this IWin32MarkerExtension uiaRadioButton)
        {
            return uiaRadioButton.GetTogglePattern().Current.ToggleState;
        }

        public static bool IsSelected(this IWin32MarkerExtension uiaRadioButton)
        {
            return uiaRadioButton.GetSelectionItemPattern().Current.IsSelected;
        }

        public static void SetSelected(this IWin32MarkerExtension uiaRadioButton, bool bSelect)
        {
            if (bSelect)
            {
                uiaRadioButton.GetSelectionItemPattern().Select();
            }
            else
            {
                //uiaRadioButton.GetSelectionItemPattern().RemoveFromSelection();
            }

        }

        public static void SetText(this IWin32MarkerExtension uiaTextBox, string text)
        {
            uiaTextBox.GetValuePattern().SetValue(text);
        }

        public static string GetText(this IWin32MarkerExtension uiaTextBox)
        {
            return uiaTextBox.GetValuePattern().Current.Value;
        }

        
    }
}