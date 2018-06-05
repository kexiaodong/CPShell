using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CPShell
{
    public partial class QuickForm : Form
    {
        public ArrayList baseKeys;
        public QuickForm(ArrayList keys)
        {
            this.baseKeys = keys;
            InitializeComponent();
        }

        private void QuickForm_Load(object sender, EventArgs e)
        {
            foreach (QuickData data in this.baseKeys)
            {
                QuickData temp = new QuickData(data.name, data.data);
                this.resultList.Items.Add(temp);
            }
        }

        int lastIndex = -1;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((resultList.SelectedIndex != -1) && (resultList.SelectedIndex != lastIndex))
            {                
                QuickData select = (QuickData)resultList.SelectedItem;
                this.keyname.Text = select.name;
                this.keydata.Text = select.data;
                lastIndex = resultList.SelectedIndex;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((resultList.SelectedIndex != -1) && (resultList.SelectedIndex == lastIndex))
            {
                ((QuickData)resultList.SelectedItem).name = this.keyname.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((resultList.SelectedIndex != -1) && (resultList.SelectedIndex == lastIndex))
            {
                ((QuickData)resultList.SelectedItem).data = this.keydata.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuickData newData = new QuickData("+ new +", "");
            this.resultList.Items.Add(newData);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (resultList.SelectedIndex != -1)
            {
                lastIndex = -1;
                resultList.Items.Remove(resultList.SelectedItem);
                this.keydata.Text = "";
                this.keyname.Text = "";
            }
        }
    }
}
