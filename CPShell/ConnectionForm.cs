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
        public ConnectionForm(TreeNode rootNode, ArrayList quickData)
        {
            InitializeComponent();
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
        }

        private void Connection_Load(object sender, EventArgs e)
        {
         
        }


        public void setData(ConnectionData puttyData)
        {
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
        }

        private void radioSSH_Click(object sender, EventArgs e)
        {
            this.c_protocol = "SSH";
        }

        private void radioTelnet_Click(object sender, EventArgs e)
        {
            this.c_protocol = "TELNET";
        }
    }
}
