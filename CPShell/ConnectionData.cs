using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace CPShell
{
    [Serializable]
    public class ConnectionData
    {
        public string name = "";
        public string ip;
        public string port;
        public string protocol;
        public string username;
        public string password = "";
        public string keyfile = "";
        public string parent = "Default";
        public string quickType;
        public string command;
        public string waitTime;
        public string color = "0";
        public override string ToString()
        {
            return String.Format("name={0}\nip={1}\nport={2}\nprotocol={3}\nusername={4}\npassword={5}\nkeyfile={6}\nparent={7}\nquickType={8}\ncommand={9}\nwaitTime={10}\ncolor={11}\n",
                name,
                ip,
                port,
                protocol,
                username,
                password,
                keyfile,
                parent,
                quickType,
                command.Replace("\r", "").Replace("\n", "[ENTER]"),
                waitTime,
                color);
        }

    }

    class WindowConnection
    {
        public string name;
        public ConnectionData connection;
        public IntPtr hWnd;
        public TabPage tabPage;
        public string panel;
        public WindowConnection(string name, ConnectionData connection, IntPtr hWnd, TabPage tabPage, string panel)
        {
            this.name = name;
            this.connection = connection;
            this.hWnd = hWnd;
            this.tabPage = tabPage;
            this.panel = panel;
        }
    }

    class QuickData
    {
        public string name;
        public string data;
        public QuickData(string name, string data)
        {
            this.name = name;
            this.data = data;
        }
        public override string ToString()
        {
            return this.name;
        }
    }

}
