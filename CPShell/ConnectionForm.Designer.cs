namespace CPShell
{
    partial class ConnectionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.c_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.c_ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.c_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.c_username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.c_password = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.c_keyfile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioSSH = new System.Windows.Forms.RadioButton();
            this.radioTelnet = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.c_parent = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.c_command = new System.Windows.Forms.TextBox();
            this.c_scriptType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.c_waitTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.c_color = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // c_name
            // 
            this.c_name.Location = new System.Drawing.Point(94, 8);
            this.c_name.Name = "c_name";
            this.c_name.Size = new System.Drawing.Size(203, 21);
            this.c_name.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "IP";
            // 
            // c_ip
            // 
            this.c_ip.Location = new System.Drawing.Point(94, 54);
            this.c_ip.Name = "c_ip";
            this.c_ip.Size = new System.Drawing.Size(203, 21);
            this.c_ip.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Port";
            // 
            // c_port
            // 
            this.c_port.Location = new System.Drawing.Point(94, 78);
            this.c_port.Name = "c_port";
            this.c_port.Size = new System.Drawing.Size(203, 21);
            this.c_port.TabIndex = 10;
            this.c_port.Text = "22";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Protocol";
            // 
            // c_username
            // 
            this.c_username.Location = new System.Drawing.Point(94, 125);
            this.c_username.Name = "c_username";
            this.c_username.Size = new System.Drawing.Size(203, 21);
            this.c_username.TabIndex = 13;
            this.c_username.Text = "root";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "UserName";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Password";
            // 
            // c_password
            // 
            this.c_password.Location = new System.Drawing.Point(94, 149);
            this.c_password.Name = "c_password";
            this.c_password.Size = new System.Drawing.Size(203, 21);
            this.c_password.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Keyfile";
            // 
            // c_keyfile
            // 
            this.c_keyfile.Location = new System.Drawing.Point(94, 173);
            this.c_keyfile.Name = "c_keyfile";
            this.c_keyfile.Size = new System.Drawing.Size(203, 21);
            this.c_keyfile.TabIndex = 15;
            this.c_keyfile.Text = "ssh.ppk";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 25;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioSSH
            // 
            this.radioSSH.AutoSize = true;
            this.radioSSH.Checked = true;
            this.radioSSH.Location = new System.Drawing.Point(95, 102);
            this.radioSSH.Name = "radioSSH";
            this.radioSSH.Size = new System.Drawing.Size(41, 16);
            this.radioSSH.TabIndex = 11;
            this.radioSSH.TabStop = true;
            this.radioSSH.Text = "SSH";
            this.radioSSH.UseVisualStyleBackColor = true;
            this.radioSSH.Click += new System.EventHandler(this.radioSSH_Click);
            // 
            // radioTelnet
            // 
            this.radioTelnet.AutoSize = true;
            this.radioTelnet.Location = new System.Drawing.Point(165, 103);
            this.radioTelnet.Name = "radioTelnet";
            this.radioTelnet.Size = new System.Drawing.Size(59, 16);
            this.radioTelnet.TabIndex = 12;
            this.radioTelnet.Text = "TELNET";
            this.radioTelnet.UseVisualStyleBackColor = true;
            this.radioTelnet.Click += new System.EventHandler(this.radioTelnet_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Folder";
            // 
            // c_parent
            // 
            this.c_parent.FormattingEnabled = true;
            this.c_parent.Location = new System.Drawing.Point(95, 30);
            this.c_parent.Name = "c_parent";
            this.c_parent.Size = new System.Drawing.Size(202, 20);
            this.c_parent.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "Commands";
            // 
            // c_command
            // 
            this.c_command.Location = new System.Drawing.Point(98, 269);
            this.c_command.Multiline = true;
            this.c_command.Name = "c_command";
            this.c_command.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.c_command.Size = new System.Drawing.Size(202, 61);
            this.c_command.TabIndex = 24;
            // 
            // c_scriptType
            // 
            this.c_scriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_scriptType.FormattingEnabled = true;
            this.c_scriptType.Location = new System.Drawing.Point(94, 224);
            this.c_scriptType.Name = "c_scriptType";
            this.c_scriptType.Size = new System.Drawing.Size(202, 20);
            this.c_scriptType.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(96, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "Wait Millisecond";
            // 
            // c_waitTime
            // 
            this.c_waitTime.Location = new System.Drawing.Point(217, 247);
            this.c_waitTime.Name = "c_waitTime";
            this.c_waitTime.Size = new System.Drawing.Size(79, 21);
            this.c_waitTime.TabIndex = 23;
            this.c_waitTime.Text = "5000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 201);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "Color";
            // 
            // c_color
            // 
            this.c_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_color.FormattingEnabled = true;
            this.c_color.Items.AddRange(new object[] {
            "Default",
            "Black",
            "White",
            "Yellow",
            "Green"});
            this.c_color.Location = new System.Drawing.Point(94, 198);
            this.c_color.Name = "c_color";
            this.c_color.Size = new System.Drawing.Size(202, 20);
            this.c_color.TabIndex = 16;
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 358);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.c_command);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.c_scriptType);
            this.Controls.Add(this.c_color);
            this.Controls.Add(this.c_parent);
            this.Controls.Add(this.radioTelnet);
            this.Controls.Add(this.radioSSH);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.c_waitTime);
            this.Controls.Add(this.c_keyfile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.c_password);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.c_username);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.c_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.c_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.c_name);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.Connection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox c_name;
        public System.Windows.Forms.TextBox c_ip;
        public System.Windows.Forms.TextBox c_port;
        public System.Windows.Forms.TextBox c_username;
        public System.Windows.Forms.TextBox c_password;
        public System.Windows.Forms.TextBox c_keyfile;
        private System.Windows.Forms.RadioButton radioSSH;
        private System.Windows.Forms.RadioButton radioTelnet;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox c_parent;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox c_command;
        public System.Windows.Forms.ComboBox c_scriptType;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox c_waitTime;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox c_color;
    }
}