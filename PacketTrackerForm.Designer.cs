namespace TDM_IP_Tracker
{
    partial class PacketTrackerForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbAdapters;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ListView listViewPackets;
        private System.Windows.Forms.RichTextBox txtPacketDetails;
        private System.Windows.Forms.RichTextBox txtPayload;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.Label lblTotalPackets;
        private System.Windows.Forms.Label lblBandwidth;
        private System.Windows.Forms.Label lblProtocolStats;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitDetails;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colSourceIP;
        private System.Windows.Forms.ColumnHeader colDestIP;
        private System.Windows.Forms.ColumnHeader colProtocol;
        private System.Windows.Forms.ColumnHeader colSourcePort;
        private System.Windows.Forms.ColumnHeader colDestPort;
        private System.Windows.Forms.ColumnHeader colLength;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            "",
            "",
            ""}, -1);
            this.cmbAdapters = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.listViewPackets = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSourceIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDestIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSourcePort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDestPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitDetails = new System.Windows.Forms.SplitContainer();
            this.txtPacketDetails = new System.Windows.Forms.RichTextBox();
            this.txtPayload = new System.Windows.Forms.RichTextBox();
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.lblTotalPackets = new System.Windows.Forms.Label();
            this.lblBandwidth = new System.Windows.Forms.Label();
            this.lblProtocolStats = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDetails)).BeginInit();
            this.splitDetails.Panel1.SuspendLayout();
            this.splitDetails.Panel2.SuspendLayout();
            this.splitDetails.SuspendLayout();
            this.grpStatistics.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbAdapters
            // 
            this.cmbAdapters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAdapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdapters.FormattingEnabled = true;
            this.cmbAdapters.Location = new System.Drawing.Point(12, 12);
            this.cmbAdapters.Name = "cmbAdapters";
            this.cmbAdapters.Size = new System.Drawing.Size(300, 23);
            this.cmbAdapters.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(255, 76, 175, 80);

            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(318, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Capture";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);

            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(424, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(510, 12);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(200, 23);
            this.txtFilter.TabIndex = 3;
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);

            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(716, 12);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(70, 23);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(255, 255, 152, 0);

            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(792, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // splitMain
            // 
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.Location = new System.Drawing.Point(12, 41);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.listViewPackets);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitDetails);
            this.splitMain.Size = new System.Drawing.Size(860, 500);
            this.splitMain.SplitterDistance = 250;
            this.splitMain.TabIndex = 6;
            // 
            // listViewPackets
            // 
            this.listViewPackets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colSourceIP,
            this.colDestIP,
            this.colProtocol,
            this.colSourcePort,
            this.colDestPort,
            this.colLength});
            this.listViewPackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPackets.FullRowSelect = true;
            this.listViewPackets.GridLines = true;
            this.listViewPackets.HideSelection = false;
            this.listViewPackets.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewPackets.Location = new System.Drawing.Point(0, 0);
            this.listViewPackets.Name = "listViewPackets";
            this.listViewPackets.Size = new System.Drawing.Size(860, 250);
            this.listViewPackets.TabIndex = 0;
            this.listViewPackets.UseCompatibleStateImageBehavior = false;
            this.listViewPackets.View = System.Windows.Forms.View.Details;
            this.listViewPackets.SelectedIndexChanged += new System.EventHandler(this.ListViewPackets_SelectedIndexChanged);
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 120;
            // 
            // colSourceIP
            // 
            this.colSourceIP.Text = "Source IP";
            this.colSourceIP.Width = 120;
            // 
            // colDestIP
            // 
            this.colDestIP.Text = "Destination IP";
            this.colDestIP.Width = 120;
            // 
            // colProtocol
            // 
            this.colProtocol.Text = "Protocol";
            this.colProtocol.Width = 80;
            // 
            // colSourcePort
            // 
            this.colSourcePort.Text = "Source Port";
            this.colSourcePort.Width = 80;
            // 
            // colDestPort
            // 
            this.colDestPort.Text = "Dest Port";
            this.colDestPort.Width = 80;
            // 
            // colLength
            // 
            this.colLength.Text = "Length";
            this.colLength.Width = 80;
            // 
            // splitDetails
            // 
            this.splitDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDetails.Location = new System.Drawing.Point(0, 0);
            this.splitDetails.Name = "splitDetails";
            // 
            // splitDetails.Panel1
            // 
            this.splitDetails.Panel1.Controls.Add(this.txtPacketDetails);
            // 
            // splitDetails.Panel2
            // 
            this.splitDetails.Panel2.Controls.Add(this.grpStatistics);
            this.splitDetails.Panel2.Controls.Add(this.txtPayload);
            this.splitDetails.Size = new System.Drawing.Size(860, 246);
            this.splitDetails.SplitterDistance = 400;
            this.splitDetails.TabIndex = 0;
            // 
            // txtPacketDetails
            // 
            this.txtPacketDetails.BackColor = System.Drawing.Color.White;
            this.txtPacketDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPacketDetails.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacketDetails.Location = new System.Drawing.Point(0, 0);
            this.txtPacketDetails.Name = "txtPacketDetails";
            this.txtPacketDetails.ReadOnly = true;
            this.txtPacketDetails.Size = new System.Drawing.Size(400, 246);
            this.txtPacketDetails.TabIndex = 0;
            this.txtPacketDetails.Text = "";
            // 
            // txtPayload
            // 
            this.txtPayload.BackColor = System.Drawing.Color.White;
            this.txtPayload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPayload.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayload.Location = new System.Drawing.Point(0, 0);
            this.txtPayload.Name = "txtPayload";
            this.txtPayload.ReadOnly = true;
            this.txtPayload.Size = new System.Drawing.Size(456, 246);
            this.txtPayload.TabIndex = 0;
            this.txtPayload.Text = "";
            // 
            // grpStatistics
            // 
            this.grpStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatistics.BackColor = System.Drawing.Color.Transparent;
            this.grpStatistics.Controls.Add(this.lblTotalPackets);
            this.grpStatistics.Controls.Add(this.lblBandwidth);
            this.grpStatistics.Controls.Add(this.lblProtocolStats);
            this.grpStatistics.Location = new System.Drawing.Point(256, 146);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(200, 100);
            this.grpStatistics.TabIndex = 7;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "Live Statistics";
            // 
            // lblTotalPackets
            // 
            this.lblTotalPackets.AutoSize = true;
            this.lblTotalPackets.Location = new System.Drawing.Point(6, 20);
            this.lblTotalPackets.Name = "lblTotalPackets";
            this.lblTotalPackets.Size = new System.Drawing.Size(68, 15);
            this.lblTotalPackets.TabIndex = 0;
            this.lblTotalPackets.Text = "Packets: 0";
            // 
            // lblBandwidth
            // 
            this.lblBandwidth.AutoSize = true;
            this.lblBandwidth.Location = new System.Drawing.Point(6, 45);
            this.lblBandwidth.Name = "lblBandwidth";
            this.lblBandwidth.Size = new System.Drawing.Size(92, 15);
            this.lblBandwidth.TabIndex = 1;
            this.lblBandwidth.Text = "Bandwidth: 0 KB";
            // 
            // lblProtocolStats
            // 
            this.lblProtocolStats.AutoSize = true;
            this.lblProtocolStats.Location = new System.Drawing.Point(6, 70);
            this.lblProtocolStats.Name = "lblProtocolStats";
            this.lblProtocolStats.Size = new System.Drawing.Size(118, 15);
            this.lblProtocolStats.TabIndex = 2;
            this.lblProtocolStats.Text = "TCP: 0, UDP: 0, ICMP: 0";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(878, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Ready";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip.Location = new System.Drawing.Point(0, 544);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(884, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "Packets: 0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel2.Text = "Bandwidth: 0 KB";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel3.Text = "TCP: 0, UDP: 0, ICMP: 0";
            // 
            // PacketTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 566);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbAdapters);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "PacketTrackerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Packet Tracker";
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitDetails.Panel1.ResumeLayout(false);
            this.splitDetails.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDetails)).EndInit();
            this.splitDetails.ResumeLayout(false);
            this.grpStatistics.ResumeLayout(false);
            this.grpStatistics.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}