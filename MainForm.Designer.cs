    namespace TDM_IP_Tracker
    {
        partial class MainForm
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Button btnSingleIP;
            private System.Windows.Forms.Button btnMassIP;
            private System.Windows.Forms.Timer fadeTimer;
            private System.Windows.Forms.Timer pulseTimer;
            private System.Windows.Forms.PictureBox logoPictureBox;


            private System.Windows.Forms.MenuStrip menuStrip1;
            private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
            private System.Windows.Forms.TabControl tabControl1;
            private System.Windows.Forms.TabPage tabDashboard;
            private System.Windows.Forms.TabPage tabTools;
            private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem pingSettingsToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
            private System.Windows.Forms.Button btnPortScanner;
            private System.Windows.Forms.Button btnNetworkMonitor;
            private System.Windows.Forms.Button btnPingTest;
            private System.Windows.Forms.StatusStrip statusStrip1;
            private System.Windows.Forms.ToolStripStatusLabel lblStatus;

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
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            btnSingleIP = new Button();
            btnMassIP = new Button();
            fadeTimer = new System.Windows.Forms.Timer(components);
            pulseTimer = new System.Windows.Forms.Timer(components);
            logoPictureBox = new PictureBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            pingSettingsToolStripMenuItem = new ToolStripMenuItem();
            themeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            documentationToolStripMenuItem = new ToolStripMenuItem();
            myMapToolStripMenuItem = new ToolStripMenuItem();
            gOOGLEMAPToolStripMenuItem = new ToolStripMenuItem();
            gOOGLEMAPToolStripMenuItem1 = new ToolStripMenuItem();
            sTREETMAPToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            tabDashboard = new TabPage();
            tabTools = new TabPage();
            btnPacketTracker = new Button();
            btnWiFiScanner = new Button();
            BtnPingVulnerabilityScan = new Button();
            btnPortScanner = new Button();
            btnNetworkMonitor = new Button();
            btnPingTest = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabDashboard.SuspendLayout();
            tabTools.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
            lblTitle.Location = new Point(-11, 33);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(800, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TDM IP TRACKER";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSingleIP
            // 
            btnSingleIP.BackColor = Color.FromArgb(0, 120, 215);
            btnSingleIP.FlatAppearance.BorderSize = 0;
            btnSingleIP.FlatStyle = FlatStyle.Flat;
            btnSingleIP.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSingleIP.ForeColor = Color.White;
            btnSingleIP.Location = new Point(296, 152);
            btnSingleIP.Name = "btnSingleIP";
            btnSingleIP.Size = new Size(200, 60);
            btnSingleIP.TabIndex = 1;
            btnSingleIP.Text = "Check Single IP";
            btnSingleIP.UseVisualStyleBackColor = false;
            // 
            // btnMassIP
            // 
            btnMassIP.BackColor = Color.FromArgb(0, 120, 215);
            btnMassIP.FlatAppearance.BorderSize = 0;
            btnMassIP.FlatStyle = FlatStyle.Flat;
            btnMassIP.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMassIP.ForeColor = Color.White;
            btnMassIP.Location = new Point(296, 218);
            btnMassIP.Name = "btnMassIP";
            btnMassIP.Size = new Size(200, 60);
            btnMassIP.TabIndex = 2;
            btnMassIP.Text = "Mass IP Tracker";
            btnMassIP.UseVisualStyleBackColor = false;
            // 
            // fadeTimer
            // 
            fadeTimer.Interval = 20;
            // 
            // pulseTimer
            // 
            pulseTimer.Interval = 50;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Image = Properties.Resources.NetworkIcon;
            logoPictureBox.Location = new Point(351, 86);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(100, 60);
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoPictureBox.TabIndex = 3;
            logoPictureBox.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, helpToolStripMenuItem, myMapToolStripMenuItem, gOOGLEMAPToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(797, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportToolStripMenuItem, importToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(137, 22);
            exportToolStripMenuItem.Text = "Export Data";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(137, 22);
            importToolStripMenuItem.Text = "Import Data";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(137, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pingSettingsToolStripMenuItem, themeToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // pingSettingsToolStripMenuItem
            // 
            pingSettingsToolStripMenuItem.Name = "pingSettingsToolStripMenuItem";
            pingSettingsToolStripMenuItem.Size = new Size(143, 22);
            pingSettingsToolStripMenuItem.Text = "Ping Settings";
            // 
            // themeToolStripMenuItem
            // 
            themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            themeToolStripMenuItem.Size = new Size(143, 22);
            themeToolStripMenuItem.Text = "Theme";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, documentationToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(157, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click_1;
            // 
            // documentationToolStripMenuItem
            // 
            documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            documentationToolStripMenuItem.Size = new Size(157, 22);
            documentationToolStripMenuItem.Text = "Documentation";
            // 
            // myMapToolStripMenuItem
            // 
            myMapToolStripMenuItem.Name = "myMapToolStripMenuItem";
            myMapToolStripMenuItem.Size = new Size(63, 20);
            myMapToolStripMenuItem.Text = "My Map";
            myMapToolStripMenuItem.Click += myMapToolStripMenuItem_Click;
            // 
            // gOOGLEMAPToolStripMenuItem
            // 
            gOOGLEMAPToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gOOGLEMAPToolStripMenuItem1, sTREETMAPToolStripMenuItem });
            gOOGLEMAPToolStripMenuItem.Name = "gOOGLEMAPToolStripMenuItem";
            gOOGLEMAPToolStripMenuItem.Size = new Size(45, 20);
            gOOGLEMAPToolStripMenuItem.Text = "MAP";
            // 
            // gOOGLEMAPToolStripMenuItem1
            // 
            gOOGLEMAPToolStripMenuItem1.Name = "gOOGLEMAPToolStripMenuItem1";
            gOOGLEMAPToolStripMenuItem1.Size = new Size(180, 22);
            gOOGLEMAPToolStripMenuItem1.Text = "GOOGLE MAP";
            gOOGLEMAPToolStripMenuItem1.Click += gOOGLEMAPToolStripMenuItem1_Click;
            // 
            // sTREETMAPToolStripMenuItem
            // 
            sTREETMAPToolStripMenuItem.Name = "sTREETMAPToolStripMenuItem";
            sTREETMAPToolStripMenuItem.Size = new Size(180, 22);
            sTREETMAPToolStripMenuItem.Text = "STREET MAP";
            sTREETMAPToolStripMenuItem.Click += sTREETMAPToolStripMenuItem_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabDashboard);
            tabControl1.Controls.Add(tabTools);
            tabControl1.Location = new Point(0, 27);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 400);
            tabControl1.TabIndex = 5;
            // 
            // tabDashboard
            // 
            tabDashboard.Controls.Add(lblTitle);
            tabDashboard.Controls.Add(logoPictureBox);
            tabDashboard.Controls.Add(btnMassIP);
            tabDashboard.Controls.Add(btnSingleIP);
            tabDashboard.Location = new Point(4, 24);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Padding = new Padding(3);
            tabDashboard.Size = new Size(792, 372);
            tabDashboard.TabIndex = 0;
            tabDashboard.Text = "Dashboard";
            tabDashboard.UseVisualStyleBackColor = true;
            // 
            // tabTools
            // 
            tabTools.Controls.Add(btnPacketTracker);
            tabTools.Controls.Add(btnWiFiScanner);
            tabTools.Controls.Add(BtnPingVulnerabilityScan);
            tabTools.Controls.Add(btnPortScanner);
            tabTools.Controls.Add(btnNetworkMonitor);
            tabTools.Controls.Add(btnPingTest);
            tabTools.Location = new Point(4, 24);
            tabTools.Name = "tabTools";
            tabTools.Padding = new Padding(3);
            tabTools.Size = new Size(792, 372);
            tabTools.TabIndex = 1;
            tabTools.Text = "Tools";
            tabTools.UseVisualStyleBackColor = true;
            // 
            // btnPacketTracker
            // 
            btnPacketTracker.BackColor = Color.FromArgb(0, 120, 215);
            btnPacketTracker.FlatAppearance.BorderSize = 0;
            btnPacketTracker.FlatStyle = FlatStyle.Flat;
            btnPacketTracker.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnPacketTracker.ForeColor = Color.White;
            btnPacketTracker.Location = new Point(27, 50);
            btnPacketTracker.Name = "btnPacketTracker";
            btnPacketTracker.Size = new Size(200, 60);
            btnPacketTracker.TabIndex = 5;
            btnPacketTracker.Text = "PACKET TRACKER";
            btnPacketTracker.UseVisualStyleBackColor = false;
            btnPacketTracker.Click += btnPacketTracker_Click;
            // 
            // btnWiFiScanner
            // 
            btnWiFiScanner.BackColor = Color.FromArgb(0, 120, 215);
            btnWiFiScanner.FlatAppearance.BorderSize = 0;
            btnWiFiScanner.FlatStyle = FlatStyle.Flat;
            btnWiFiScanner.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnWiFiScanner.ForeColor = Color.White;
            btnWiFiScanner.Location = new Point(555, 130);
            btnWiFiScanner.Name = "btnWiFiScanner";
            btnWiFiScanner.Size = new Size(200, 60);
            btnWiFiScanner.TabIndex = 4;
            btnWiFiScanner.Text = "WiFiScanner";
            btnWiFiScanner.UseVisualStyleBackColor = false;
            btnWiFiScanner.Click += btnWiFiScanner_Click;
            // 
            // BtnPingVulnerabilityScan
            // 
            BtnPingVulnerabilityScan.BackColor = Color.FromArgb(0, 120, 215);
            BtnPingVulnerabilityScan.FlatAppearance.BorderSize = 0;
            BtnPingVulnerabilityScan.FlatStyle = FlatStyle.Flat;
            BtnPingVulnerabilityScan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            BtnPingVulnerabilityScan.ForeColor = Color.White;
            BtnPingVulnerabilityScan.Location = new Point(555, 50);
            BtnPingVulnerabilityScan.Name = "BtnPingVulnerabilityScan";
            BtnPingVulnerabilityScan.Size = new Size(200, 60);
            BtnPingVulnerabilityScan.TabIndex = 3;
            BtnPingVulnerabilityScan.Text = "PingVulnerabilityScan";
            BtnPingVulnerabilityScan.UseVisualStyleBackColor = false;
            BtnPingVulnerabilityScan.Click += BtnPingVulnerabilityScan_Click;
            // 
            // btnPortScanner
            // 
            btnPortScanner.BackColor = Color.FromArgb(0, 120, 215);
            btnPortScanner.FlatAppearance.BorderSize = 0;
            btnPortScanner.FlatStyle = FlatStyle.Flat;
            btnPortScanner.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnPortScanner.ForeColor = Color.White;
            btnPortScanner.Location = new Point(300, 50);
            btnPortScanner.Name = "btnPortScanner";
            btnPortScanner.Size = new Size(200, 60);
            btnPortScanner.TabIndex = 0;
            btnPortScanner.Text = "Port Scanner";
            btnPortScanner.UseVisualStyleBackColor = false;
            // 
            // btnNetworkMonitor
            // 
            btnNetworkMonitor.BackColor = Color.FromArgb(0, 120, 215);
            btnNetworkMonitor.FlatAppearance.BorderSize = 0;
            btnNetworkMonitor.FlatStyle = FlatStyle.Flat;
            btnNetworkMonitor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNetworkMonitor.ForeColor = Color.White;
            btnNetworkMonitor.Location = new Point(300, 130);
            btnNetworkMonitor.Name = "btnNetworkMonitor";
            btnNetworkMonitor.Size = new Size(200, 60);
            btnNetworkMonitor.TabIndex = 1;
            btnNetworkMonitor.Text = "Network Monitor";
            btnNetworkMonitor.UseVisualStyleBackColor = false;
            // 
            // btnPingTest
            // 
            btnPingTest.BackColor = Color.FromArgb(0, 120, 215);
            btnPingTest.FlatAppearance.BorderSize = 0;
            btnPingTest.FlatStyle = FlatStyle.Flat;
            btnPingTest.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnPingTest.ForeColor = Color.White;
            btnPingTest.Location = new Point(300, 210);
            btnPingTest.Name = "btnPingTest";
            btnPingTest.Size = new Size(200, 60);
            btnPingTest.TabIndex = 2;
            btnPingTest.Text = "Advanced Ping Test";
            btnPingTest.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(797, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(80, 17);
            lblStatus.Text = "Ready to scan";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(797, 450);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TDM IP Tracker - Professional Network Toolkit";
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabDashboard.ResumeLayout(false);
            tabTools.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button BtnPingVulnerabilityScan;
        private Button btnWiFiScanner;
        private Button btnPacketTracker;
        private ToolStripMenuItem myMapToolStripMenuItem;
        private ToolStripMenuItem gOOGLEMAPToolStripMenuItem;
        private ToolStripMenuItem gOOGLEMAPToolStripMenuItem1;
        private ToolStripMenuItem sTREETMAPToolStripMenuItem;
    }
    }