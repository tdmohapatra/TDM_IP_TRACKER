using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TDM_IP_Tracker
{
    public partial class AboutForm : Form
    {
        private bool darkMode = false;  // Flag to toggle between light and dark mode
        private Timer glowTimer;        // Timer for glow effect

        public AboutForm()
        {
            InitializeComponent();
            ApplyTheme();   // Apply the theme based on the mode
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            // Setting the text for various labels
            lblTitle.Text = "About TDM IP Tracker";
            lblDescription.Text = "TDM IP Tracker is a powerful network monitoring and security tool designed for professionals to track, analyze, and optimize network performance.";
            lblAboutMe.Text = "Developed by Tarakanta Dasmohapatra\nSenior Associate at Jindal Nature Cure\nContact: tmohapatra111@gmail.com\nMobile: 8114627876";
            lblProjectInfo.Text = "This project was created to gather detailed network information for security, speed, and performance optimization.";
            lblFeatures.Text = "Key Features:\n- Single IP Scanner\n- Mass IP Scanner\n- Port Scanner\n- Network Monitor\n- Ping Test\n- Wi-Fi Scanner\n- Packet Tracker";
            lblUseCases.Text = "Use Cases:\n- IT Security\n- Cybersecurity\n- Network Engineers\n- Healthcare IT\n- Home Networking\n- Educational Institutions";
            lblTechnicalStack.Text = "Technical Stack:\n- .NET Framework\n- C#\n- SharpPcap\n- Windows Forms";

            // Start glow animation effect
            AnimateGlowEffect();
        }

        private void ApplyTheme()
        {
            if (darkMode)
            {
                this.BackColor = Color.FromArgb(35, 35, 35);  // Dark background color
                lblTitle.ForeColor = Color.FromArgb(0, 150, 255);  // Light Blue title color
                lblDescription.ForeColor = Color.White;
                lblAboutMe.ForeColor = Color.White;
                lblProjectInfo.ForeColor = Color.White;
                lblFeatures.ForeColor = Color.White;
                lblUseCases.ForeColor = Color.White;
                lblTechnicalStack.ForeColor = Color.White;
                btnClose.BackColor = Color.FromArgb(0, 150, 255);  // Button color
                btnClose.ForeColor = Color.White;
                btnClose.FlatStyle = FlatStyle.Flat;
                btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 255);
            }
            else
            {
                this.BackColor = Color.White;
                lblTitle.ForeColor = Color.FromArgb(0, 120, 215);  // Dark blue for title
                lblDescription.ForeColor = Color.Black;
                lblAboutMe.ForeColor = Color.Black;
                lblProjectInfo.ForeColor = Color.Black;
                lblFeatures.ForeColor = Color.Black;
                lblUseCases.ForeColor = Color.Black;
                lblTechnicalStack.ForeColor = Color.Black;
                btnClose.BackColor = Color.FromArgb(0, 120, 215);  // Button background color
                btnClose.ForeColor = Color.White;
                btnClose.FlatStyle = FlatStyle.Flat;
                btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            }
        }

        // This function animates the title text with a glowing effect.
        private void AnimateGlowEffect()
        {
            glowTimer = new Timer();
            glowTimer.Interval = 100; // Interval for glow effect
            glowTimer.Tick += (sender, args) =>
            {
                lblTitle.ForeColor = Color.FromArgb(
                    (int)(Math.Abs(Math.Sin(DateTime.Now.Ticks / 10000000.0) * 255)),
                    0,
                    120, 215);
            };
            glowTimer.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();  // Close the form when the button is clicked
        }

        #region Windows Form Designer generated code
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblAboutMe;
        private System.Windows.Forms.Label lblProjectInfo;
        private System.Windows.Forms.Label lblFeatures;
        private System.Windows.Forms.Label lblUseCases;
        private System.Windows.Forms.Label lblTechnicalStack;
        private System.Windows.Forms.Button btnClose;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblAboutMe = new System.Windows.Forms.Label();
            this.lblProjectInfo = new System.Windows.Forms.Label();
            this.lblFeatures = new System.Windows.Forms.Label();
            this.lblUseCases = new System.Windows.Forms.Label();
            this.lblTechnicalStack = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.Location = new Point(50, 20);
            this.lblTitle.Size = new Size(700, 50);
            this.lblTitle.Text = "About TDM IP Tracker";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblDescription
            this.lblDescription.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblDescription.Location = new Point(50, 80);
            this.lblDescription.Size = new Size(700, 80);
            this.lblDescription.Text = "TDM IP Tracker is a powerful network monitoring and security tool designed for professionals to track, analyze, and optimize network performance.";

            // lblAboutMe
            this.lblAboutMe.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblAboutMe.Location = new Point(50, 170);
            this.lblAboutMe.Size = new Size(700, 80);
            this.lblAboutMe.Text = "Developed by Tarakanta Dasmohapatra\nSenior Associate at Jindal Nature Cure\nContact: tmohapatra111@gmail.com\nMobile: 8114627876";

            // lblProjectInfo
            this.lblProjectInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblProjectInfo.Location = new Point(50, 260);
            this.lblProjectInfo.Size = new Size(700, 80);
            this.lblProjectInfo.Text = "This project was created to gather detailed network information for security, speed, and performance optimization.";

            // lblFeatures
            this.lblFeatures.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblFeatures.Location = new Point(50, 350);
            this.lblFeatures.Size = new Size(700, 80);
            this.lblFeatures.Text = "Key Features:\n- Single IP Scanner\n- Mass IP Scanner\n- Port Scanner\n- Network Monitor\n- Ping Test\n- Wi-Fi Scanner\n- Packet Tracker";

            // lblUseCases
            this.lblUseCases.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblUseCases.Location = new Point(50, 450);
            this.lblUseCases.Size = new Size(700, 80);
            this.lblUseCases.Text = "Use Cases:\n- IT Security\n- Cybersecurity\n- Network Engineers\n- Healthcare IT\n- Home Networking\n- Educational Institutions";

            // lblTechnicalStack
            this.lblTechnicalStack.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblTechnicalStack.Location = new Point(50, 540);
            this.lblTechnicalStack.Size = new Size(700, 60);
            this.lblTechnicalStack.Text = "Technical Stack:\n- .NET Framework\n- C#\n- SharpPcap\n- Windows Forms";

            // btnClose
            this.btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.btnClose.Location = new Point(650, 620);
            this.btnClose.Size = new Size(100, 40);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // AboutForm
            this.ClientSize = new Size(800, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblAboutMe);
            this.Controls.Add(this.lblProjectInfo);
            this.Controls.Add(this.lblFeatures);
            this.Controls.Add(this.lblUseCases);
            this.Controls.Add(this.lblTechnicalStack);
            this.Controls.Add(this.btnClose);
            this.Name = "AboutForm";
            this.Load += new EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
        }
        #endregion
    }
}
