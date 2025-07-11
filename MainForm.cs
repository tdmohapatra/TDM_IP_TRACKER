﻿using Microsoft.VisualBasic.Devices;
using SharpPcap;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class MainForm : Form
    {
        private float glowIntensity = 0f;
        private bool glowDirection = true;
        private bool darkMode = false;

        public MainForm()
        {
            byte[] iconBytes = Properties.Resources.TDM_NETWORK_APP;
            using (MemoryStream ms = new MemoryStream(iconBytes))
            {
                this.Icon = new Icon(ms);
            }
            // where MyIcon is a resource in your project
            InitializeComponent();
            WireUpEvents();
        }

        private void WireUpEvents()
        {
            // Form events
            this.Load += MainForm_Load;

            // Title label
            this.lblTitle.Paint += lblTitle_Paint;

            // Main buttons
            this.btnSingleIP.Click += btnSingleIP_Click;
            this.btnMassIP.Click += btnMassIP_Click;
            this.btnSingleIP.MouseEnter += Button_MouseEnter;
            this.btnSingleIP.MouseLeave += Button_MouseLeave;
            this.btnMassIP.MouseEnter += Button_MouseEnter;
            this.btnMassIP.MouseLeave += Button_MouseLeave;

            // Tool buttons
            this.btnPortScanner.Click += btnPortScanner_Click;
            this.btnNetworkMonitor.Click += btnNetworkMonitor_Click;
            this.btnPingTest.Click += btnPingTest_Click;
            this.btnPortScanner.MouseEnter += Button_MouseEnter;
            this.btnPortScanner.MouseLeave += Button_MouseLeave;
            this.btnNetworkMonitor.MouseEnter += Button_MouseEnter;
            this.btnNetworkMonitor.MouseLeave += Button_MouseLeave;
            this.btnPingTest.MouseEnter += Button_MouseEnter;
            this.btnPingTest.MouseLeave += Button_MouseLeave;

            // Timers
            this.fadeTimer.Tick += fadeTimer_Tick;
            this.pulseTimer.Tick += pulseTimer_Tick;

            // Menu items
            this.exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            this.importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            this.exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            this.pingSettingsToolStripMenuItem.Click += pingSettingsToolStripMenuItem_Click;
            this.themeToolStripMenuItem.Click += themeToolStripMenuItem_Click;
            this.aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            this.documentationToolStripMenuItem.Click += documentationToolStripMenuItem_Click;
        }

        #region Form and Animation Handlers
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            fadeTimer.Start();
            pulseTimer.Start();
            UpdateStatus("Application loaded successfully");
        }

        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
            else
                fadeTimer.Stop();
        }

        private void pulseTimer_Tick(object sender, EventArgs e)
        {
            if (glowDirection)
            {
                glowIntensity += 0.05f;
                if (glowIntensity >= 0.8f)
                    glowDirection = false;
            }
            else
            {
                glowIntensity -= 0.05f;
                if (glowIntensity <= 0.2f)
                    glowDirection = true;
            }
            lblTitle.Invalidate();
        }

        private void lblTitle_Paint(object sender, PaintEventArgs e)
        {
            //Label label = (Label)sender;

            //// Draw glowing text effect
            //for (int i = 0; i < 5; i++)
            //{
            //    using (var glowBrush = new SolidBrush(Color.FromArgb(
            //        (int)(glowIntensity * 255),
            //        0, 120, 215)))
            //    {
            //        e.Graphics.DrawString(label.Text, label.Font, glowBrush,
            //            new PointF(label.ClientRectangle.X + i, label.ClientRectangle.Y + i));
            //    }
            //}

            //// Draw main text (centered)
            //TextRenderer.DrawText(e.Graphics, label.Text, label.Font,
            //    label.ClientRectangle, label.ForeColor,
            //    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(0, 150, 255);
            button.Size = new Size(210, 65);
            button.Location = new Point(button.Location.X - 5, button.Location.Y - 2);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.FromArgb(0, 120, 215);
            button.Size = new Size(200, 60);
            button.Location = new Point(button.Location.X + 5, button.Location.Y + 2);
        }
        #endregion

        #region Main Functionality Handlers
        private void btnSingleIP_Click(object sender, EventArgs e)
        {
            UpdateStatus("Launching Single IP Scanner...");
            var singleIPForm = new SingleIPForm();
            LoadFormIcon(singleIPForm);
            singleIPForm.Show();
        }

        private void btnMassIP_Click(object sender, EventArgs e)
        {
            UpdateStatus("Launching Mass IP Scanner...");
            var massIPForm = new MassIPForm();
            LoadFormIcon(massIPForm);

            massIPForm.Show();
        }

        private void btnPortScanner_Click(object sender, EventArgs e)
        {
            UpdateStatus("Launching Port Scanner...");
            var portScannerForm = new PortScannerForm();
            LoadFormIcon(portScannerForm);

            portScannerForm.Show();
        }

        private void btnNetworkMonitor_Click(object sender, EventArgs e)
        {
            UpdateStatus("Launching Network Monitor...");
            var networkMonitorForm = new NetworkMonitorForm();
            LoadFormIcon(networkMonitorForm);

            networkMonitorForm.Show();
        }

        private void btnPingTest_Click(object sender, EventArgs e)
        {
            UpdateStatus("Launching Advanced Ping Tool...");
            var pingTestForm = new PingTestForm();
            LoadFormIcon(pingTestForm);

            pingTestForm.Show();
        }
        #endregion

        #region Menu Handlers
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus("Preparing to export data...");
            // Implement export functionality
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // Export logic here
                UpdateStatus($"Data exported to {saveDialog.FileName}");
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus("Preparing to import data...");
            // Implement import functionality
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Import logic here
                UpdateStatus($"Data imported from {openDialog.FileName}");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus("Exiting application...");
            Application.Exit();
        }

        private void pingSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus("Opening Ping Settings...");
            var settingsForm = new PingSettingsForm();
            settingsForm.ShowDialog();
        }

        private void themeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkMode = !darkMode;
            ApplyTheme();
            UpdateStatus(darkMode ? "Dark theme activated" : "Light theme activated");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus("Showing about information...");
            MessageBox.Show("TDM IP Tracker\nVersion 2.0\n© 2023 TDM Solutions",
                          "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatus("Opening documentation...");
            try
            {
                System.Diagnostics.Process.Start("https://tdm-ip-tracker.docs.example.com");
            }
            catch (Exception ex)
            {
                UpdateStatus("Error opening documentation: " + ex.Message);
            }
        }
        #endregion

        #region Utility Methods
        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
            lblStatus.ToolTipText = message;
        }

        private void ApplyTheme()
        {
            if (darkMode)
            {
                // Dark theme colors
                this.BackColor = Color.FromArgb(32, 32, 32);
                lblTitle.ForeColor = Color.FromArgb(0, 150, 255);
                tabDashboard.BackColor = Color.FromArgb(40, 40, 40);
                tabTools.BackColor = Color.FromArgb(40, 40, 40);
                statusStrip1.BackColor = Color.FromArgb(60, 60, 60);
                lblStatus.ForeColor = Color.White;
            }
            else
            {
                // Light theme colors
                this.BackColor = Color.FromArgb(240, 240, 240);
                lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
                tabDashboard.BackColor = SystemColors.Control;
                tabTools.BackColor = SystemColors.Control;
                statusStrip1.BackColor = SystemColors.Control;
                lblStatus.ForeColor = SystemColors.ControlText;
            }
        }



        #endregion


        private void BtnPingVulnerabilityScan_Click(object sender, EventArgs e)
        {
            // Create an instance of the PingVulnerabilityScannerForm
            PingVulnerabilityScannerForm pingVulnerabilityForm = new PingVulnerabilityScannerForm();
            LoadFormIcon(pingVulnerabilityForm);

            // Show the form
            pingVulnerabilityForm.Show();

        }
        private void btnWiFiScanner_Click(object sender, EventArgs e)
        {
            // Create an instance of the WiFiScannerForm
            WiFiScannerForm WiFiScanner = new WiFiScannerForm();

            // Load the icon for the new form
            LoadFormIcon(WiFiScanner);

            // Show the form
            WiFiScanner.Show();
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the AboutForm
            AboutForm aboutForm = new AboutForm();

            // Load the icon for the new form
            LoadFormIcon(aboutForm);

            // Show the form
            aboutForm.Show();
        }

        private void btnPacketTracker_Click(object sender, EventArgs e)
        {
            // Create an instance of the PacketTrackerForm
            PacketTrackerForm packetTrackerFrm = new PacketTrackerForm();

            // Load the icon for the new form
            LoadFormIcon(packetTrackerFrm);

            // Show the form
            packetTrackerFrm.Show();
        }

        private void gOOGLEMAPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Create an instance of the GoogleMapPage
            GoogleMapPage MyGoogleForm = new GoogleMapPage();

            // Load the icon for the new form
            LoadFormIcon(MyGoogleForm);

            // Show the form
            MyGoogleForm.Show();
        }

        private void sTREETMAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the OpenStreetMapPage
            OpenStreetMapPage OpenStreetForm = new OpenStreetMapPage();

            // Load the icon for the new form
            LoadFormIcon(OpenStreetForm);

            // Show the form
            OpenStreetForm.Show();
        }

        private void documentationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the NetworkCommandReference
            NetworkCommandReference networkCommandRef = new NetworkCommandReference();

            // Load the icon for the new form
            LoadFormIcon(networkCommandRef);

            // Show the form
            networkCommandRef.Show();
        }



        private void LoadFormIcon(Form form)
        {
            try
            {
                // Load the icon from resources as a byte array
                byte[] iconBytes = Properties.Resources.TDM_NETWORK_APP; // Ensure the resource name matches

                // Convert the byte array to a stream
                using (MemoryStream ms = new MemoryStream(iconBytes))
                {
                    // Load the icon from the stream and assign it to the form's Icon property
                    form.Icon = new Icon(ms);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., if the icon isn't found or there's an issue with the resources)
                MessageBox.Show($"Error loading icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void documentationToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            NetworkCommandReference ncrform= new NetworkCommandReference();
            LoadFormIcon(ncrform);

            ncrform.Show();

        }

        //private void LoadFormIcon()
        //{
        //    try
        //    {
        //        // Load the icon from resources as a byte array
        //        byte[] iconBytes = Properties.Resources.TDM_NETWORK_APP; // Make sure the resource name matches

        //        // Convert the byte array to a stream
        //        using (MemoryStream ms = new MemoryStream(iconBytes))
        //        {
        //            // Load the icon from the stream and assign it to the form's Icon property
        //            this.Icon = new Icon(ms);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions (e.g., if the icon isn't found or there's an issue with the resources)
        //        MessageBox.Show($"Error loading icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // Other code related to the AboutForm, such as button clicks or event handlers...
    }
}
