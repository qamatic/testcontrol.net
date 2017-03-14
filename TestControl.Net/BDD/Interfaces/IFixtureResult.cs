// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;

namespace TestControl.Net.BDD.Interfaces
{
    public interface IFixtureResult
    {
        int Right { get; set; }
        int Wrong { get; set; }
        int Exceptions { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        bool Pass { get; }
        TimeSpan GetTimeSpan();
    }
}