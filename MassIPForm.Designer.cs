using System.Windows.Forms.DataVisualization.Charting;

namespace TDM_IP_Tracker
{
    partial class MassIPForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
     
        private Chart sectionChart;

        private DataGridView sectionGridView;
  
        // Add this to InitializeApplication()
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
        public void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelIPContainer = new FlowLayoutPanel();
            btnLoadExcel = new Button();
            btnTrack = new Button();
            btnSelectAll = new Button();
            btnDeselectAll = new Button();
            chkAutoCheck = new CheckBox();
            btnAddIP = new Button();
            btnRemoveIP = new Button();
            btnClear = new Button();
            btnExport = new Button();
            btnSettings = new Button();
            btnTheme = new Button();
            autoTimer = new System.Windows.Forms.Timer(components);
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusProgress = new ToolStripProgressBar();
            toolTip = new ToolTip(components);
            numColumns = new NumericUpDown();
            label1 = new Label();
            titleLabel = new Label();
            panelTitle = new Panel();
            fadeTimer = new System.Windows.Forms.Timer(components);
            splitContainerMain = new SplitContainer();
            panelDashboard = new Panel();
            panelStats = new Panel();
            lblFailedIPs = new Label();
            lblActiveIPs = new Label();
            lblTotalIPs = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            tabControl = new TabControl();
            tabPageDashboard = new TabPage();
            tabPageDetails = new TabPage();
            dataGridViewDetails = new DataGridView();
            btnExportPDF = new Button();
            btnRefreshStats = new Button();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numColumns).BeginInit();
            panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            panelDashboard.SuspendLayout();
            panelStats.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageDashboard.SuspendLayout();
            tabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetails).BeginInit();
            SuspendLayout();
            // 
            // panelIPContainer
            // 
            panelIPContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelIPContainer.AutoScroll = true;
            panelIPContainer.BackColor = Color.FromArgb(240, 240, 240);
            panelIPContainer.Location = new Point(7, 3);
            panelIPContainer.Margin = new Padding(0);
            panelIPContainer.Name = "panelIPContainer";
            panelIPContainer.Padding = new Padding(5);
            panelIPContainer.Size = new Size(752, 422);
            panelIPContainer.TabIndex = 0;
            // 
            // btnLoadExcel
            // 
            btnLoadExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadExcel.BackColor = Color.Transparent;
            btnLoadExcel.FlatAppearance.BorderSize = 0;
            btnLoadExcel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnLoadExcel.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnLoadExcel.FlatStyle = FlatStyle.Flat;
            btnLoadExcel.Font = new Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoadExcel.ForeColor = SystemColors.ActiveCaptionText;
            btnLoadExcel.ImageAlign = ContentAlignment.MiddleLeft;
            btnLoadExcel.Location = new Point(0, 33);
            btnLoadExcel.Name = "btnLoadExcel";
            btnLoadExcel.Size = new Size(144, 65);
            btnLoadExcel.TabIndex = 1;
            btnLoadExcel.Text = "  Load Excel";
            btnLoadExcel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLoadExcel.UseVisualStyleBackColor = false;
            btnLoadExcel.Click += btnLoadExcel_Click;
            // 
            // btnTrack
            // 
            btnTrack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTrack.BackColor = Color.Transparent;
            btnTrack.FlatAppearance.BorderSize = 0;
            btnTrack.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnTrack.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnTrack.FlatStyle = FlatStyle.Flat;
            btnTrack.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTrack.ForeColor = Color.MidnightBlue;
            btnTrack.ImageAlign = ContentAlignment.MiddleLeft;
            btnTrack.Location = new Point(150, 34);
            btnTrack.Name = "btnTrack";
            btnTrack.Size = new Size(90, 65);
            btnTrack.TabIndex = 2;
            btnTrack.Text = "  Track";
            btnTrack.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTrack.UseVisualStyleBackColor = false;
            btnTrack.Click += btnTrack_Click;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectAll.BackColor = Color.Transparent;
            btnSelectAll.FlatAppearance.BorderSize = 0;
            btnSelectAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnSelectAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnSelectAll.FlatStyle = FlatStyle.Flat;
            btnSelectAll.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectAll.ImageAlign = ContentAlignment.MiddleLeft;
            btnSelectAll.Location = new Point(246, 35);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(100, 27);
            btnSelectAll.TabIndex = 3;
            btnSelectAll.Text = "  Select All";
            btnSelectAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectAll.UseVisualStyleBackColor = false;
            btnSelectAll.Click += btnSelectAll_Click;
            // 
            // btnDeselectAll
            // 
            btnDeselectAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeselectAll.BackColor = Color.Transparent;
            btnDeselectAll.FlatAppearance.BorderSize = 0;
            btnDeselectAll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnDeselectAll.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnDeselectAll.FlatStyle = FlatStyle.Flat;
            btnDeselectAll.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeselectAll.ImageAlign = ContentAlignment.MiddleLeft;
            btnDeselectAll.Location = new Point(246, 68);
            btnDeselectAll.Name = "btnDeselectAll";
            btnDeselectAll.Size = new Size(110, 29);
            btnDeselectAll.TabIndex = 4;
            btnDeselectAll.Text = "  Deselect All";
            btnDeselectAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeselectAll.UseVisualStyleBackColor = false;
            btnDeselectAll.Click += btnDeselectAll_Click;
            // 
            // chkAutoCheck
            // 
            chkAutoCheck.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkAutoCheck.Appearance = Appearance.Button;
            chkAutoCheck.BackColor = Color.Transparent;
            chkAutoCheck.FlatAppearance.BorderSize = 0;
            chkAutoCheck.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 120, 215);
            chkAutoCheck.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 230, 250);
            chkAutoCheck.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            chkAutoCheck.FlatStyle = FlatStyle.Flat;
            chkAutoCheck.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAutoCheck.ImageAlign = ContentAlignment.MiddleLeft;
            chkAutoCheck.Location = new Point(790, 39);
            chkAutoCheck.Name = "chkAutoCheck";
            chkAutoCheck.Size = new Size(85, 58);
            chkAutoCheck.TabIndex = 5;
            chkAutoCheck.Text = "  Auto Check";
            chkAutoCheck.TextImageRelation = TextImageRelation.ImageBeforeText;
            chkAutoCheck.UseVisualStyleBackColor = false;
            chkAutoCheck.CheckedChanged += chkAutoCheck_CheckedChanged;
            // 
            // btnAddIP
            // 
            btnAddIP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddIP.BackColor = Color.Transparent;
            btnAddIP.FlatAppearance.BorderSize = 0;
            btnAddIP.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnAddIP.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnAddIP.FlatStyle = FlatStyle.Flat;
            btnAddIP.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddIP.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddIP.Location = new Point(362, 29);
            btnAddIP.Name = "btnAddIP";
            btnAddIP.Size = new Size(90, 65);
            btnAddIP.TabIndex = 6;
            btnAddIP.Text = "  Add IP";
            btnAddIP.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddIP.UseVisualStyleBackColor = false;
            btnAddIP.Click += btnAddIP_Click;
            // 
            // btnRemoveIP
            // 
            btnRemoveIP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRemoveIP.BackColor = Color.Transparent;
            btnRemoveIP.FlatAppearance.BorderSize = 0;
            btnRemoveIP.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnRemoveIP.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnRemoveIP.FlatStyle = FlatStyle.Flat;
            btnRemoveIP.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRemoveIP.ImageAlign = ContentAlignment.MiddleLeft;
            btnRemoveIP.Location = new Point(458, 33);
            btnRemoveIP.Name = "btnRemoveIP";
            btnRemoveIP.Size = new Size(75, 62);
            btnRemoveIP.TabIndex = 7;
            btnRemoveIP.Text = "  Remove IP";
            btnRemoveIP.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveIP.UseVisualStyleBackColor = false;
            btnRemoveIP.Click += btnRemoveIP_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.Transparent;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(539, 35);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(77, 62);
            btnClear.TabIndex = 8;
            btnClear.Text = "  Clear All";
            btnClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = Color.Transparent;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExport.ImageAlign = ContentAlignment.MiddleLeft;
            btnExport.Location = new Point(707, 35);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(90, 30);
            btnExport.TabIndex = 9;
            btnExport.Text = "  Export";
            btnExport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSettings.BackColor = Color.MediumOrchid;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(995, 67);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(31, 26);
            btnSettings.TabIndex = 10;
            btnSettings.Text = "S";
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnTheme
            // 
            btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTheme.BackColor = Color.FromArgb(128, 255, 128);
            btnTheme.FlatAppearance.BorderSize = 0;
            btnTheme.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnTheme.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTheme.ImageAlign = ContentAlignment.MiddleLeft;
            btnTheme.Location = new Point(995, 33);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(29, 28);
            btnTheme.TabIndex = 11;
            btnTheme.Text = "T";
            btnTheme.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTheme.UseVisualStyleBackColor = false;
            btnTheme.Click += btnTheme_Click;
            // 
            // autoTimer
            // 
            autoTimer.Interval = 10000;
            autoTimer.Tick += autoTimer_Tick;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(240, 240, 240);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, statusProgress });
            statusStrip.Location = new Point(0, 559);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1024, 22);
            statusStrip.TabIndex = 12;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";
            // 
            // statusProgress
            // 
            statusProgress.ForeColor = Color.DarkViolet;
            statusProgress.Name = "statusProgress";
            statusProgress.Size = new Size(100, 16);
            statusProgress.Click += statusProgress_Click;
            // 
            // numColumns
            // 
            numColumns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numColumns.BackColor = Color.White;
            numColumns.BorderStyle = BorderStyle.None;
            numColumns.Location = new Point(909, 46);
            numColumns.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numColumns.Name = "numColumns";
            numColumns.Size = new Size(53, 19);
            numColumns.TabIndex = 13;
            numColumns.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numColumns.ValueChanged += numColumns_ValueChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(909, 75);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 14;
            label1.Text = "Columns";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(10, 5);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(152, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "TDM PING TRACKER";
            // 
            // panelTitle
            // 
            panelTitle.BackColor = Color.FromArgb(0, 120, 215);
            panelTitle.Controls.Add(titleLabel);
            panelTitle.Dock = DockStyle.Top;
            panelTitle.Location = new Point(0, 0);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new Size(1024, 30);
            panelTitle.TabIndex = 16;
            // 
            // fadeTimer
            // 
            fadeTimer.Interval = 10;
            fadeTimer.Tick += fadeTimer_Tick;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerMain.Location = new Point(0, 100);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(panelDashboard);
            splitContainerMain.Panel1MinSize = 250;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tabControl);
            splitContainerMain.Size = new Size(1024, 456);
            splitContainerMain.SplitterDistance = 250;
            splitContainerMain.TabIndex = 17;
            // 
            // panelDashboard
            // 
            panelDashboard.Controls.Add(panelStats);
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(0, 0);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(250, 456);
            panelDashboard.TabIndex = 0;
            panelDashboard.Paint += panelDashboard_Paint;
            // 
            // panelStats
            // 
            panelStats.Controls.Add(lblFailedIPs);
            panelStats.Controls.Add(lblActiveIPs);
            panelStats.Controls.Add(lblTotalIPs);
            panelStats.Controls.Add(label4);
            panelStats.Controls.Add(label3);
            panelStats.Controls.Add(label2);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 0);
            panelStats.Name = "panelStats";
            panelStats.Size = new Size(250, 140);
            panelStats.TabIndex = 0;
            // 
            // lblFailedIPs
            // 
            lblFailedIPs.AutoSize = true;
            lblFailedIPs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFailedIPs.Location = new Point(150, 80);
            lblFailedIPs.Name = "lblFailedIPs";
            lblFailedIPs.Size = new Size(19, 21);
            lblFailedIPs.TabIndex = 5;
            lblFailedIPs.Text = "0";
            // 
            // lblActiveIPs
            // 
            lblActiveIPs.AutoSize = true;
            lblActiveIPs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblActiveIPs.Location = new Point(150, 50);
            lblActiveIPs.Name = "lblActiveIPs";
            lblActiveIPs.Size = new Size(19, 21);
            lblActiveIPs.TabIndex = 4;
            lblActiveIPs.Text = "0";
            // 
            // lblTotalIPs
            // 
            lblTotalIPs.AutoSize = true;
            lblTotalIPs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalIPs.Location = new Point(150, 20);
            lblTotalIPs.Name = "lblTotalIPs";
            lblTotalIPs.Size = new Size(19, 21);
            lblTotalIPs.TabIndex = 3;
            lblTotalIPs.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(20, 80);
            label4.Name = "label4";
            label4.Size = new Size(65, 17);
            label4.TabIndex = 2;
            label4.Text = "Failed IPs:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(20, 50);
            label3.Name = "label3";
            label3.Size = new Size(65, 17);
            label3.TabIndex = 1;
            label3.Text = "Active IPs:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(20, 20);
            label2.Name = "label2";
            label2.Size = new Size(59, 17);
            label2.TabIndex = 0;
            label2.Text = "Total IPs:";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageDashboard);
            tabControl.Controls.Add(tabPageDetails);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(770, 456);
            tabControl.TabIndex = 0;
            // 
            // tabPageDashboard
            // 
            tabPageDashboard.Controls.Add(panelIPContainer);
            tabPageDashboard.Location = new Point(4, 24);
            tabPageDashboard.Name = "tabPageDashboard";
            tabPageDashboard.Padding = new Padding(3);
            tabPageDashboard.Size = new Size(762, 428);
            tabPageDashboard.TabIndex = 0;
            tabPageDashboard.Text = "IP Dashboard";
            tabPageDashboard.UseVisualStyleBackColor = true;
            // 
            // tabPageDetails
            // 
            tabPageDetails.Controls.Add(dataGridViewDetails);
            tabPageDetails.Location = new Point(4, 24);
            tabPageDetails.Name = "tabPageDetails";
            tabPageDetails.Padding = new Padding(3);
            tabPageDetails.Size = new Size(762, 428);
            tabPageDetails.TabIndex = 1;
            tabPageDetails.Text = "Detailed View";
            tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDetails
            // 
            dataGridViewDetails.AllowUserToAddRows = false;
            dataGridViewDetails.AllowUserToDeleteRows = false;
            dataGridViewDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDetails.Dock = DockStyle.Fill;
            dataGridViewDetails.Location = new Point(3, 3);
            dataGridViewDetails.Name = "dataGridViewDetails";
            dataGridViewDetails.ReadOnly = true;
            dataGridViewDetails.Size = new Size(756, 422);
            dataGridViewDetails.TabIndex = 0;
            // 
            // btnExportPDF
            // 
            btnExportPDF.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportPDF.BackColor = Color.Transparent;
            btnExportPDF.FlatAppearance.BorderSize = 0;
            btnExportPDF.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnExportPDF.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExportPDF.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportPDF.Location = new Point(707, 69);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(90, 28);
            btnExportPDF.TabIndex = 18;
            btnExportPDF.Text = "  Export PDF";
            btnExportPDF.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExportPDF.UseVisualStyleBackColor = false;
            btnExportPDF.Click += btnExportPDF_Click;
            // 
            // btnRefreshStats
            // 
            btnRefreshStats.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshStats.BackColor = Color.Transparent;
            btnRefreshStats.FlatAppearance.BorderSize = 0;
            btnRefreshStats.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 215);
            btnRefreshStats.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 230, 250);
            btnRefreshStats.FlatStyle = FlatStyle.Flat;
            btnRefreshStats.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefreshStats.ImageAlign = ContentAlignment.MiddleLeft;
            btnRefreshStats.Location = new Point(622, 36);
            btnRefreshStats.Name = "btnRefreshStats";
            btnRefreshStats.Size = new Size(79, 61);
            btnRefreshStats.TabIndex = 19;
            btnRefreshStats.Text = "  Refresh";
            btnRefreshStats.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshStats.UseVisualStyleBackColor = false;
            btnRefreshStats.Click += btnRefreshStats_Click;
            // 
            // MassIPForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(1024, 581);
            Controls.Add(btnRefreshStats);
            Controls.Add(btnExportPDF);
            Controls.Add(splitContainerMain);
            Controls.Add(panelTitle);
            Controls.Add(label1);
            Controls.Add(numColumns);
            Controls.Add(statusStrip);
            Controls.Add(btnTheme);
            Controls.Add(btnSettings);
            Controls.Add(btnExport);
            Controls.Add(btnClear);
            Controls.Add(btnRemoveIP);
            Controls.Add(btnAddIP);
            Controls.Add(chkAutoCheck);
            Controls.Add(btnDeselectAll);
            Controls.Add(btnSelectAll);
            Controls.Add(btnTrack);
            Controls.Add(btnLoadExcel);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MinimumSize = new Size(800, 600);
            Name = "MassIPForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Advanced Mass IP Tracker";
            Load += MassIPForm_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numColumns).EndInit();
            panelTitle.ResumeLayout(false);
            panelTitle.PerformLayout();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            panelDashboard.ResumeLayout(false);
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPageDashboard.ResumeLayout(false);
            tabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetails).EndInit();
            ResumeLayout(false);
            PerformLayout();

            // Create a new tab for section monitoring

        }

        // Then in your initialization code:

        #endregion

        public FlowLayoutPanel panelIPContainer;
        public Button btnLoadExcel;
        public Button btnTrack;
        public Button btnSelectAll;
        public Button btnDeselectAll;
        public CheckBox chkAutoCheck;
        public Button btnAddIP;
        public Button btnRemoveIP;
        public Button btnClear;
        public Button btnExport;
        public Button btnSettings;
        public Button btnTheme;
        public System.Windows.Forms.Timer autoTimer;
        public StatusStrip statusStrip;
        public ToolStripStatusLabel statusLabel;
        public ToolStripProgressBar statusProgress;
        public ToolTip toolTip;
        public NumericUpDown numColumns;
        public Label label1;
        public Panel panelToolbar;
        public Button btnMinimize;
        public Button btnMaximize;
        public Button btnClose;
        public Label titleLabel;
        public Panel panelTitle;
        public System.Windows.Forms.Timer fadeTimer;
        public SplitContainer splitContainerMain;
        public Panel panelDashboard;
        public Panel panelStats;
        public Label lblFailedIPs;
        public Label lblActiveIPs;
        public Label lblTotalIPs;
        public Label label4;
        public Label label3;
        public Label label2;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartStatus;
        public TabControl tabControl;
        public TabPage tabPageDashboard;
        public TabPage tabPageDetails;
        public Button btnExportPDF;
        public Button btnRefreshStats;
        public System.Windows.Forms.DataGridView dataGridViewDetails;
    }
}