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
            lblTitle = new Label();
            btnScan = new Button();
            dgvWiFiResults = new DataGridView();
            lblStatus = new Label();
            progressBarScan = new ProgressBar();
            txtMinSignalStrength = new TextBox();
            cmbEncryptionType = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvWiFiResults).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BackColor = SystemColors.ActiveCaption;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(560, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Wi-Fi Network Scanner";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnScan
            // 
            btnScan.Font = new Font("Segoe UI", 12F);
            btnScan.Location = new Point(430, 62);
            btnScan.Name = "btnScan";
            btnScan.Size = new Size(142, 35);
            btnScan.TabIndex = 2;
            btnScan.Text = "Start Scan";
            btnScan.UseVisualStyleBackColor = true;
            btnScan.Click += btnScan_Click;
            // 
            // dgvWiFiResults
            // 
            dgvWiFiResults.AllowUserToAddRows = false;
            dgvWiFiResults.AllowUserToDeleteRows = false;
            dgvWiFiResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvWiFiResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWiFiResults.Location = new Point(15, 110);
            dgvWiFiResults.Name = "dgvWiFiResults";
            dgvWiFiResults.ReadOnly = true;
            dgvWiFiResults.Size = new Size(557, 300);
            dgvWiFiResults.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 420);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(64, 15);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status: Idle";
            // 
            // progressBarScan
            // 
            progressBarScan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBarScan.Location = new Point(400, 415);
            progressBarScan.Name = "progressBarScan";
            progressBarScan.Size = new Size(172, 23);
            progressBarScan.TabIndex = 5;
            // 
            // txtMinSignalStrength
            // 
            txtMinSignalStrength.Font = new Font("Segoe UI", 12F);
            txtMinSignalStrength.Location = new Point(15, 62);
            txtMinSignalStrength.Name = "txtMinSignalStrength";
            txtMinSignalStrength.PlaceholderText = "Min Signal Strength (dBm)";
            txtMinSignalStrength.Size = new Size(200, 29);
            txtMinSignalStrength.TabIndex = 6;
            // 
            // cmbEncryptionType
            // 
            cmbEncryptionType.Font = new Font("Segoe UI", 12F);
            cmbEncryptionType.FormattingEnabled = true;
            cmbEncryptionType.Items.AddRange(new object[] { "All", "WEP", "WPA", "WPA2", "None" });
            cmbEncryptionType.Location = new Point(230, 62);
            cmbEncryptionType.Name = "cmbEncryptionType";
            cmbEncryptionType.Size = new Size(170, 29);
            cmbEncryptionType.TabIndex = 7;
            // 
            // WiFiScannerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 450);
            Controls.Add(cmbEncryptionType);
            Controls.Add(txtMinSignalStrength);
            Controls.Add(progressBarScan);
            Controls.Add(lblStatus);
            Controls.Add(dgvWiFiResults);
            Controls.Add(btnScan);
            Controls.Add(lblTitle);
            MinimumSize = new Size(600, 489);
            Name = "WiFiScannerForm";
            Text = "Wi-Fi Network Scanner";
            ((System.ComponentModel.ISupportInitialize)dgvWiFiResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
