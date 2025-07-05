namespace TDM_IP_Tracker
{
    partial class SingleIPForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.CheckBox chkAutoPing;
        private System.Windows.Forms.NumericUpDown numPingInterval;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.DataGridView dataGridPingResults;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer autoPingTimer;
        private System.Windows.Forms.Label lblResponseTime;
        private System.Windows.Forms.Label lblPacketLoss;
        private System.Windows.Forms.Label lblLastSeen;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.ComboBox cmbPingCount;
        private System.Windows.Forms.Label lblPingCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                autoPingTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.chkAutoPing = new System.Windows.Forms.CheckBox();
            this.numPingInterval = new System.Windows.Forms.NumericUpDown();
            this.lblInterval = new System.Windows.Forms.Label();
            this.dataGridPingResults = new System.Windows.Forms.DataGridView();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.autoPingTimer = new System.Windows.Forms.Timer(this.components);
            this.lblResponseTime = new System.Windows.Forms.Label();
            this.lblPacketLoss = new System.Windows.Forms.Label();
            this.lblLastSeen = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.cmbPingCount = new System.Windows.Forms.ComboBox();
            this.lblPingCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPingInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPingResults)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPrompt.Location = new System.Drawing.Point(20, 20);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(133, 15);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Text = "Enter IP or Hostname:";

            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.Color.White;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtIP.Location = new System.Drawing.Point(20, 40);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(250, 23);
            this.txtIP.TabIndex = 1;
            this.txtIP.TextChanged += new System.EventHandler(this.txtIP_TextChanged);

            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(280, 40);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(100, 25);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Ping Now";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);

            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(20, 80);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(5);
            this.lblStatus.Size = new System.Drawing.Size(360, 40);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // chkAutoPing
            // 
            this.chkAutoPing.AutoSize = true;
            this.chkAutoPing.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAutoPing.Location = new System.Drawing.Point(20, 130);
            this.chkAutoPing.Name = "chkAutoPing";
            this.chkAutoPing.Size = new System.Drawing.Size(77, 19);
            this.chkAutoPing.TabIndex = 4;
            this.chkAutoPing.Text = "Auto Ping";
            this.chkAutoPing.UseVisualStyleBackColor = true;
            this.chkAutoPing.CheckedChanged += new System.EventHandler(this.chkAutoPing_CheckedChanged);

            // 
            // numPingInterval
            // 
            this.numPingInterval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numPingInterval.Location = new System.Drawing.Point(170, 130);
            this.numPingInterval.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numPingInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPingInterval.Name = "numPingInterval";
            this.numPingInterval.Size = new System.Drawing.Size(60, 23);
            this.numPingInterval.TabIndex = 5;
            this.numPingInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});

            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInterval.Location = new System.Drawing.Point(100, 132);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(64, 15);
            this.lblInterval.TabIndex = 6;
            this.lblInterval.Text = "Interval (s):";

            // 
            // dataGridPingResults
            // 
            this.dataGridPingResults.AllowUserToAddRows = false;
            this.dataGridPingResults.AllowUserToDeleteRows = false;
            this.dataGridPingResults.AllowUserToResizeRows = false;
            this.dataGridPingResults.BackgroundColor = System.Drawing.Color.White;
            this.dataGridPingResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridPingResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridPingResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridPingResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridPingResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridPingResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridPingResults.EnableHeadersVisualStyles = false;
            this.dataGridPingResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridPingResults.Location = new System.Drawing.Point(20, 220);
            this.dataGridPingResults.Name = "dataGridPingResults";
            this.dataGridPingResults.ReadOnly = true;
            this.dataGridPingResults.RowHeadersVisible = false;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.dataGridPingResults.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridPingResults.RowTemplate.Height = 25;
            this.dataGridPingResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridPingResults.Size = new System.Drawing.Size(560, 200);
            this.dataGridPingResults.TabIndex = 7;

            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnViewDetails.FlatAppearance.BorderSize = 0;
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnViewDetails.ForeColor = System.Drawing.Color.White;
            this.btnViewDetails.Location = new System.Drawing.Point(390, 40);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 25);
            this.btnViewDetails.TabIndex = 8;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);

            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(500, 40);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 25);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 180);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(560, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 10;

            // 
            // autoPingTimer
            // 
            this.autoPingTimer.Interval = 5000;
            this.autoPingTimer.Tick += new System.EventHandler(this.autoPingTimer_Tick);

            // 
            // lblResponseTime
            // 
            this.lblResponseTime.AutoSize = true;
            this.lblResponseTime.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResponseTime.Location = new System.Drawing.Point(10, 10);
            this.lblResponseTime.Name = "lblResponseTime";
            this.lblResponseTime.Size = new System.Drawing.Size(92, 13);
            this.lblResponseTime.TabIndex = 11;
            this.lblResponseTime.Text = "Response: 0 ms";

            // 
            // lblPacketLoss
            // 
            this.lblPacketLoss.AutoSize = true;
            this.lblPacketLoss.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPacketLoss.Location = new System.Drawing.Point(120, 10);
            this.lblPacketLoss.Name = "lblPacketLoss";
            this.lblPacketLoss.Size = new System.Drawing.Size(76, 13);
            this.lblPacketLoss.TabIndex = 12;
            this.lblPacketLoss.Text = "Loss: 0.00%";

            // 
            // lblLastSeen
            // 
            this.lblLastSeen.AutoSize = true;
            this.lblLastSeen.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLastSeen.Location = new System.Drawing.Point(220, 10);
            this.lblLastSeen.Name = "lblLastSeen";
            this.lblLastSeen.Size = new System.Drawing.Size(92, 13);
            this.lblLastSeen.TabIndex = 13;
            this.lblLastSeen.Text = "Last seen: Never";

            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.lblResponseTime);
            this.panelStats.Controls.Add(this.lblLastSeen);
            this.panelStats.Controls.Add(this.lblPacketLoss);
            this.panelStats.Location = new System.Drawing.Point(20, 430);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(560, 35);
            this.panelStats.TabIndex = 14;

            // 
            // cmbPingCount
            // 
            this.cmbPingCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPingCount.FormattingEnabled = true;
            this.cmbPingCount.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16"});
            this.cmbPingCount.Location = new System.Drawing.Point(350, 130);
            this.cmbPingCount.Name = "cmbPingCount";
            this.cmbPingCount.Size = new System.Drawing.Size(60, 23);
            this.cmbPingCount.TabIndex = 15;

            // 
            // lblPingCount
            // 
            this.lblPingCount.AutoSize = true;
            this.lblPingCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPingCount.Location = new System.Drawing.Point(270, 132);
            this.lblPingCount.Name = "lblPingCount";
            this.lblPingCount.Size = new System.Drawing.Size(74, 15);
            this.lblPingCount.TabIndex = 16;
            this.lblPingCount.Text = "Ping Count:";

            // 
            // SingleIPForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 480);
            this.Controls.Add(this.lblPingCount);
            this.Controls.Add(this.cmbPingCount);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.dataGridPingResults);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.numPingInterval);
            this.Controls.Add(this.chkAutoPing);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblPrompt);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleIPForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced IP Tracker";
            this.Load += new System.EventHandler(this.SingleIPForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPingInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPingResults)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}