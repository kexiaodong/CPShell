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

        public const int WM_CHAR = 0x0102;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        public static extern int IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern int ShowWindow(IntPtr hWnd, uint nCmdShow);
        #endregion
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2MinSize = 0;
            toolStripButton1_Click(null, null);

            m_nodes["Servers"] = new TreeNode("Servers");
            this.treeView1.Nodes.Add((TreeNode)m_nodes["Servers"]);

            loadServer();
            loadKey();
            reloadKeyButton();
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
        }

        private void layoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                int count = m_window.Keys.Count;
                int width = 0;
                int height = 0;
                switch (count)
                {
                    case 2:
                        width = this.Width - this.treeView1.Width - 10;
                        height = (this.Height - this.toolStrip1.Height - this.tabControl1.Height - 28) / 2;
                        foreach (string key1 in m_window.Keys)
                        {
                            WindowConnection windowData = (WindowConnection)m_window[key1];
                            ShowWindow(windowData.hWnd, 1);
                            MoveWindow(windowData.hWnd, 0, index * height, width, height, true);                            
                            index++;
                        }
                        break;
                    case 3:
                        width = (this.Width - this.treeView1.Width - 10)/2;
                        height = (this.Height - this.toolStrip1.Height - this.tabControl1.Height - 28) / 2;
                        foreach (string key1 in m_window.Keys)
                        {
                            WindowConnection windowData = (WindowConnection)m_window[key1];
                            ShowWindow(windowData.hWnd, 1);
                            MoveWindow(windowData.hWnd, index % 2 * width, index / 2 * height, (index / 2 + 1) * width, height, true);                                                       
                            index++;
                        }
                        break;
                    case 4:
                        width = (this.Width - this.treeView1.Width - 10)/2;
                        height = (this.Height - this.toolStrip1.Height - this.tabControl1.Height - 28) / 2;
                        foreach (string key1 in m_window.Keys)
                        {
                            WindowConnection windowData = (WindowConnection)m_window[key1];
                            ShowWindow(windowData.hWnd, 1);
                            MoveWindow(windowData.hWnd, index % 2 * width, index / 2 * height, width, height, true);                                                       
                            index++;
                        }
                        break;
                    default:
                        foreach (string key1 in m_window.Keys)
                        {
                            WindowConnection windowData = (WindowConnection)m_window[key1];
                            ShowWindow(windowData.hWnd, 1);
                        }
                        break;
                }             

            }
            catch (Exception exp)
            {
            }
        }

        #region Key
        private ArrayList m_quickDatas = new ArrayList();
        private int m_keyButtonIndex = 0;
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
            while (this.toolStrip1.Items.Count > 5)
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
                    SetForegroundWindow(m_lastWin);
                }
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
                    if (input.StartsWith("["))
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
        private Hashtable m_nodes = new Hashtable();
        private Hashtable m_server = new Hashtable();

        private void loadServer()
        {
            m_server = loadConfig("server.ini");
            getParent("Default");
            foreach (string key in m_server.Keys)
            {
                ConnectionData puttyData = (ConnectionData)m_server[key];
                if (puttyData.parent == null || puttyData.parent == "")
                {
                    puttyData.parent = "Default";
                }
                TreeNode parent = getParent(puttyData.parent);
                parent.Nodes.Add(puttyData.name);
            }
            this.treeView1.SelectedNode = (TreeNode)m_nodes["Servers"];
            this.treeView1.ExpandAll();
        }

        private void saveConfig(Hashtable obj, string dataFile)
        {
            FileStream stream = new FileStream(dataFile, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        private Hashtable loadConfig(string dataFile)
        {
            Hashtable obj = null;
            try
            {
                FileStream readstream = new FileStream(dataFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                obj = (Hashtable)formatter.Deserialize(readstream);
                readstream.Close();
            }
            catch (Exception)
            {
                obj = new Hashtable();
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
            ConnectionForm connection = new ConnectionForm((TreeNode)m_nodes["Servers"], m_quickDatas);
            if (connection.ShowDialog() == DialogResult.OK)
            {
                ConnectionData puttyData = Dlg2Data(connection);
                m_server[puttyData.name] = puttyData;

                TreeNode temp = new TreeNode(puttyData.name);
                TreeNode parent = getParent(puttyData.parent);
                parent.Nodes.Add(temp);
                parent.ExpandAll();

                saveConfig(m_server, "server.ini");
            }
        }

        private void deleteServer_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                ConnectionData puttyData = (ConnectionData)m_server[node.Text];
                TreeNode temp = getNode(puttyData.parent, puttyData.name);
                if (temp != null)
                {
                    m_server.Remove(puttyData.name);
                    TreeNode parent = getParent(puttyData.parent);
                    parent.Nodes.Remove(temp);
                    saveConfig(m_server, "server.ini");
                }                
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
                ConnectionForm connection = new ConnectionForm((TreeNode)m_nodes["Servers"], m_quickDatas);
                connection.setData(puttyData);
                string oldParent = puttyData.parent;
                string oldName = puttyData.name;
                if (connection.ShowDialog() == DialogResult.OK)
                {
                    puttyData = Dlg2Data(connection);
                    m_server[puttyData.name] = puttyData;
                    saveConfig(m_server, "server.ini");
                    
                    if (oldName != puttyData.name)
                    {
                        //改名新建
                        TreeNode temp = new TreeNode(puttyData.name);
                        TreeNode parentNode = getParent(puttyData.parent);
                        parentNode.Nodes.Add(temp);
                        parentNode.ExpandAll();
                    }
                    else
                    {
                        //改组
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
        private Hashtable m_window = new Hashtable();
        private int m_windowIndex = 0;

        private void toolStripButton2_Click(object sender, EventArgs e)
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

            string installPath = @"putty.exe";
            Process process = Process.Start(installPath, param);
            process.WaitForInputIdle();

            // 嵌入到parentHandle指定的句柄中
            IntPtr appWin = process.MainWindowHandle;
            SetParent(appWin, this.panel2.Handle);
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

            WindowConnection winConnection = new WindowConnection(windowName, puttyData, appWin, tabPage);
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
                    this.m_quickDatas, 
                    puttyData.waitTime);
                Thread nonParameterThread = new Thread(new ThreadStart(after.run));
                nonParameterThread.Start();
            }
            else if (puttyData.command != null && puttyData.command.Trim() != "")
            {
                AfterLogin after = new AfterLogin(appWin,
                    puttyData.command, 
                    puttyData.waitTime);
                Thread nonParameterThread = new Thread(new ThreadStart(after.run));
                nonParameterThread.Start();
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

        IntPtr m_lastWin = IntPtr.Zero;
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


    }
}
