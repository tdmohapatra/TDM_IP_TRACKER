namespace TDM_IP_Tracker
{
    partial class MassIPForm
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
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numColumns).BeginInit();
            panelTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panelIPContainer
            // 
            panelIPContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelIPContainer.AutoScroll = true;
            panelIPContainer.BackColor = Color.FromArgb(240, 240, 240);
            panelIPContainer.Location = new Point(10, 90);
            panelIPContainer.Margin = new Padding(0);
            panelIPContainer.Name = "panelIPContainer";
            panelIPContainer.Padding = new Padding(5);
            panelIPContainer.Size = new Size(1004, 460);
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
            btnLoadExcel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoadExcel.ImageAlign = ContentAlignment.MiddleLeft;
            btnLoadExcel.Location = new Point(10, 50);
            btnLoadExcel.Name = "btnLoadExcel";
            btnLoadExcel.Size = new Size(110, 30);
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
            btnTrack.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTrack.ImageAlign = ContentAlignment.MiddleLeft;
            btnTrack.Location = new Point(125, 50);
            btnTrack.Name = "btnTrack";
            btnTrack.Size = new Size(90, 30);
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
            btnSelectAll.Location = new Point(231, 44);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(100, 21);
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
            btnDeselectAll.Location = new Point(221, 65);
            btnDeselectAll.Name = "btnDeselectAll";
            btnDeselectAll.Size = new Size(110, 20);
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
            chkAutoCheck.Location = new Point(824, 51);
            chkAutoCheck.Name = "chkAutoCheck";
            chkAutoCheck.Size = new Size(110, 30);
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
            btnAddIP.Location = new Point(337, 50);
            btnAddIP.Name = "btnAddIP";
            btnAddIP.Size = new Size(90, 30);
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
            btnRemoveIP.Location = new Point(433, 50);
            btnRemoveIP.Name = "btnRemoveIP";
            btnRemoveIP.Size = new Size(100, 30);
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
            btnClear.Location = new Point(530, 50);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(90, 30);
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
            btnExport.Location = new Point(615, 52);
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
            btnSettings.Location = new Point(742, 52);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(31, 26);
            btnSettings.TabIndex = 10;
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
            btnTheme.Location = new Point(789, 52);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(29, 28);
            btnTheme.TabIndex = 11;
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
            statusProgress.Name = "statusProgress";
            statusProgress.Size = new Size(100, 16);
            // 
            // numColumns
            // 
            numColumns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numColumns.BackColor = Color.White;
            numColumns.BorderStyle = BorderStyle.None;
            numColumns.Location = new Point(940, 59);
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
            label1.Location = new Point(940, 74);
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
            // MassIPForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(1024, 581);
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
            Controls.Add(panelIPContainer);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel panelIPContainer;
        private Button btnLoadExcel;
        private Button btnTrack;
        private Button btnSelectAll;
        private Button btnDeselectAll;
        private CheckBox chkAutoCheck;
        private Button btnAddIP;
        private Button btnRemoveIP;
        private Button btnClear;
        private Button btnExport;
        private Button btnSettings;
        private Button btnTheme;
        private System.Windows.Forms.Timer autoTimer;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar statusProgress;
        private ToolTip toolTip;
        private NumericUpDown numColumns;
        private Label label1;
        private Panel panelToolbar;
        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnClose;
        private Label titleLabel;
        private Panel panelTitle;
        private System.Windows.Forms.Timer fadeTimer;
    }
}