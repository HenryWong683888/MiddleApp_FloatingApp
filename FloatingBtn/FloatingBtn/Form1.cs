using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp;

namespace FloatingBtn
{
    public partial class Form1 : Form
    {

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        public Form1()
        {
            InitializeComponent();


            this.StartPosition = FormStartPosition.Manual;
            foreach (var scrn in Screen.AllScreens)
            {
                if (scrn.Bounds.Contains(this.Location))
                {
                    this.Location = new Point(scrn.Bounds.Right - this.Width - 150, scrn.Bounds.Top);
                    return;
                }
            }
        }

        public static void HandleRunningInstance(Process instance)
        {
            // 相同時透過ShowWindowAsync還原，以及SetForegroundWindow將程式至於前景
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var ws = new WebSocket("ws://localhost:13812"))
            {
                ws.OnMessage += (s, e1) =>
                {
                    Console.WriteLine("Laputa says: " + e1.Data);

                };

                ws.Connect();
                ws.Send("{\"Status\":\"setting\",\"Mode\":\"3\"," +
                    "\"Handwriting\":\"1\",\"Gamma\":\"4\",\"PenWidth\":" +
                    "\"3\",\"EraserWidth\":\"10\",\"DisplayMode\":\"0\"}");
                //Console.ReadKey(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("OUTLOOK").Length > 0)
            {
                HandleRunningInstance(Process.GetProcessesByName("OUTLOOK")[0]);
            }
            else
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\microsoft\\windows\\currentversion\\app paths\\OUTLOOK.EXE");
                string path = (string)key.GetValue("Path");
                if (path != null)
                    System.Diagnostics.Process.Start("OUTLOOK.EXE");
                else
                    MessageBox.Show("There is no Outlook in this computer!", "SystemError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            using (var ws = new WebSocket("ws://localhost:13812"))
            {
                ws.OnMessage += (s, e1) =>
                {
                    Console.WriteLine("Laputa says: " + e1.Data);

                };

                ws.Connect();
                ws.Send("{\"Status\":\"setting\",\"Mode\":\"3\"," +
                    "\"Handwriting\":\"1\",\"Gamma\":\"4\",\"PenWidth\":" +
                    "\"3\",\"EraserWidth\":\"10\",\"DisplayMode\":\"1\"}");
                //Console.ReadKey(true);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
