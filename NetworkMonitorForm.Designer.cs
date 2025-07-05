namespace TDM_IP_Tracker
{
    partial class NetworkMonitorForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbInterfaces;
        private System.Windows.Forms.Label lblInternalIP;
        private System.Windows.Forms.Label lblExternalIP;
        private System.Windows.Forms.Label lblMAC;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPing;
        private System.Windows.Forms.Label lblRealTimeClock;

        private System.Windows.Forms.DataGridView dgvConnections;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnections;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDownload;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUpload;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCPU;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRAM;

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNetworkTraffic;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSystemUsage;

        private System.Windows.Forms.GroupBox groupBoxInterfaceDetails;
        private System.Windows.Forms.GroupBox groupBoxNetworkTraffic;
        private System.Windows.Forms.GroupBox groupBoxSystemUsage;
        private System.Windows.Forms.GroupBox groupBoxConnections;

        private System.Windows.Forms.DataGridViewTextBoxColumn colProtocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemoteAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;

        private System.Windows.Forms.Button btnStopAction;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                sentCounter?.Dispose();
                receivedCounter?.Dispose();
                cpuCounter?.Dispose();
                ramCounter?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {


            // Add default text for detail labels for clarity
            //lblInternalIP.Text = "Internal IP: N/A";
            //lblExternalIP.Text = "External IP: N/A";
            //lblMAC.Text = "MAC: N/A";
            //lblSpeed.Text = "Speed: N/A";
            //lblStatus.Text = "Status: N/A";
            //lblPing.Text = "Ping: N/A";


            //// Consistent font for all detail labels (optional)
            //Font detailLabelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            //lblInternalIP.Font = detailLabelFont;
            //lblExternalIP.Font = detailLabelFont;
            //lblMAC.Font = detailLabelFont;
            //lblSpeed.Font = detailLabelFont;
            //lblStatus.Font = detailLabelFont;
            //lblPing.Font = detailLabelFont;

            // Tab order for key controls
            //cmbInterfaces.TabIndex = 0;
            //btnRefresh.TabIndex = 1;
            //groupBoxInterfaceDetails.TabIndex = 2;
            //groupBoxNetworkTraffic.TabIndex = 3;
            //groupBoxSystemUsage.TabIndex = 4;
            //groupBoxConnections.TabIndex = 5;
            //statusStrip.TabIndex = 6;

            // Timer enabled explicitly
            //timer1.Enabled = true;


            components = new System.ComponentModel.Container();

            cmbInterfaces = new System.Windows.Forms.ComboBox();
            lblInternalIP = new System.Windows.Forms.Label();
            lblExternalIP = new System.Windows.Forms.Label();
            lblMAC = new System.Windows.Forms.Label();
            lblSpeed = new System.Windows.Forms.Label();
            lblStatus = new System.Windows.Forms.Label();
            lblPing = new System.Windows.Forms.Label();
            lblRealTimeClock = new System.Windows.Forms.Label();

            dgvConnections = new System.Windows.Forms.DataGridView();
            statusStrip = new System.Windows.Forms.StatusStrip();

            toolStripStatusLabelConnections = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelDownload = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelUpload = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelCPU = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelRAM = new System.Windows.Forms.ToolStripStatusLabel();

            btnRefresh = new System.Windows.Forms.Button();
            timer1 = new System.Windows.Forms.Timer(components);

            chartNetworkTraffic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartSystemUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();

            groupBoxInterfaceDetails = new System.Windows.Forms.GroupBox();
            groupBoxNetworkTraffic = new System.Windows.Forms.GroupBox();
            groupBoxSystemUsage = new System.Windows.Forms.GroupBox();
            groupBoxConnections = new System.Windows.Forms.GroupBox();

            colProtocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colLocalAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRemoteAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(dgvConnections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(chartNetworkTraffic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(chartSystemUsage)).BeginInit();

            SuspendLayout();

            // cmbInterfaces
            cmbInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbInterfaces.Location = new System.Drawing.Point(12, 35);
            cmbInterfaces.Name = "cmbInterfaces";
            cmbInterfaces.Size = new System.Drawing.Size(350, 23);
            cmbInterfaces.TabIndex = 0;
            cmbInterfaces.SelectedIndexChanged += new System.EventHandler(cmbInterfaces_SelectedIndexChanged);

            // lblRealTimeClock
            lblRealTimeClock.AutoSize = true;
            lblRealTimeClock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblRealTimeClock.Location = new System.Drawing.Point(700, 10);
            lblRealTimeClock.Name = "lblRealTimeClock";
            lblRealTimeClock.Size = new System.Drawing.Size(85, 19);
            lblRealTimeClock.TabIndex = 20;
            lblRealTimeClock.Text = "00:00:00 AM";

            // groupBoxInterfaceDetails
            groupBoxInterfaceDetails.Controls.Add(lblInternalIP);
            groupBoxInterfaceDetails.Controls.Add(lblExternalIP);
            groupBoxInterfaceDetails.Controls.Add(lblMAC);
            groupBoxInterfaceDetails.Controls.Add(lblSpeed);
            groupBoxInterfaceDetails.Controls.Add(lblStatus);
            groupBoxInterfaceDetails.Controls.Add(lblPing);
            groupBoxInterfaceDetails.Location = new System.Drawing.Point(12, 65);
            groupBoxInterfaceDetails.Name = "groupBoxInterfaceDetails";
            groupBoxInterfaceDetails.Size = new System.Drawing.Size(850, 100);
            groupBoxInterfaceDetails.TabIndex = 1;
            groupBoxInterfaceDetails.TabStop = false;
            groupBoxInterfaceDetails.Text = "Selected Interface Details";

            // Details labels inside groupBoxInterfaceDetails
            int labelLeft = 10, labelTopStart = 20, labelSpacing = 20;
            lblInternalIP.AutoSize = true;
            lblInternalIP.Location = new System.Drawing.Point(labelLeft, labelTopStart);
            lblInternalIP.Name = "lblInternalIP";
            lblInternalIP.Size = new System.Drawing.Size(90, 15);
            lblInternalIP.TabIndex = 2;
            lblInternalIP.Text = "Internal IP: ...";

            lblExternalIP.AutoSize = true;
            lblExternalIP.Location = new System.Drawing.Point(labelLeft, labelTopStart + labelSpacing);
            lblExternalIP.Name = "lblExternalIP";
            lblExternalIP.Size = new System.Drawing.Size(92, 15);
            lblExternalIP.TabIndex = 3;
            lblExternalIP.Text = "External IP: ...";

            lblMAC.AutoSize = true;
            lblMAC.Location = new System.Drawing.Point(labelLeft + 300, labelTopStart);
            lblMAC.Name = "lblMAC";
            lblMAC.Size = new System.Drawing.Size(80, 15);
            lblMAC.TabIndex = 4;
            lblMAC.Text = "MAC: ...";

            lblSpeed.AutoSize = true;
            lblSpeed.Location = new System.Drawing.Point(labelLeft + 300, labelTopStart + labelSpacing);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new System.Drawing.Size(90, 15);
            lblSpeed.TabIndex = 5;
            lblSpeed.Text = "Speed: ...";

            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(labelLeft + 600, labelTopStart);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(70, 15);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status: ...";

            lblPing.AutoSize = true;
            lblPing.Location = new System.Drawing.Point(labelLeft + 600, labelTopStart + labelSpacing);
            lblPing.Name = "lblPing";
            lblPing.Size = new System.Drawing.Size(70, 15);
            lblPing.TabIndex = 7;
            lblPing.Text = "Ping: ...";

            // groupBoxNetworkTraffic
            groupBoxNetworkTraffic.Controls.Add(chartNetworkTraffic);
            groupBoxNetworkTraffic.Location = new System.Drawing.Point(12, 170);
            groupBoxNetworkTraffic.Name = "groupBoxNetworkTraffic";
            groupBoxNetworkTraffic.Size = new System.Drawing.Size(850, 250);
            groupBoxNetworkTraffic.TabIndex = 2;
            groupBoxNetworkTraffic.TabStop = false;
            groupBoxNetworkTraffic.Text = "Network Traffic (Upload / Download)";

            // chartNetworkTraffic
            chartNetworkTraffic.Dock = System.Windows.Forms.DockStyle.Fill;
            chartNetworkTraffic.Location = new System.Drawing.Point(3, 19);
            chartNetworkTraffic.Name = "chartNetworkTraffic";
            chartNetworkTraffic.Size = new System.Drawing.Size(844, 228);
            chartNetworkTraffic.TabIndex = 0;

            // groupBoxSystemUsage
            groupBoxSystemUsage.Controls.Add(chartSystemUsage);
            groupBoxSystemUsage.Location = new System.Drawing.Point(12, 425);
            groupBoxSystemUsage.Name = "groupBoxSystemUsage";
            groupBoxSystemUsage.Size = new System.Drawing.Size(850, 150);
            groupBoxSystemUsage.TabIndex = 3;
            groupBoxSystemUsage.TabStop = false;
            groupBoxSystemUsage.Text = "System Usage (CPU / RAM)";

            // chartSystemUsage
            chartSystemUsage.Dock = System.Windows.Forms.DockStyle.Fill;
            chartSystemUsage.Location = new System.Drawing.Point(3, 19);
            chartSystemUsage.Name = "chartSystemUsage";
            chartSystemUsage.Size = new System.Drawing.Size(844, 128);
            chartSystemUsage.TabIndex = 0;

            // groupBoxConnections
            groupBoxConnections.Controls.Add(dgvConnections);
            groupBoxConnections.Location = new System.Drawing.Point(12, 580);
            groupBoxConnections.Name = "groupBoxConnections";
            groupBoxConnections.Size = new System.Drawing.Size(850, 250);
            groupBoxConnections.TabIndex = 4;
            groupBoxConnections.TabStop = false;
            groupBoxConnections.Text = "Active Network Connections";

            // dgvConnections
            dgvConnections.AllowUserToAddRows = false;
            dgvConnections.AllowUserToDeleteRows = false;
            dgvConnections.AllowUserToOrderColumns = true;
            dgvConnections.AllowUserToResizeRows = false;
            dgvConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvConnections.MultiSelect = false;
            dgvConnections.ReadOnly = true;
            dgvConnections.RowHeadersVisible = false;
            dgvConnections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvConnections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colProtocol,
                colLocalAddress,
                colRemoteAddress,
                colState,
                colProcessName
            });

            // Columns
            colProtocol.HeaderText = "Protocol";
            colProtocol.Name = "colProtocol";
            colProtocol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            colLocalAddress.HeaderText = "Local Address";
            colLocalAddress.Name = "colLocalAddress";
            colLocalAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            colRemoteAddress.HeaderText = "Remote Address";
            colRemoteAddress.Name = "colRemoteAddress";
            colRemoteAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            colState.HeaderText = "State";
            colState.Name = "colState";
            colState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            colProcessName.HeaderText = "Process Name";
            colProcessName.Name = "colProcessName";
            colProcessName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            // statusStrip
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                toolStripStatusLabelConnections,
                toolStripStatusLabelDownload,
                toolStripStatusLabelUpload,
                toolStripStatusLabelCPU,
                toolStripStatusLabelRAM
            });
            statusStrip.Location = new System.Drawing.Point(0, 840);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(874, 22);
            statusStrip.TabIndex = 10;

            // StatusStrip labels default texts
            toolStripStatusLabelConnections.Text = "Connections: 0";
            toolStripStatusLabelDownload.Text = "Download: 0 B/s";
            toolStripStatusLabelUpload.Text = "Upload: 0 B/s";
            toolStripStatusLabelCPU.Text = "CPU: 0 %";
            toolStripStatusLabelRAM.Text = "RAM: 0 %";

            // btnRefresh
            btnRefresh.Location = new System.Drawing.Point(370, 35);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(75, 25);
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += new System.EventHandler(btnRefresh_Click);

            // timer1
            timer1.Interval = 1000;
            timer1.Tick += new System.EventHandler(timer1_Tick);

            // NetworkMonitorForm
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(874, 862);
            Controls.Add(lblRealTimeClock);
            Controls.Add(btnRefresh);
            Controls.Add(statusStrip);
            Controls.Add(groupBoxConnections);
            Controls.Add(groupBoxSystemUsage);
            Controls.Add(groupBoxNetworkTraffic);
            Controls.Add(groupBoxInterfaceDetails);
            Controls.Add(cmbInterfaces);
            Controls.Add(btnStopAction);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "NetworkMonitorForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Advanced Network Monitor";


            // Initialize btnStopAction before adding it to Controls
            btnStopAction = new System.Windows.Forms.Button();
            btnStopAction.Location = new System.Drawing.Point(450, 35);
            btnStopAction.Name = "btnStopAction";
            btnStopAction.Size = new System.Drawing.Size(75, 25);
            btnStopAction.TabIndex = 12;
            btnStopAction.Text = "Stop";
            btnStopAction.UseVisualStyleBackColor = true;
            // btnStopAction.Click += new System.EventHandler(btnStopAction_Click);

            // Add btnStopAction to the form controls once, after initialization
            Controls.Add(btnStopAction);



            ((System.ComponentModel.ISupportInitialize)(dgvConnections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(chartNetworkTraffic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(chartSystemUsage)).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
