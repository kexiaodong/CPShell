namespace CPShell
{
    partial class QuickForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resultList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.keydata = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.keyname = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resultList
            // 
            this.resultList.FormattingEnabled = true;
            this.resultList.Location = new System.Drawing.Point(2, 3);
            this.resultList.Name = "resultList";
            this.resultList.Size = new System.Drawing.Size(120, 186);
            this.resultList.TabIndex = 0;
            this.resultList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // keydata
            // 
            this.keydata.Location = new System.Drawing.Point(129, 30);
            this.keydata.Multiline = true;
            this.keydata.Name = "keydata";
            this.keydata.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.keydata.Size = new System.Drawing.Size(340, 211);
            this.keydata.TabIndex = 2;
            this.keydata.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(196, 247);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // keyname
            // 
            this.keyname.Location = new System.Drawing.Point(129, 4);
            this.keyname.Name = "keyname";
            this.keyname.Size = new System.Drawing.Size(340, 20);
            this.keyname.TabIndex = 1;
            this.keyname.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 218);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // QuickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 282);
            this.Controls.Add(this.keyname);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.keydata);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resultList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Command";
            this.Load += new System.EventHandler(this.QuickForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox keydata;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox keyname;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.ListBox resultList;
    }
}