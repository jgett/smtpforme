namespace SmtpForMe
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LinkWebInterface = new System.Windows.Forms.LinkLabel();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LinkWebInterface);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Web Interface";
            // 
            // LinkWebInterface
            // 
            this.LinkWebInterface.AutoSize = true;
            this.LinkWebInterface.Location = new System.Drawing.Point(18, 25);
            this.LinkWebInterface.Name = "LinkWebInterface";
            this.LinkWebInterface.Size = new System.Drawing.Size(23, 13);
            this.LinkWebInterface.TabIndex = 1;
            this.LinkWebInterface.TabStop = true;
            this.LinkWebInterface.Text = "link";
            this.LinkWebInterface.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkWebInterface_LinkClicked);
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(13, 75);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(252, 23);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(12, 104);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(252, 23);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 137);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "SmtpForMe";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel LinkWebInterface;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnExit;
    }
}

