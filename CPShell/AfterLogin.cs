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

        public AfterLogin(IntPtr appWin, string scriptType, ArrayList quickDatas, string waitTime)
        {
            this.appWin = appWin;
            this.scriptType = scriptType;
            this.quickDatas = quickDatas;
            this.waitTime = waitTime;
        }

        public AfterLogin(IntPtr appWin, string command, string waitTime)
        {
            this.appWin = appWin;
            this.command = command;
            this.waitTime = waitTime;
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
                int index = -1;
                for (int i = 0; i < quickDatas.Count; i++)
                {
                    QuickData data = (QuickData)quickDatas[i];
                    if (data.name == scriptType)
                    {
                        index = i + 1;
                    }
                }
                if (index != -1)
                {
                    int keyCode = 111 + index;
                    SendMessage(appWin, WM_KEYDOWN, keyCode, 0);
                    SendMessage(appWin, WM_KEYUP, keyCode, 0);
                }
            }
            //send Command
            else if (this.command != null)
            {
                string[] array = this.command.Replace("\r", "").Split('\n');
                foreach (string line in array)
                {
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
