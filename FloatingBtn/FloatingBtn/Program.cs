using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace FloatingBtn
{
    static class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lp1, string lp2);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImportAttribute("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_SHOW = 9;
        private const int SW_RESTORE = 9;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>s
        [STAThread]
        static void Main()
        {

            Process current = Process.GetCurrentProcess();
            string procName = current.ProcessName;
            Process[] processes = Process.GetProcessesByName(procName);

            if (processes.Length > 1)
            {

                IntPtr handle = FindWindow(null, procName);
                if (handle != IntPtr.Zero)
                {
                    ShowWindow(handle, SW_RESTORE);
                    ShowWindow(handle, SW_SHOW);
                    SetForegroundWindow(handle);
                }
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
