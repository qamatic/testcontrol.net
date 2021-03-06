﻿// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://www.testcontrol.org  (or) http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================
using System;
using System.Collections.Generic;

namespace TestControl.Net.Interfaces
{
    public interface IControlLocatorDef
    {
        IntPtr Handle { get; }
        Object[] FindControls { get; }        
        IntPtr Play();
        void SetRetryTime(int retryCount, int waitMilliSecPerRetry);
        void Clear();
    }
}