namespace TDM_IP_Tracker
{
    partial class WiFiScannerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.DataGridView dgvWiFiResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBarScan;
        private System.Windows.Forms.TextBox txtMinSignalStrength;
        private System.Windows.Forms.ComboBox cmbEncryptionType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.dgvWiFiResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBarScan = new System.Windows.Forms.ProgressBar();
            this.txtMinSignalStrength = new System.Windows.Forms.TextBox();
            this.cmbEncryptionType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWiFiResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(560, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Wi-Fi Network Scanner";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnScan.Location = new System.Drawing.Point(430, 62);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(142, 35);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "Start Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // dgvWiFiResults
            // 
            this.dgvWiFiResults.AllowUserToAddRows = false;
            this.dgvWiFiResults.AllowUserToDeleteRows = false;
            this.dgvWiFiResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                           | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWiFiResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWiFiResults.Location = new System.Drawing.Point(15, 110);
            this.dgvWiFiResults.Name = "dgvWiFiResults";
            this.dgvWiFiResults.ReadOnly = true;
            this.dgvWiFiResults.RowTemplate.Height = 25;
            this.dgvWiFiResults.Size = new System.Drawing.Size(557, 300);
            this.dgvWiFiResults.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 420);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status: Idle";
            // 
            // progressBarScan
            // 
            this.progressBarScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarScan.Location = new System.Drawing.Point(400, 415);
            this.progressBarScan.Name = "progressBarScan";
            this.progressBarScan.Size = new System.Drawing.Size(172, 23);
            this.progressBarScan.TabIndex = 5;
            // 
            // txtMinSignalStrength
            // 
            this.txtMinSignalStrength.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtMinSignalStrength.Location = new System.Drawing.Point(15, 62);
            this.txtMinSignalStrength.Name = "txtMinSignalStrength";
            this.txtMinSignalStrength.Size = new System.Drawing.Size(200, 29);
            this.txtMinSignalStrength.TabIndex = 6;
            this.txtMinSignalStrength.PlaceholderText = "Min Signal Strength (dBm)";
            // 
            // cmbEncryptionType
            // 
            this.cmbEncryptionType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbEncryptionType.FormattingEnabled = true;
            this.cmbEncryptionType.Items.AddRange(new object[] { "All", "WEP", "WPA", "WPA2", "None" });
            this.cmbEncryptionType.Location = new System.Drawing.Point(230, 62);
            this.cmbEncryptionType.Name = "cmbEncryptionType";
            this.cmbEncryptionType.Size = new System.Drawing.Size(170, 29);
            this.cmbEncryptionType.TabIndex = 7;
            this.cmbEncryptionType.SelectedIndex = 0;
            // 
            // WiFiScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.cmbEncryptionType);
            this.Controls.Add(this.txtMinSignalStrength);
            this.Controls.Add(this.progressBarScan);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvWiFiResults);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(600, 489);
            this.Name = "WiFiScannerForm";
            this.Text = "Wi-Fi Network Scanner";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWiFiResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
