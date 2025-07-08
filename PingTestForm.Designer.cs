namespace TDM_IP_Tracker
{
    partial class PingTestForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button btnStartPing;
        private System.Windows.Forms.ListView lvPingResults;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colResponseTime;
        private System.Windows.Forms.ColumnHeader colTTL;
        private System.Windows.Forms.TextBox txtTraceRoute;
        private System.Windows.Forms.Button btnTraceRoute;
        private System.Windows.Forms.TextBox txtDnsInfo;
        private System.Windows.Forms.Button btnDnsLookup;
        private System.Windows.Forms.TextBox txtOpenPorts;
        private System.Windows.Forms.Button btnPortScan;
        private System.Windows.Forms.TextBox txtGeoLocation;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnStopAction;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        private void OpenPingSettings()
        {
            var settingsForm = new PingSettingsForm(
                timeout: 4000,
                pingCount: 4,
                packetSize: 32,
                ttl: 128,
                dontFragment: false,
                interval: 1000,
                resolveHostnames: true);

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Apply the updated settings from the form
                int timeout = settingsForm.Timeout;
                int pingCount = settingsForm.PingCount;
                int packetSize = settingsForm.PacketSize;
                int ttl = settingsForm.TTL;
                bool dontFragment = settingsForm.DontFragment;
                int interval = settingsForm.Interval;
                bool resolveHostnames = settingsForm.ResolveHostnames;

                // TODO: Use these values in your ping logic
                MessageBox.Show($"Settings Updated:\nTimeout={timeout} ms\nCount={pingCount}\nPacket Size={packetSize} bytes\nTTL={ttl}\nDon't Fragment={dontFragment}\nInterval={interval} ms\nResolve Hostnames={resolveHostnames}");
            }
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtTarget = new TextBox();
            btnStartPing = new Button();
            lvPingResults = new ListView();
            colTime = new ColumnHeader();
            colStatus = new ColumnHeader();
            colResponseTime = new ColumnHeader();
            colTTL = new ColumnHeader();
            txtTraceRoute = new TextBox();
            btnTraceRoute = new Button();
            txtDnsInfo = new TextBox();
            btnDnsLookup = new Button();
            txtOpenPorts = new TextBox();
            btnPortScan = new Button();
            txtGeoLocation = new TextBox();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            progressBar = new ProgressBar();
            btnStopAction = new Button();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // txtTarget
            // 
            txtTarget.Location = new Point(12, 12);
            txtTarget.Name = "txtTarget";
            txtTarget.PlaceholderText = "Enter IP or Hostname to test";
            txtTarget.Size = new Size(250, 23);
            txtTarget.TabIndex = 0;
            // 
            // btnStartPing
            // 
            btnStartPing.Location = new Point(270, 12);
            btnStartPing.Name = "btnStartPing";
            btnStartPing.Size = new Size(100, 25);
            btnStartPing.TabIndex = 1;
            btnStartPing.Text = "Start Ping";
            btnStartPing.Click += BtnStartPing_Click;
            // 
            // lvPingResults
            // 
            lvPingResults.Columns.AddRange(new ColumnHeader[] { colTime, colStatus, colResponseTime, colTTL });
            lvPingResults.FullRowSelect = true;
            lvPingResults.GridLines = true;
            lvPingResults.Location = new Point(12, 45);
            lvPingResults.Name = "lvPingResults";
            lvPingResults.Size = new Size(470, 150);
            lvPingResults.TabIndex = 2;
            lvPingResults.UseCompatibleStateImageBehavior = false;
            lvPingResults.View = View.Details;
            // 
            // colTime
            // 
            colTime.Text = "Time";
            colTime.Width = 130;
            // 
            // colStatus
            // 
            colStatus.Text = "Status";
            colStatus.Width = 100;
            // 
            // colResponseTime
            // 
            colResponseTime.Text = "Response Time (ms)";
            colResponseTime.Width = 130;
            // 
            // colTTL
            // 
            colTTL.Text = "TTL";
            colTTL.Width = 70;
            // 
            // txtTraceRoute
            // 
            txtTraceRoute.Location = new Point(490, 45);
            txtTraceRoute.Multiline = true;
            txtTraceRoute.Name = "txtTraceRoute";
            txtTraceRoute.ReadOnly = true;
            txtTraceRoute.ScrollBars = ScrollBars.Vertical;
            txtTraceRoute.Size = new Size(300, 150);
            txtTraceRoute.TabIndex = 3;
            // 
            // btnTraceRoute
            // 
            btnTraceRoute.Location = new Point(490, 12);
            btnTraceRoute.Name = "btnTraceRoute";
            btnTraceRoute.Size = new Size(100, 25);
            btnTraceRoute.TabIndex = 4;
            btnTraceRoute.Text = "Traceroute";
            btnTraceRoute.Click += BtnTraceRoute_Click;
            // 
            // txtDnsInfo
            // 
            txtDnsInfo.Location = new Point(12, 235);
            txtDnsInfo.Multiline = true;
            txtDnsInfo.Name = "txtDnsInfo";
            txtDnsInfo.ReadOnly = true;
            txtDnsInfo.ScrollBars = ScrollBars.Vertical;
            txtDnsInfo.Size = new Size(470, 100);
            txtDnsInfo.TabIndex = 5;
            // 
            // btnDnsLookup
            // 
            btnDnsLookup.Location = new Point(12, 205);
            btnDnsLookup.Name = "btnDnsLookup";
            btnDnsLookup.Size = new Size(100, 25);
            btnDnsLookup.TabIndex = 6;
            btnDnsLookup.Text = "DNS Lookup";
            btnDnsLookup.Click += BtnDnsLookup_Click;
            // 
            // txtOpenPorts
            // 
            txtOpenPorts.Location = new Point(490, 235);
            txtOpenPorts.Multiline = true;
            txtOpenPorts.Name = "txtOpenPorts";
            txtOpenPorts.ReadOnly = true;
            txtOpenPorts.ScrollBars = ScrollBars.Vertical;
            txtOpenPorts.Size = new Size(300, 100);
            txtOpenPorts.TabIndex = 7;
            // 
            // btnPortScan
            // 
            btnPortScan.Location = new Point(490, 205);
            btnPortScan.Name = "btnPortScan";
            btnPortScan.Size = new Size(100, 25);
            btnPortScan.TabIndex = 8;
            btnPortScan.Text = "Port Scan";
            btnPortScan.Click += BtnPortScan_Click;
            // 
            // txtGeoLocation
            // 
            txtGeoLocation.Location = new Point(12, 345);
            txtGeoLocation.Multiline = true;
            txtGeoLocation.Name = "txtGeoLocation";
            txtGeoLocation.ReadOnly = true;
            txtGeoLocation.ScrollBars = ScrollBars.Vertical;
            txtGeoLocation.Size = new Size(470, 80);
            txtGeoLocation.TabIndex = 9;
            txtGeoLocation.Click += txtGeoLocation_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 435);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(804, 22);
            statusStrip.TabIndex = 10;
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(39, 17);
            toolStripStatusLabel.Text = "Ready";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 430);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(778, 20);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 11;
            // 
            // btnStopAction
            // 
            btnStopAction.Location = new Point(380, 12);
            btnStopAction.Name = "btnStopAction";
            btnStopAction.Size = new Size(100, 25);
            btnStopAction.TabIndex = 0;
            btnStopAction.Text = "Stop";
            // 
            // PingTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 457);
            Controls.Add(btnStopAction);
            Controls.Add(txtTarget);
            Controls.Add(btnStartPing);
            Controls.Add(lvPingResults);
            Controls.Add(txtTraceRoute);
            Controls.Add(btnTraceRoute);
            Controls.Add(txtDnsInfo);
            Controls.Add(btnDnsLookup);
            Controls.Add(txtOpenPorts);
            Controls.Add(btnPortScan);
            Controls.Add(txtGeoLocation);
            Controls.Add(statusStrip);
            Controls.Add(progressBar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "PingTestForm";
            Text = "Advanced Ping Tester";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
