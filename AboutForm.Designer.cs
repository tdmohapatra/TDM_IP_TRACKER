using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TDM_IP_Tracker
{
    public partial class AboutForm : Form
    {
        private bool darkMode = true;  // Default to dark mode for modern look
        private Timer glowTimer;
        private int glowPhase = 0;
        private Color[] glowColors = new Color[]
        {
            Color.FromArgb(0, 150, 255),   // Blue
            Color.FromArgb(0, 200, 255),     // Cyan
            Color.FromArgb(100, 220, 255),   // Light cyan
            Color.FromArgb(0, 200, 255),     // Cyan
            Color.FromArgb(0, 150, 255)      // Blue
        };

        public AboutForm()
        {
            InitializeComponent();
            ApplyTheme();
            SetupModernControls();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "TDM IP TRACKER";
            lblVersion.Text = "Version 1.0.0";
            lblDescription.Text = "A comprehensive network monitoring and security tool designed for IT professionals to track, analyze, and optimize network performance.";

            lblFeatures.Text = "✦ Single IP Scanner\n✦ Mass IP Scanner\n✦ Port Scanner\n✦ Network Monitor\n✦ Ping Test\n✦ Wi-Fi Scanner\n✦ Packet Tracker";
            lblUseCases.Text = "✦ IT Security\n✦ Cybersecurity\n✦ Network Engineering\n✦ Healthcare IT\n✦ Home Networking\n✦ Educational Institutions";
            lblTechnicalStack.Text = "✦ .NET Framework\n✦ C#\n✦ SharpPcap\n✦ Windows Forms";

            lblDeveloper.Text = "Developed by:";
            lblDeveloperName.Text = "Tarakanta Dasmohapatra";
            lblPosition.Text = "Senior Associate at Jindal Nature Cure";
            lblContact.Text = "tmohapatra111@gmail.com | +91 8114627876";

            AnimateGlowEffect();
        }

        private void SetupModernControls()
        {
            // Form styling
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(20);

            // Set rounded corners (Windows 11+ style)
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void ApplyTheme()
        {
            if (darkMode)
            {
                // Dark theme colors
                this.BackColor = Color.FromArgb(32, 32, 32);
                panelMain.BackColor = Color.FromArgb(40, 40, 40);
                panelMain.BorderStyle = BorderStyle.None;

                // Title styling
                lblTitle.ForeColor = Color.FromArgb(0, 150, 255);
                lblVersion.ForeColor = Color.FromArgb(150, 150, 150);
                lblDescription.ForeColor = Color.FromArgb(200, 200, 200);

                // Section styling
                lblFeatures.ForeColor = Color.FromArgb(220, 220, 220);
                lblUseCases.ForeColor = Color.FromArgb(220, 220, 220);
                lblTechnicalStack.ForeColor = Color.FromArgb(220, 220, 220);

                // Developer info
                lblDeveloper.ForeColor = Color.FromArgb(180, 180, 180);
                lblDeveloperName.ForeColor = Color.FromArgb(0, 180, 255);
                lblPosition.ForeColor = Color.FromArgb(180, 180, 180);
                lblContact.ForeColor = Color.FromArgb(180, 180, 180);

                // Button styling
                btnClose.BackColor = Color.FromArgb(0, 120, 215);
                btnClose.ForeColor = Color.White;
                btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 140, 235);
                btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 195);
            }
            else
            {
                // Light theme colors
                this.BackColor = Color.FromArgb(240, 240, 240);
                panelMain.BackColor = Color.White;
                panelMain.BorderStyle = BorderStyle.FixedSingle;

                // Title styling
                lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
                lblVersion.ForeColor = Color.FromArgb(100, 100, 100);
                lblDescription.ForeColor = Color.FromArgb(70, 70, 70);

                // Section styling
                lblFeatures.ForeColor = Color.FromArgb(50, 50, 50);
                lblUseCases.ForeColor = Color.FromArgb(50, 50, 50);
                lblTechnicalStack.ForeColor = Color.FromArgb(50, 50, 50);

                // Developer info
                lblDeveloper.ForeColor = Color.FromArgb(100, 100, 100);
                lblDeveloperName.ForeColor = Color.FromArgb(0, 120, 215);
                lblPosition.ForeColor = Color.FromArgb(100, 100, 100);
                lblContact.ForeColor = Color.FromArgb(100, 100, 100);

                // Button styling
                btnClose.BackColor = Color.FromArgb(0, 120, 215);
                btnClose.ForeColor = Color.White;
                btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 140, 235);
                btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 195);
            }

            // Common styling that applies to both themes
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 1;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void AnimateGlowEffect()
        {
            glowTimer = new Timer();
            glowTimer.Interval = 100;
            glowTimer.Tick += (sender, args) =>
            {
                glowPhase = (glowPhase + 1) % glowColors.Length;
                lblTitle.ForeColor = glowColors[glowPhase];
            };
            glowTimer.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Windows Form Designer generated code
        private Panel panelMain;
        private Label lblTitle;
        private Label lblVersion;
        private Label lblDescription;
        private Label lblFeatures;
        private Label lblUseCases;
        private Label lblTechnicalStack;
        private Label lblDeveloper;
        private Label lblDeveloperName;
        private Label lblPosition;
        private Label lblContact;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.panelMain = new Panel();
            this.lblTitle = new Label();
            this.lblVersion = new Label();
            this.lblDescription = new Label();
            this.lblFeatures = new Label();
            this.lblUseCases = new Label();
            this.lblTechnicalStack = new Label();
            this.lblDeveloper = new Label();
            this.lblDeveloperName = new Label();
            this.lblPosition = new Label();
            this.lblContact = new Label();
            this.btnClose = new Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();

            // panelMain
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblVersion);
            this.panelMain.Controls.Add(this.lblDescription);
            this.panelMain.Controls.Add(this.lblFeatures);
            this.panelMain.Controls.Add(this.lblUseCases);
            this.panelMain.Controls.Add(this.lblTechnicalStack);
            this.panelMain.Controls.Add(this.lblDeveloper);
            this.panelMain.Controls.Add(this.lblDeveloperName);
            this.panelMain.Controls.Add(this.lblPosition);
            this.panelMain.Controls.Add(this.lblContact);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Location = new Point(10, 10);
            this.panelMain.Size = new Size(760, 680);
            this.panelMain.TabIndex = 0;

            // lblTitle
            this.lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Size = new Size(720, 40);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblVersion
            this.lblVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblVersion.Location = new Point(20, 70);
            this.lblVersion.Size = new Size(720, 20);
            this.lblVersion.TextAlign = ContentAlignment.MiddleCenter;

            // lblDescription
            this.lblDescription.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblDescription.Location = new Point(30, 110);
            this.lblDescription.Size = new Size(700, 60);
            this.lblDescription.TextAlign = ContentAlignment.MiddleCenter;

            // Section headers and content with better spacing
            int yPos = 190;

            // Features section
            var lblFeaturesHeader = new Label();
            lblFeaturesHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblFeaturesHeader.Location = new Point(30, yPos);
            lblFeaturesHeader.Size = new Size(700, 25);
            lblFeaturesHeader.Text = "KEY FEATURES";
            lblFeaturesHeader.ForeColor = darkMode ? Color.FromArgb(0, 180, 255) : Color.FromArgb(0, 120, 215);
            panelMain.Controls.Add(lblFeaturesHeader);
            yPos += 30;

            this.lblFeatures.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblFeatures.Location = new Point(50, yPos);
            this.lblFeatures.Size = new Size(320, 150);
            yPos += 160;

            // Use Cases section
            var lblUseCasesHeader = new Label();
            lblUseCasesHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblUseCasesHeader.Location = new Point(30, yPos);
            lblUseCasesHeader.Size = new Size(700, 25);
            lblUseCasesHeader.Text = "USE CASES";
            lblUseCasesHeader.ForeColor = darkMode ? Color.FromArgb(0, 180, 255) : Color.FromArgb(0, 120, 215);
            panelMain.Controls.Add(lblUseCasesHeader);
            yPos += 30;

            this.lblUseCases.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblUseCases.Location = new Point(50, yPos);
            this.lblUseCases.Size = new Size(320, 150);
            yPos += 160;

            // Technical Stack section
            var lblTechHeader = new Label();
            lblTechHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTechHeader.Location = new Point(30, yPos);
            lblTechHeader.Size = new Size(700, 25);
            lblTechHeader.Text = "TECHNICAL STACK";
            lblTechHeader.ForeColor = darkMode ? Color.FromArgb(0, 180, 255) : Color.FromArgb(0, 120, 215);
            panelMain.Controls.Add(lblTechHeader);
            yPos += 30;

            this.lblTechnicalStack.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblTechnicalStack.Location = new Point(50, yPos);
            this.lblTechnicalStack.Size = new Size(320, 100);

            // Developer info (right side)
            int rightColX = 400;
            yPos = 190;

            var lblDevHeader = new Label();
            lblDevHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDevHeader.Location = new Point(rightColX, yPos);
            lblDevHeader.Size = new Size(300, 25);
            lblDevHeader.Text = "DEVELOPER INFORMATION";
            lblDevHeader.ForeColor = darkMode ? Color.FromArgb(0, 180, 255) : Color.FromArgb(0, 120, 215);
            panelMain.Controls.Add(lblDevHeader);
            yPos += 40;

            this.lblDeveloper.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblDeveloper.Location = new Point(rightColX, yPos);
            this.lblDeveloper.Size = new Size(300, 20);
            yPos += 30;

            this.lblDeveloperName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblDeveloperName.Location = new Point(rightColX, yPos);
            this.lblDeveloperName.Size = new Size(300, 25);
            yPos += 35;

            this.lblPosition.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblPosition.Location = new Point(rightColX, yPos);
            this.lblPosition.Size = new Size(300, 40);
            yPos += 50;

            this.lblContact.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblContact.Location = new Point(rightColX, yPos);
            this.lblContact.Size = new Size(300, 20);

            // btnClose
            this.btnClose.Size = new Size(120, 35);
            this.btnClose.Location = new Point(panelMain.Width - 140, panelMain.Height - 50);
            this.btnClose.Text = "Close";
            this.btnClose.TabIndex = 0;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // AboutForm
            this.ClientSize = new Size(780, 700);
            this.Controls.Add(this.panelMain);
            this.Text = "About TDM IP Tracker";
            this.Load += new EventHandler(this.AboutForm_Load);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
    }
}