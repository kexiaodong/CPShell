using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace CPShell
{
    public partial class Form1 : Form
    {
        #region WINAPI
        [DllImport("user32.dll")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("user32.dll ")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hwnd, UInt32 wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError=true)]
        static extern IntPtr SetFocus(IntPtr hWnd);

        public const int WM_CHAR = 0x0102;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        public static extern int IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern int ShowWindow(IntPtr hWnd, uint nCmdShow);

        const UInt32 WM_CLOSE = 0x0010;
        #endregion

        private TreeNode m_rootNode;
        private Hashtable m_nodes = new Hashtable();
        private Hashtable m_server = new Hashtable();
        private ArrayList m_quickDatas = new ArrayList();
        private int m_keyButtonIndex = 0;
        private Hashtable m_window = new Hashtable();
        private int m_windowIndex = 0;
        private IntPtr m_lastWin = IntPtr.Zero;
        private IntPtr m_iPopWin = IntPtr.Zero;
        private string defaultColor = "0";

        public Form1()
        {
            this.Visible = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //init container 1
            this.splitContainer1.Visible = false;
            this.splitContainer1.Panel2MinSize = 0;
            toolStripButton1_Click(null, null);
            //init container 2
            this.splitContainer2.Panel2Collapsed = true;
            changeSplitPanel2();
            //init server node
            m_rootNode = new TreeNode("Servers");
            m_nodes["Servers"] = m_rootNode;
            this.treeView1.Nodes.Add((TreeNode)m_nodes["Servers"]);
            loadServer();
            //init short key
            loadKey();
            reloadKeyButton();
            //start
            this.Visible = true;
            Thread.Sleep(3);
            this.splitContainer1.Visible = true;
            this.panel3.Visible = true;
            this.panel4.Visible = true;
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.splitContainer1.Panel2MinSize != 200)
            {
                this.splitContainer1.Panel2MinSize = 200;
                this.splitContainer1.SplitterDistance = this.Width - 200;
            }
            else
            {
                this.splitContainer1.Panel2MinSize = 0;
                this.splitContainer1.SplitterDistance = this.Width + 10;
            }
            Form1_Resize(null, null);
        }


        #region Key

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            QuickForm quickForm = new QuickForm(m_quickDatas);
            if (quickForm.ShowDialog() == DialogResult.OK)
            {
                m_quickDatas = new ArrayList();
                foreach (QuickData data in quickForm.resultList.Items)
                {
                    m_quickDatas.Add(data);
                }
                saveKey();
                reloadKeyButton();
            }
        }

        private void reloadKeyButton()
        {
            while (this.toolStrip1.Items.Count > 7)
            {
                this.toolStrip1.Items.RemoveAt(this.toolStrip1.Items.Count - 1);
            }

            m_keyButtonIndex++;
            foreach (QuickData quickData in m_quickDatas)
            {
                ToolStripButton tempButton = new System.Windows.Forms.ToolStripButton();
                tempButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                tempButton.Size = new System.Drawing.Size(37, 22);
                tempButton.Name = "toolStripButton" + (m_keyButtonIndex++);
                tempButton.Text = quickData.name;
                tempButton.Click += new EventHandler(tempButton_Click);
                tempButton.MouseMove += new MouseEventHandler(tempButton_MouseMove);
                this.toolStrip1.Items.Add(tempButton);
            }            
        }

        void tempButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        void tempButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            QuickData data = null;
            int index = -1;
            for (int i = 0; i < m_quickDatas.Count; i++)
            {
                if (((QuickData)m_quickDatas[i]).name == button.Text)
                {
                    data = (QuickData)m_quickDatas[i];
                    index = i + 1;
                    break;
                }
            }

            if (m_lastWin != IntPtr.Zero)
            {
                if (index <= 25)
                {
                    //让putty自己完成
                    int keyCode = 111 + index;
                    SendMessage(m_lastWin, WM_KEYDOWN, keyCode, 0);
                    SendMessage(m_lastWin, WM_KEYUP, keyCode, 0);
                }
                else
                {
                    string line = data.data;
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        SendMessage(m_lastWin, WM_CHAR, c, 0);
                        Thread.Sleep(30);
                    }
                }
                SetForegroundWindow(m_lastWin);
            }
        }

        private void loadKey()
        {
            if (!File.Exists("config.ini"))
            {
                return;
            }
            try
            {
                StreamReader reader = new StreamReader("config.ini", Encoding.GetEncoding("gb2312"));
                string input;
                string keyword = "";
                StringBuilder keyData = new StringBuilder();
                while ((input = reader.ReadLine()) != null)
                {
                    if (input.StartsWith("[DEFAULT-COLOR="))
                    {
                        defaultColor = input.Replace("[DEFAULT-COLOR=", "").Replace("]", "");
                    }
                    else if (input.StartsWith("["))
                    {
                        if (keyword != "")
                        {
                            QuickData quickData = new QuickData(keyword, keyData.ToString());
                            m_quickDatas.Add(quickData);
                        }
                        keyword = input.Replace("[", "").Replace("]", "");
                        keyData = new StringBuilder();
                    }
                    else
                    {
                        keyData.Append(input + "\r\n");
                    }
                }
                if (keyword != "")
                {
                    QuickData quickData = new QuickData(keyword, keyData.ToString());
                    m_quickDatas.Add(quickData);
                }
                reader.Close();
            }
            catch (Exception exp)
            { 
            }
        }

        private void saveKey()
        {
            StreamWriter writer = new StreamWriter("config.ini", false, Encoding.GetEncoding("gb2312"));
            writer.WriteLine("[DEFAULT-COLOR=" + defaultColor + "]");
            for(int i=0;i < m_quickDatas.Count;i++)
            {
                QuickData quickData = (QuickData)(m_quickDatas[i]);
                writer.WriteLine("[" + quickData.name + "]");
                if (i != m_quickDatas.Count - 1)
                {
                    writer.WriteLine(quickData.data.Trim());
                }
                else
                {
                    writer.Write(quickData.data.Trim());
                }                
            }            
            writer.Close();
        }


        #endregion

        #region Server
        
        private void loadServer()
        {
            m_server = loadConfig("server.ini");
            getParent("Default");
            ArrayList parentNames = new ArrayList();
            Hashtable serverFolder = new Hashtable();
            serverFolder["Default"] = new ArrayList();
            foreach (string key in m_server.Keys)
            {
                ConnectionData puttyData = (ConnectionData)m_server[key];
                if (puttyData.parent != "Default" && parentNames.IndexOf(puttyData.parent) == -1)
                {
                    parentNames.Add(puttyData.parent);
                    serverFolder[puttyData.parent] = new ArrayList();
                }
                ArrayList serverList = (ArrayList)serverFolder[puttyData.parent];
                serverList.Add(puttyData);
            }
            parentNames.Sort();
            parentNames.Insert(0, "Default");
            //sort
            for (int i = 0; i < parentNames.Count; i++)
            {
                string parentName = (string)parentNames[i];
                ArrayList serverList = (ArrayList)serverFolder[parentName];
                for (int c1 = 0; c1 < serverList.Count - 1; c1++)
                {
                    for (int c2 = c1; c2 < serverList.Count; c2++)
                    {
                        ConnectionData data1 = (ConnectionData)serverList[c1];
                        ConnectionData data2 = (ConnectionData)serverList[c2];
                        if (data1.name.CompareTo(data2.name) > 0)
                        {
                            serverList[c1] = data2;
                            serverList[c2] = data1;
                        }
                    }
                }
                //add
                for (int j = 0; j < serverList.Count; j++)
                {
                    ConnectionData puttyData = (ConnectionData)serverList[j];
                    TreeNode parent = getParent(puttyData.parent);
                    parent.Nodes.Add(puttyData.name);
                }
            }

            this.treeView1.SelectedNode = (TreeNode)m_nodes["Servers"];
            this.treeView1.ExpandAll();
        }

        private void saveConfig(Hashtable obj, string dataFile)
        {
            StreamWriter stream = new StreamWriter(dataFile, false, Encoding.GetEncoding("gb2312"));
            foreach (string key in obj.Keys)
            {
                ConnectionData data = (ConnectionData)obj[key];
                stream.WriteLine(data);
            }
            stream.Close();
        }

        private Hashtable loadConfig(string dataFile)
        {
            Hashtable obj = new Hashtable();
            try
            {
                StreamReader reader = new StreamReader(dataFile, Encoding.GetEncoding("gb2312"));
                ConnectionData data = new ConnectionData();
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    int find = input.IndexOf("=");
                    if (find == -1)
                    {
                        continue;
                    }
                    string key = input.Substring(0, find);
                    string value = input.Substring(find + 1, input.Length - (find + 1));
                    if (key == "name")
                    {
                        if (data.name != "")
                        {
                            if (data.parent == "")
                            {
                                data.parent = "Default";
                            }
                            obj[data.name] = data;
                        }
                        data = new ConnectionData();
                        data.name = value;
                    }
                    else if (key == "command")
                    {
                        data.command = value.Replace("[ENTER]", "\r\n");
                    }
                    else
                    {
                        data.GetType().GetField(key).SetValue(data, value);
                    }
                }
                if (data.name != "")
                {
                    if (data.parent == "")
                    {
                        data.parent = "Default";
                    }
                    obj[data.name] = data;
                }
                reader.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return obj;
        }

        ConnectionData Dlg2Data(ConnectionForm connection)
        {
            ConnectionData puttyData = new ConnectionData();
            puttyData.name = connection.c_name.Text;
            puttyData.ip = connection.c_ip.Text;
            puttyData.port = connection.c_port.Text;
            puttyData.protocol = connection.c_protocol;
            puttyData.username = connection.c_username.Text;
            puttyData.password = connection.c_password.Text;
            puttyData.keyfile = connection.c_keyfile.Text;
            puttyData.parent = connection.c_parent.Text;
            if (puttyData.parent.Trim() == "")
            {
                puttyData.parent = "Default";
            }
            puttyData.quickType = connection.c_scriptType.Text;
            puttyData.command = connection.c_command.Text;
            puttyData.waitTime = connection.c_waitTime.Text;
            puttyData.color = connection.c_color.SelectedIndex + "";
            return puttyData;
        }


        private TreeNode getParent(string parent)
        {
            TreeNode rootNode = (TreeNode)m_nodes["Servers"];
            for (int i = 0; i < rootNode.Nodes.Count; i++)
            {
                if (rootNode.Nodes[i].Text == parent)
                {
                    return rootNode.Nodes[i];
                }
            }
            TreeNode temp = new TreeNode(parent);
            rootNode.Nodes.Add(temp);
            return temp;
        }

        private TreeNode getNode(string parent_name, string name)
        {
            TreeNode parent = getParent(parent_name);
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                if (parent.Nodes[i].Text == name)
                {
                    return parent.Nodes[i];
                }
            }
            return null;
        }

        private void addNewServer_Click(object sender, EventArgs e)
        {
            ConnectionForm connection = new ConnectionForm((TreeNode)m_nodes["Servers"], m_quickDatas, m_server);
            if (connection.ShowDialog() == DialogResult.OK)
            {
                ConnectionData puttyData = Dlg2Data(connection);
                if (m_server.ContainsKey(puttyData.name))
                {
                    removeServer(puttyData.name, false);
                }
                m_server[puttyData.name] = puttyData;

                TreeNode temp = new TreeNode(puttyData.name);
                TreeNode parent = getParent(puttyData.parent);
                parent.Nodes.Add(temp);
                parent.ExpandAll();

                saveConfig(m_server, "server.ini");
            }
        }

        private void removeServer(string name, bool save)
        {
            ConnectionData puttyData = (ConnectionData)m_server[name];
            TreeNode temp = getNode(puttyData.parent, puttyData.name);
            if (temp != null)
            {
                m_server.Remove(puttyData.name);
                TreeNode parent = getParent(puttyData.parent);
                parent.Nodes.Remove(temp);
                if (save)
                {
                    saveConfig(m_server, "server.ini");
                }
            }
        }

        private void deleteServer_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeView1.SelectedNode;
                if (node != null)
                {
                    if (node == m_rootNode || node.Text == "Servers")
                    {
                        return;
                    }

                    if (node.Parent == m_rootNode)
                    {
                        DialogResult result = MessageBox.Show(
                            "Delete all node in " + node.Text + "?", "Confirm", MessageBoxButtons.OKCancel);
                        if (result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }

                        ArrayList list = new ArrayList();
                        foreach (TreeNode childNode in node.Nodes)
                        {
                            list.Add(childNode.Text);
                        }
                        foreach (string name in list)
                        {
                            removeServer(name, false);
                        }
                        saveConfig(m_server, "server.ini");
                        if (node.Text != "Default")
                        {
                            node.Remove();
                        }
                    }
                    else
                    {
                        removeServer(node.Text, true);                        
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void editServer_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                ConnectionData puttyData = (ConnectionData)m_server[node.Text];
                if (puttyData == null)
                {
                    return;
                }
                ConnectionForm connection = new ConnectionForm((TreeNode)m_nodes["Servers"], m_quickDatas, m_server);
                connection.setData(puttyData);
                string oldParent = puttyData.parent;
                string oldName = puttyData.name;
                if (connection.ShowDialog() == DialogResult.OK)
                {
                    puttyData = Dlg2Data(connection);                    
                    if (oldName != puttyData.name)
                    {
                        //改名新建
                        if (m_server.ContainsKey(puttyData.name))
                        {
                            removeServer(puttyData.name, false);
                        }
                        m_server[puttyData.name] = puttyData;
                        saveConfig(m_server, "server.ini");
                        TreeNode temp = new TreeNode(puttyData.name);
                        TreeNode parentNode = getParent(puttyData.parent);
                        parentNode.Nodes.Add(temp);
                        parentNode.ExpandAll();
                    }
                    else
                    {
                        //改组
                        m_server[puttyData.name] = puttyData;
                        saveConfig(m_server, "server.ini");
                        if (oldParent != puttyData.parent)
                        {
                            TreeNode oldParentNode = getParent(oldParent);
                            TreeNode parentNode = getParent(puttyData.parent);
                            TreeNode temp = getNode(oldParent, puttyData.name);
                            oldParentNode.Nodes.Remove(temp);
                            parentNode.Nodes.Add(temp);
                            parentNode.ExpandAll();
                        }
                    }
                }
            }
        }

        #endregion

        #region Windows
        
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                WindowConnection win = getLastWindow();
                string param = "";
                if (win != null)
                {
                    if (win.connection.password.Trim() != "")
                    {
                        param = String.Format("scp://{0}:\"{1}\"@{2}:{3}",
                            win.connection.username,
                            win.connection.password,
                            win.connection.ip,
                            win.connection.port
                            );
                    }
                    else
                    {
                        param = String.Format("scp://{0}@{1}:{2}",
                            win.connection.username,
                            win.connection.ip,
                            win.connection.port
                            );
                    }
                    if (win.connection.keyfile.Trim() != "")
                    {
                        param = param + " /privatekey=" + win.connection.keyfile;
                    }
                    string installPath = @"WinSCP.exe";
                    Process process = Process.Start(installPath, param);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        }

        private void createNewConnection(ConnectionData puttyData)
        {
            string param = String.Format("-{0} -l {1} -P {2} {3}",
                puttyData.protocol.ToLower(),
                puttyData.username,
                puttyData.port,
                puttyData.ip);

            if (puttyData.protocol == "SSH" && puttyData.password.Trim() != "")
            {
                param = param + " -pw " + puttyData.password;
            }
            if (puttyData.keyfile.Trim() != "")
            {
                param = param + " -i " + puttyData.keyfile;
            }
            try
            {
                int colorIndex = Convert.ToInt32(puttyData.color);
                if (colorIndex > 1)
                {
                    param = param + " -color " + (colorIndex - 1);
                }
                else if (colorIndex == 0)
                {
                    colorIndex = Convert.ToInt32(defaultColor);
                    if (colorIndex > 0)
                    {
                        param = param + " -color " + (colorIndex - 1);
                    }
                }
            }
            catch (Exception)
            {
            }

            string installPath = @"putty.exe";
            Process process = Process.Start(installPath, param);
            process.WaitForInputIdle();

            // 嵌入到parentHandle指定的句柄中
            IntPtr appWin = process.MainWindowHandle;
            string panel = "";
            if (this.splitContainer2.Panel2Collapsed)
            {
                SetParent(appWin, this.splitContainer2.Panel1.Handle);
                panel = "panel1";
            }
            else
            {
                WindowConnection lastConnection = this.getLastWindow();
                if (lastConnection == null)
                {
                    panel = "panel1";
                }
                else
                {
                    panel = lastConnection.panel;
                }
                if (panel == "panel1")
                {
                    SetParent(appWin, this.splitContainer2.Panel1.Handle);
                }
                else
                {
                    SetParent(appWin, this.splitContainer2.Panel2.Handle);
                }                
            }            

            MoveWindow(appWin, 0, 0, 1024, 600, true);
            ShowWindow(appWin, 3);

            string windowName = (++m_windowIndex) + " - " + puttyData.name;

            TabPage tabPage = new System.Windows.Forms.TabPage();
            tabPage.Location = new System.Drawing.Point(0, 0);
            tabPage.Name = windowName + "_1";
            tabPage.Padding = new System.Windows.Forms.Padding(2);
            tabPage.Size = new System.Drawing.Size(511, 0);
            tabPage.TabIndex = 1;
            tabPage.Text = windowName;
            tabPage.UseVisualStyleBackColor = true;
            this.tabControl1.TabPages.Add(tabPage);

            WindowConnection winConnection = new WindowConnection(windowName, puttyData, appWin, tabPage, panel);
            m_window[windowName] = winConnection;
            m_lastWin = appWin;

            if (puttyData.quickType == null)
            {
                return;
            }

            if (puttyData.quickType != "Input Command")
            {
                AfterLogin after = new AfterLogin(appWin,
                    puttyData.quickType, 
                    null,
                    this.m_quickDatas, 
                    puttyData.waitTime);
                Thread nonParameterThread = new Thread(new ThreadStart(after.run));
                nonParameterThread.Start();
            }
            else if (puttyData.command != null && puttyData.command.Trim() != "")
            {
                AfterLogin after = new AfterLogin(appWin,
                    null,
                    puttyData.command, 
                    this.m_quickDatas,
                    puttyData.waitTime);
                Thread nonParameterThread = new Thread(new ThreadStart(after.run));
                nonParameterThread.Start();
            }
        }

        private void changeSplitPanel2()
        {
            if (this.splitContainer2.Orientation == Orientation.Vertical)
            {
                //ver -> h
                this.splitContainer2.Orientation = Orientation.Horizontal;
                this.splitContainer2.SplitterDistance = this.splitContainer2.Height / 2;
            }
            else
            {
                //h->ver
                this.splitContainer2.Orientation = Orientation.Vertical;
                this.splitContainer2.SplitterDistance = this.splitContainer2.Width / 2;
            }
        }

        private void layoutButton_Click(object sender, EventArgs e)
        {
            if (!isPanel2Blank())
            {
                changeSplitPanel2();
                resizeConnection();
            }
            else
            {
                if (m_window.Count > 1)
                {
                    splitToolStripMenuItem_Click(null, null);
                }
            }
        }

        private void resizeConnection()
        {
            this.splitContainer2.Visible = false;
            try
            {
                ArrayList keyList = new ArrayList();
                foreach (string key1 in m_window.Keys)
                {
                    keyList.Add(key1);
                }
                foreach (string key in keyList)
                {
                    WindowConnection winConnection = (WindowConnection)m_window[key];
                    if (IsWindow(winConnection.hWnd) != 0)
                    {
                        ShowWindow(winConnection.hWnd, 1);
                        ShowWindow(winConnection.hWnd, 3);
                    }
                }
            }
            catch (Exception)
            {
            }
            this.splitContainer2.Visible = true;
        }

        private bool isPanel2Blank()
        {
            return isOnePanelBlank("", "panel2");
        }

        private bool isOnePanelBlank(string panel1, string panel2)
        {
            bool panel1Blank = true;
            bool panel2Blank = true;
            try
            {
                ArrayList keyList = new ArrayList();
                foreach (string key1 in m_window.Keys)
                {
                    keyList.Add(key1);
                }
                foreach (string key in keyList)
                {
                    WindowConnection winConnection = (WindowConnection)m_window[key];
                    if (winConnection.panel == panel1)
                    {
                        panel1Blank = false;
                    }
                    else if (winConnection.panel == panel2)
                    {
                        panel2Blank = false;
                    }
                }
            }
            catch (Exception)
            {
            }
            if (panel1 == "")
            {
                return panel2Blank;
            }
            else if (panel2 == "")
            {
                return panel1Blank;
            }
            if (panel1Blank || panel2Blank)
            {
                return true;
            }
            return false;
        }

        private void movePanel2ToPanel1()
        {
            try
            {
                ArrayList keyList = new ArrayList();
                foreach (string key1 in m_window.Keys)
                {
                    keyList.Add(key1);
                }
                foreach (string key in keyList)
                {
                    WindowConnection winConnection = (WindowConnection)m_window[key];
                    if (winConnection.panel == "panel2")
                    {
                        SetParent(winConnection.hWnd, this.splitContainer2.Panel1.Handle);
                        winConnection.panel = "panel1";
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private WindowConnection getLastWindow()
        {
            try
            {
                foreach (string key1 in m_window.Keys)
                {
                    WindowConnection windowData = (WindowConnection)m_window[key1];
                    if (windowData.hWnd == m_lastWin)
                    {
                        return windowData;
                    }
                }
            }
            catch (Exception exp)
            {
            }
            return null;
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            ConnectionData puttyData = (ConnectionData)m_server[node.Text];
            if (puttyData != null)
            {
                createNewConnection(puttyData);
                return;
            }

            WindowConnection windowData = (WindowConnection)m_window[node.Text];
            if (windowData != null)
            {
                SetForegroundWindow(windowData.hWnd);
                m_lastWin = windowData.hWnd;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr active = GetForegroundWindow();
            ArrayList keyList = new ArrayList();
            foreach (string key1 in m_window.Keys)
            {
                keyList.Add(key1);
            }
            foreach (string key in keyList)
            {
                WindowConnection winConnection = (WindowConnection)m_window[key];
                if (winConnection.hWnd == active)
                {
                    if (winConnection.hWnd != m_lastWin)
                    {
                        m_lastWin = winConnection.hWnd;                       
                    }
                    tabControl1.SelectedTab = winConnection.tabPage; 
                }
                else if (IsWindow(winConnection.hWnd) == 0)
                {
                    m_lastWin = IntPtr.Zero;
                    tabControl1.TabPages.Remove(winConnection.tabPage);
                    m_window.Remove(key);
                }
            }
            if (!this.splitContainer2.Panel2Collapsed)
            {
                if (isOnePanelBlank("panel1", "panel2"))
                {
                    movePanel2ToPanel1();
                    this.splitContainer2.Panel2Collapsed = true;
                    resizeConnection();
                }
            }
        }


        private void splitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowConnection winConnection = getLastWindow();
            if (winConnection != null)
            {
                if (this.splitContainer2.Panel2Collapsed)
                {
                    //unv-> ver                
                    this.splitContainer2.Panel2Collapsed = false;
                }
                try
                {
                    if (winConnection.panel == "panel1")
                    {
                        SetParent(winConnection.hWnd, this.splitContainer2.Panel2.Handle);
                        winConnection.panel = "panel2";
                    }
                    else
                    {
                        SetParent(winConnection.hWnd, this.splitContainer2.Panel1.Handle);
                        winConnection.panel = "panel1";
                    }
                    resizeConnection();
                }
                catch (Exception exp) {
                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                WindowConnection windowData = (WindowConnection)m_window[tabControl1.TabPages[tabControl1.SelectedIndex].Text];
                if (windowData != null)
                {
                    SetForegroundWindow(windowData.hWnd);
                    m_lastWin = windowData.hWnd;
                }
            }
        }
        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeConnection();
        }

        private ConnectionData getQuickData()
        {
            ConnectionData puttyData = new ConnectionData();
            puttyData.name = txtIp.Text;
            puttyData.ip = txtIp.Text;
            puttyData.port = txtPort.Text;
            if (puttyData.port == "23")
            {
                puttyData.protocol = "TELNET";
            }
            else
            {
                puttyData.protocol = "SSH";
            }
            puttyData.username = txtUser.Text;
            puttyData.password = txtPassword.Text;
            puttyData.keyfile = "ssh.ppk";
            puttyData.parent = "Default";
            puttyData.quickType = "";
            puttyData.command = "";
            puttyData.waitTime = "2000";            
            return puttyData;
        }

        private void btnSaveQuick_Click(object sender, EventArgs e)
        {
            ConnectionData puttyData = getQuickData();
            if (puttyData.ip == "")
            {
                return;
            }
            m_server[puttyData.name] = puttyData;
            bool find = false;
            TreeNode temp = new TreeNode(puttyData.name);
            TreeNode parent = getParent(puttyData.parent);
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                if (parent.Nodes[i].Text == txtIp.Text)
                {
                    find = true;
                }
            }
            if (!find)
            {
                parent.Nodes.Add(temp);
                parent.ExpandAll();
            }
            saveConfig(m_server, "server.ini");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            ConnectionData puttyData = getQuickData();
            if (puttyData.ip == "")
            {
                return;
            }
            createNewConnection(puttyData);
        }

        private void closeActive(object sender, EventArgs e)
        {
            WindowConnection windowdata = (WindowConnection)m_window[tabControl1.TabPages[tabControl1.SelectedIndex].Text];
            if (windowdata != null)
            {
                SendMessage(windowdata.hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private void copyActive(object sender, EventArgs e)
        {

            WindowConnection windowdata = (WindowConnection)m_window[tabControl1.TabPages[tabControl1.SelectedIndex].Text];
            if (windowdata != null)
            {
                createNewConnection(windowdata.connection);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (m_iPopWin != IntPtr.Zero)
            {
                if (IsWindow(m_iPopWin) == 0)
                {
                    m_iPopWin = IntPtr.Zero;
                }
            }
            try
            {
                if (m_iPopWin != IntPtr.Zero)
                {
                    SetForegroundWindow(m_iPopWin);
                }
                else
                {
                    string installPath = @"ipop.exe";
                    Process process = Process.Start(installPath);
                    process.WaitForInputIdle();
                    m_iPopWin = process.MainWindowHandle;
                }                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ColorSelectForm colorForm = new ColorSelectForm();
            colorForm.setColor(defaultColor);
            DialogResult result = colorForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                defaultColor = colorForm.selectColor;
                saveKey();
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            resizeConnection();
        }

        private void winscpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(null, null);
        }

    }
}
