using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPShell
{
    public partial class ColorSelectForm : Form
    {
        public string selectColor = "";
        public void setColor(string defaultColor)
        {
            if (defaultColor == "1")
            {
                this.labResult.Text = "Current: Black";
            }
            else if (defaultColor == "2")
            {
                this.labResult.Text = "Current: White";
            }
            else if (defaultColor == "3")
            {
                this.labResult.Text = "Current: Yellow";
            }
            else if (defaultColor == "4")
            {
                this.labResult.Text = "Current: Green";
            }
        }

        public ColorSelectForm()
        {
            InitializeComponent();
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            selectColor = "1";
            this.DialogResult = DialogResult.OK;
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            selectColor = "2";
            this.DialogResult = DialogResult.OK;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            selectColor = "3";
            this.DialogResult = DialogResult.OK;
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            selectColor = "4";
            this.DialogResult = DialogResult.OK;
        }
    }
}
