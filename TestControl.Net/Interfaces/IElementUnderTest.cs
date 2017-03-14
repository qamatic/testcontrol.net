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

namespace TestControl.Net.Interfaces
{
    public interface IElementUnderTest
    {
        object UnderlyingObject { get; }
        bool IsObjectNull { get; }
        Hashtable SupportObjects { get; }
        string AsString(string propertyName);
        bool AsBoolean(string propertyName);
        int AsInt(string propertyName);
        double AsDouble(string propertyName);
        void SetAsString(string propertyName, string value);
        void SetAsBoolean(string propertyName, bool value);
        void SetAsInt(string propertyName, int value);
        void SetAsDouble(string propertyName, double value);
        IntPtr GetHandle();
        void SetFocus();
        string GetLastError();
        void ExecuteEvent(string eventName, object[] parameters = null);
        IElementUnderTest ExecuteMethod(string methodName, object[] parameters = null);
        void SetUnderlyingObject(object testObject);
        void Clear();
        void Click();
        string Text { get; set; }
        bool IsEnabled { get; }
        bool IsVisible { get; }
        bool IsEditable { get; }
        IElementUnderTest FindChild(String locator);
        IList<IElementUnderTest> FindChildren(String locator);

    }
}