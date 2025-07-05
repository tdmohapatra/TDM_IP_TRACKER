using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TDM_IP_Tracker
{
    public partial class PortScannerForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblHost;
        private TextBox txtHost;
        private Label lblPortRange;
        private TextBox txtStartPort;
        private Label lblTo;
        private TextBox txtEndPort;
        private Button btnScan;
        private Button btnStop;
        private ListView lvResults;
        private ColumnHeader colPort;
        private ColumnHeader colStatus;
        private ColumnHeader colService;
        private ProgressBar progressBar;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private Timer scanTimer;
        private CheckBox chkCommonPorts;
        private NumericUpDown numThreads;
        private Label lblThreads;
        private Button btnSaveResults;
        private SaveFileDialog saveFileDialog;

        // Newly added:
        private NumericUpDown numTimeout;
        private Label lblTimeout;
        private CheckBox chkShowClosed;

        //public PortScannerForm()
        //{
        //    InitializeComponent();
        //}

        //private void btnScan_Click(object sender, EventArgs e)
        //{
        //    lblStatus.Text = "Scanning...";
        //    btnScan.Enabled = false;
        //    btnStop.Enabled = true;

        //    // Example: just start the timer
        //    scanTimer.Start();
        //}

        private void btnStop_Click(object sender, EventArgs e)
        {
            scanTimer.Stop();
            lblStatus.Text = "Scan stopped";
            btnScan.Enabled = true;
            btnStop.Enabled = false;
        }

        //private void btnSaveResults_Click(object sender, EventArgs e)
        //{
        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        // Example: save logic
        //        MessageBox.Show("Results saved to " + saveFileDialog.FileName);
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblHost = new Label();
            txtHost = new TextBox();
            lblPortRange = new Label();
            txtStartPort = new TextBox();
            lblTo = new Label();
            txtEndPort = new TextBox();
            btnScan = new Button();
            btnStop = new Button();
            lvResults = new ListView();
            colPort = new ColumnHeader();
            colStatus = new ColumnHeader();
            colService = new ColumnHeader();
            progressBar = new ProgressBar();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            scanTimer = new Timer(components);
            chkCommonPorts = new CheckBox();
            numThreads = new NumericUpDown();
            lblThreads = new Label();
            btnSaveResults = new Button();
            saveFileDialog = new SaveFileDialog();
            numTimeout = new NumericUpDown();
            lblTimeout = new Label();
            chkShowClosed = new CheckBox();

            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(numThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numTimeout)).BeginInit();
            SuspendLayout();

            // lblHost
            lblHost.AutoSize = true;
            lblHost.Location = new Point(20, 20);
            lblHost.Text = "Host:";

            // txtHost
            txtHost.Location = new Point(70, 17);
            txtHost.Size = new Size(150, 23);
            txtHost.Text = "localhost";

            // lblPortRange
            lblPortRange.AutoSize = true;
            lblPortRange.Location = new Point(240, 20);
            lblPortRange.Text = "Port Range:";

            // txtStartPort
            txtStartPort.Location = new Point(320, 17);
            txtStartPort.Size = new Size(60, 23);
            txtStartPort.Text = "1";

            // lblTo
            lblTo.AutoSize = true;
            lblTo.Location = new Point(390, 20);
            lblTo.Text = "to";

            // txtEndPort
            txtEndPort.Location = new Point(420, 17);
            txtEndPort.Size = new Size(60, 23);
            txtEndPort.Text = "1024";

            // btnScan
            btnScan.Location = new Point(500, 16);
            btnScan.Size = new Size(100, 25);
            btnScan.Text = "Start Scan";
            btnScan.Click += btnScan_Click;

            // btnStop
            btnStop.Location = new Point(610, 16);
            btnStop.Size = new Size(100, 25);
            btnStop.Text = "Stop";
            btnStop.Enabled = false;
            btnStop.Click += btnStop_Click;

            // lvResults
            lvResults.Columns.AddRange(new[] { colPort, colStatus, colService });
            lvResults.FullRowSelect = true;
            lvResults.GridLines = true;
            lvResults.Location = new Point(20, 70);
            lvResults.Size = new Size(750, 300);
            lvResults.View = View.Details;

            // colPort
            colPort.Text = "Port";
            colPort.Width = 80;

            // colStatus
            colStatus.Text = "Status";
            colStatus.Width = 100;

            // colService
            colService.Text = "Service";
            colService.Width = 200;

            // progressBar
            progressBar.Location = new Point(20, 380);
            progressBar.Size = new Size(750, 20);

            // statusStrip
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 428);
            statusStrip.Size = new Size(800, 22);

            // lblStatus
            lblStatus.Text = "Ready to scan";

            // chkCommonPorts
            chkCommonPorts.AutoSize = true;
            chkCommonPorts.Location = new Point(20, 45);
            chkCommonPorts.Text = "Scan common ports only";
            chkCommonPorts.Checked = true;

            // lblThreads
            lblThreads.AutoSize = true;
            lblThreads.Location = new Point(180, 47);
            lblThreads.Text = "Threads:";

            // numThreads
            numThreads.Location = new Point(250, 45);
            numThreads.Minimum = 1;
            numThreads.Value = 10;
            numThreads.Size = new Size(60, 23);

            // lblTimeout
            lblTimeout.AutoSize = true;
            lblTimeout.Location = new Point(330, 47);
            lblTimeout.Text = "Timeout:";

            // numTimeout
            numTimeout.Location = new Point(390, 45);
            numTimeout.Minimum = 100;
            numTimeout.Maximum = 10000;
            numTimeout.Increment = 100;
            numTimeout.Value = 1000;
            numTimeout.Size = new Size(60, 23);

            // chkShowClosed
            chkShowClosed.AutoSize = true;
            chkShowClosed.Location = new Point(470, 47);
            chkShowClosed.Text = "Show Closed Ports";
            chkShowClosed.Checked = true;

            // btnSaveResults
            btnSaveResults.Location = new Point(670, 45);
            btnSaveResults.Size = new Size(100, 25);
            btnSaveResults.Text = "Save Results";
            btnSaveResults.Click += btnSaveResults_Click;

            // PortScannerForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.AddRange(new Control[]
            {
                lblHost, txtHost,
                lblPortRange, txtStartPort, lblTo, txtEndPort,
                btnScan, btnStop,
                chkCommonPorts, lblThreads, numThreads,
                lblTimeout, numTimeout,
                chkShowClosed,
                btnSaveResults, lvResults, progressBar,
                statusStrip
            });
            Name = "PortScannerForm";
            Text = "Port Scanner";

            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(numThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numTimeout)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
