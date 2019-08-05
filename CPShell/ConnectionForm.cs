using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace CPShell
{
    public partial class ConnectionForm : Form
    {
        public string c_protocol = "SSH";
        private Hashtable m_server = null;
        private string lastName = "";
        public ConnectionForm(TreeNode rootNode, ArrayList quickData, Hashtable server)
        {
            InitializeComponent();
            this.m_server = server;
            for (int i = 0; i < rootNode.Nodes.Count; i++)
            {
                c_parent.Items.Add(rootNode.Nodes[i].Text);
            }
            c_scriptType.Items.Add("Input Command");
            foreach (QuickData data in quickData)
            {
                c_scriptType.Items.Add(data.name);
            }
            c_scriptType.SelectedIndex = 0;
            c_color.SelectedIndex = 0;
        }

        private void Connection_Load(object sender, EventArgs e)
        {
         
        }


        public void setData(ConnectionData puttyData)
        {
            this.lastName = puttyData.name;
            this.c_name.Text = puttyData.name;
            this.c_ip.Text = puttyData.ip;
            this.c_port.Text = puttyData.port;
            c_protocol = puttyData.protocol;
            this.c_username.Text = puttyData.username;
            this.c_password.Text = puttyData.password;
            this.c_keyfile.Text = puttyData.keyfile;
            if (c_protocol == "SSH")
            {
                this.radioSSH.Checked = true;
                this.radioTelnet.Checked = false;
            }
            else
            {
                this.radioSSH.Checked = false;
                this.radioTelnet.Checked = true;
            }
            //c_parent
            for (int i = 0; i < c_parent.Items.Count; i++)
            {
                if (c_parent.Items[i].ToString() == puttyData.parent)
                {
                    c_parent.SelectedIndex = i;
                    break;
                }
            }
            //c_scriptType
            c_scriptType.SelectedIndex = 0;
            for (int i = 0; i < c_scriptType.Items.Count; i++)
            {
                if (c_scriptType.Items[i].ToString() == puttyData.quickType)
                {
                    c_scriptType.SelectedIndex = i;
                    break;
                }
            }
            c_command.Text = puttyData.command;
            c_waitTime.Text = puttyData.waitTime;
            int selectIndex = Convert.ToInt32(puttyData.color);
            try
            {
                c_color.SelectedIndex = selectIndex;
            }
            catch (Exception)
            {
                c_color.SelectedIndex = 0;
            }
        }

        private void radioSSH_Click(object sender, EventArgs e)
        {
            this.c_protocol = "SSH";
        }

        private void radioTelnet_Click(object sender, EventArgs e)
        {
            this.c_protocol = "TELNET";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool needConfirm = false;
            if (this.lastName != "")
            {
                if (c_name.Text != this.lastName)
                {
                    if (m_server.ContainsKey(c_name.Text))
                    {
                        needConfirm = true;
                    }
                }
            }
            else if (m_server.ContainsKey(c_name.Text))
            {
                needConfirm = true;
            }
            if (needConfirm)
            {
                DialogResult result = MessageBox.Show(
                            "Duplicate server " + c_name.Text + ", replace?", "Confirm", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
