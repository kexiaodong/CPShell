namespace CPShell
{
    partial class ColorSelectForm
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
            this.btnBlack = new System.Windows.Forms.Button();
            this.btnWhite = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.labResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBlack
            // 
            this.btnBlack.BackColor = System.Drawing.SystemColors.InfoText;
            this.btnBlack.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnBlack.Location = new System.Drawing.Point(2, 23);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(67, 23);
            this.btnBlack.TabIndex = 0;
            this.btnBlack.Text = "Black";
            this.btnBlack.UseVisualStyleBackColor = false;
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnWhite
            // 
            this.btnWhite.BackColor = System.Drawing.SystemColors.Window;
            this.btnWhite.Location = new System.Drawing.Point(75, 23);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(67, 23);
            this.btnWhite.TabIndex = 0;
            this.btnWhite.Text = "White";
            this.btnWhite.UseVisualStyleBackColor = false;
            this.btnWhite.Click += new System.EventHandler(this.btnWhite_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnYellow.Location = new System.Drawing.Point(2, 52);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(67, 23);
            this.btnYellow.TabIndex = 0;
            this.btnYellow.Text = "Yellow";
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnGreen
            // 
            this.btnGreen.BackColor = System.Drawing.Color.Black;
            this.btnGreen.ForeColor = System.Drawing.Color.Green;
            this.btnGreen.Location = new System.Drawing.Point(75, 52);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(67, 23);
            this.btnGreen.TabIndex = 0;
            this.btnGreen.Text = "Green";
            this.btnGreen.UseVisualStyleBackColor = false;
            this.btnGreen.Click += new System.EventHandler(this.btnGreen_Click);
            // 
            // labResult
            // 
            this.labResult.AutoSize = true;
            this.labResult.Location = new System.Drawing.Point(2, 5);
            this.labResult.Name = "labResult";
            this.labResult.Size = new System.Drawing.Size(89, 12);
            this.labResult.TabIndex = 1;
            this.labResult.Text = "Current: Black";
            // 
            // ColorSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(150, 82);
            this.Controls.Add(this.labResult);
            this.Controls.Add(this.btnGreen);
            this.Controls.Add(this.btnYellow);
            this.Controls.Add(this.btnWhite);
            this.Controls.Add(this.btnBlack);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorSelectForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Color Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnBlack;
        public System.Windows.Forms.Button btnWhite;
        public System.Windows.Forms.Button btnYellow;
        public System.Windows.Forms.Button btnGreen;
        public System.Windows.Forms.Label labResult;

    }
}