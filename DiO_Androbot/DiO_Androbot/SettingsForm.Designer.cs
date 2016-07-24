namespace DiO_Androbot
{
    partial class SettingsForm
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
            this.tbBTID = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblBTID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbBTID
            // 
            this.tbBTID.Location = new System.Drawing.Point(136, 99);
            this.tbBTID.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbBTID.Name = "tbBTID";
            this.tbBTID.Size = new System.Drawing.Size(180, 27);
            this.tbBTID.TabIndex = 9;
            this.tbBTID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(136, 59);
            this.tbPort.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(180, 27);
            this.tbPort.TabIndex = 8;
            this.tbPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(136, 19);
            this.tbAddress.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(180, 27);
            this.tbAddress.TabIndex = 7;
            this.tbAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(16, 22);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(107, 20);
            this.lblAddress.TabIndex = 10;
            this.lblAddress.Text = "IP Address:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(16, 62);
            this.lblPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(50, 20);
            this.lblPort.TabIndex = 11;
            this.lblPort.Text = "Port:";
            // 
            // lblBTID
            // 
            this.lblBTID.AutoSize = true;
            this.lblBTID.Location = new System.Drawing.Point(16, 102);
            this.lblBTID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBTID.Name = "lblBTID";
            this.lblBTID.Size = new System.Drawing.Size(58, 20);
            this.lblBTID.TabIndex = 12;
            this.lblBTID.Text = "BTID:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 141);
            this.Controls.Add(this.lblBTID);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.tbBTID);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbAddress);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Setings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SetingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbBTID;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblBTID;
    }
}