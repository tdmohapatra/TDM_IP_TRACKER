namespace TDM_IP_Tracker
{
    partial class NetworkCommandReference
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbCommands;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label lblTitle;

        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbCommands = new System.Windows.Forms.ComboBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // cmbCommands
            // 
            this.cmbCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommands.FormattingEnabled = true;
            this.cmbCommands.Items.AddRange(new object[] {
    "ipconfig",
    "ipconfig /flushdns",
    "ping",
    "tracert",
    "netstat",
    "netstat -b",
    "nslookup",
    "arp",
    "route",
    "hostname",
    "whoami",
    "getmac",
    "netsh",
    "net use",
    "tasklist",
    "systeminfo"
});

            this.cmbCommands.Location = new System.Drawing.Point(30, 55);
            this.cmbCommands.Name = "cmbCommands";
            this.cmbCommands.Size = new System.Drawing.Size(200, 23);
            this.cmbCommands.TabIndex = 0;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(30, 100);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(740, 320);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(250, 55);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(100, 23);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "Show Details";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(370, 55);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(100, 23);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Run Command";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 25);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Network Command Reference";
            // 
            // NetworkCommandReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.cmbCommands);
            this.Controls.Add(this.btnStop);
            this.Name = "NetworkCommandReference";
            this.Text = "Network Command Reference";
            this.Load += new System.EventHandler(this.NetworkCommandReference_Load);
            this.ResumeLayout(false);
            this.PerformLayout();


            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(490, 55);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Enabled = false; // Disabled initially
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // Add btnStop to Controls
            this.Controls.Add(this.btnStop);
        }
    }
}
