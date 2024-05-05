using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace BeatsOfGolf2
{
    internal class AutoFramework
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        const int MOUSEEVENTF_LEFTUP = 0x04;
        const int MOUSEEVENTF_LEFTDOWN = 0x02;

        public static void OpenProgram(string filePath, int width = 1000, int height = 1000, int x = 0, int y = 0)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("powershell.exe");
            startInfo.Arguments = $"-command \"Start-Process '{filePath}'\"";
            Process.Start(startInfo);
        }

        public static void RepositionWindow(string processName, int x, int y, int w, int h)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            IntPtr hWnd = processes[0].MainWindowHandle;
            Thread.Sleep(100);
            SetForegroundWindow(hWnd);
            Thread.Sleep(100);
            SetWindowPos(hWnd, IntPtr.Zero, x, y, w, h, SWP_SHOWWINDOW);
        }

        public static void DoubbleClickLeftMouseButton(int x, int y)
        {
            ClickLeftMouseButton(x, y);
            Thread.Sleep(100);
            ClickLeftMouseButton(x, y);
            Thread.Sleep(100);
        }

        public static void ClickLeftMouseButton(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, UIntPtr.Zero);
        }

        public static void DragLeftMouseButton(int x, int y, int distanceX, int distanceY)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, UIntPtr.Zero);

            for (int i = 0; i <= distanceX; i++)
            {
                Thread.Sleep(1);
                SetCursorPos(x + i, y);
            }

            for (int i = 0; i <= distanceY; i++)
            {
                Thread.Sleep(1);
                SetCursorPos(x + distanceX, y + i);
            }

            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, UIntPtr.Zero);
        }
    }
}
