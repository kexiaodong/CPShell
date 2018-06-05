﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CPShell
{
    [Serializable]
    public class ConnectionData
    {
        public string name;
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
    }

    class WindowConnection
    {
        public string name;
        public ConnectionData connection;
        public IntPtr hWnd;
        public WindowConnection(string name, ConnectionData connection, IntPtr hWnd)
        {
            this.name = name;
            this.connection = connection;
            this.hWnd = hWnd;
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