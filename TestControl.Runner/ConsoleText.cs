// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System;
using System.Runtime.InteropServices;

namespace TestControl.Runner
{
    public class ConsoleText
    {
        #region ColorCodes enum

        [Flags]
        public enum ColorCodes
        {
            Blue = 0x0001,
            Green = 0x0002,
            Red = 0x0004,
            Yellow = 0x0006,
            White = 0x0008
        }

        #endregion

        public const ColorCodes DEFAULT_COLOR = ColorCodes.White;
        private static readonly IntPtr _handle = GetStdHandle(-11);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, int wAttributes);

        public static void SetColor(ColorCodes color)
        {
            SetConsoleTextAttribute(_handle, (int) color);
        }
    }
}