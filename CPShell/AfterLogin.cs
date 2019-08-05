using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CPShell
{
    class AfterLogin
    {
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_CHAR = 0x0102;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;

        string scriptType = null;
        ArrayList quickDatas = null;
        string command = null;
        string waitTime = "";
        IntPtr appWin;

        public AfterLogin(IntPtr appWin, string scriptType, string command, ArrayList quickDatas, string waitTime)
        {
            this.appWin = appWin;
            this.scriptType = scriptType;
            this.command = command;
            this.quickDatas = quickDatas;
            this.waitTime = waitTime;
        }

        private void execQuick(String scriptType)
        {
            int index = -1;
            string line = "";
            for (int i = 0; i < quickDatas.Count; i++)
            {
                QuickData data = (QuickData)quickDatas[i];
                if (data.name == scriptType)
                {
                    index = i + 1;
                    line = data.data;
                }
            }
            if (index != -1)
            {
                if (index <= 25)
                {
                    int keyCode = 111 + index;
                    SendMessage(appWin, WM_KEYDOWN, keyCode, 0);
                    SendMessage(appWin, WM_KEYUP, keyCode, 0);
                }
                else
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        SendMessage(appWin, WM_CHAR, c, 0);
                        Thread.Sleep(30);
                    }
                }
            }
        }

        public void run()
        {
            //wait
            if (this.waitTime != null)
            {
                try
                {
                    int count = Convert.ToInt32(this.waitTime);
                    Thread.Sleep(count);
                }
                catch (Exception exp)
                { 
                }
            }
            //send FX
            if (this.scriptType != null && quickDatas != null)
            {
                execQuick(this.scriptType);
            }
            //send Command
            else if (this.command != null)
            {
                string[] array = this.command.Replace("\r", "").Split('\n');
                foreach (string line in array)
                {
                    if (line.StartsWith("CALL "))
                    {
                        string name = line.Substring(5, line.Length - 5).Trim();
                        execQuick(name);
                        break;
                    }

                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        SendMessage(appWin, WM_CHAR, c, 0);
                        Thread.Sleep(30);
                    }
                    Thread.Sleep(100);
                    SendMessage(appWin, 0, 13, 0);
                    SendMessage(appWin, WM_KEYUP, 13, 0);
                    Thread.Sleep(500);
                }
            }
        }
    }
}
