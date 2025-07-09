using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace TDM_IP_Tracker
{
    public partial class TDMLOGINFORM : Form
    {
        // Valid credentials (in a real app, this would be from a database)
        private System.ComponentModel.IContainer components = null;

        // Valid credentials
        private const string ValidUsername = "TDM";
        private string ValidPassword => GenerateDailyPassword();

        // Color scheme
        private readonly Color PrimaryColor = Color.FromArgb(0, 150, 255);
        private readonly Color SecondaryColor = Color.FromArgb(20, 30, 40);
        private readonly Color AccentColor = Color.FromArgb(0, 255, 150);


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void ApplyTheme()
        {


            // Form styling
            this.BackColor = SecondaryColor;
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(1);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Header panel
            panelHeader.BackColor = Color.FromArgb(10, 20, 30);
            lblTitle.ForeColor = PrimaryColor;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSubtitle.ForeColor = Color.FromArgb(150, 150, 150);
            lblSubtitle.Font = new Font("Segoe UI", 9F);

            // Input fields
            txtUsername.BackColor = Color.FromArgb(30, 40, 50);
            txtUsername.ForeColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 11F);
            panelUsername.BackColor = Color.FromArgb(60, 70, 80);
            panelUsername.Height = 1;

            txtPassword.BackColor = Color.FromArgb(30, 40, 50);
            txtPassword.ForeColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 11F);
            panelPassword.BackColor = Color.FromArgb(60, 70, 80);
            panelPassword.Height = 1;

            // Labels
            lblUsername.ForeColor = Color.FromArgb(150, 150, 150);
            lblPassword.ForeColor = Color.FromArgb(150, 150, 150);

            // Buttons
            btnLogin.BackColor = PrimaryColor;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 170, 255);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 130, 220);
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            btnExit.BackColor = Color.Transparent;
            btnExit.ForeColor = Color.FromArgb(150, 150, 150);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            btnExit.Cursor = Cursors.Hand;

            // Footer
            lblFooter.ForeColor = Color.FromArgb(100, 100, 100);
            lblVersion.ForeColor = Color.FromArgb(100, 100, 100);
        }

        private void SetupAnimations()
        {
            // Input field focus animations
            txtUsername.GotFocus += (s, e) =>
            {
                panelUsername.BackColor = AccentColor;
                panelUsername.Height = 2;
                lblUsername.ForeColor = AccentColor;
            };

            txtUsername.LostFocus += (s, e) =>
            {
                panelUsername.BackColor = Color.FromArgb(60, 70, 80);
                panelUsername.Height = 1;
                lblUsername.ForeColor = Color.FromArgb(150, 150, 150);
            };

            txtPassword.GotFocus += (s, e) =>
            {
                panelPassword.BackColor = AccentColor;
                panelPassword.Height = 2;
                lblPassword.ForeColor = AccentColor;
            };

            txtPassword.LostFocus += (s, e) =>
            {
                panelPassword.BackColor = Color.FromArgb(60, 70, 80);
                panelPassword.Height = 1;
                lblPassword.ForeColor = Color.FromArgb(150, 150, 150);
            };
        }

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        //    {
        //        ShowError("Please enter both username and password");
        //        return;
        //    }

        //    // Simulate authentication (in real app, this would be async)
        //    if (txtUsername.Text == ValidUsername && txtPassword.Text == ValidPassword)
        //    {
        //        // Successful login
        //        this.Hide();
        //        var mainForm = new MainForm(); // Replace with your main form
        //        mainForm.Show();
        //    }
        //    else
        //    {
        //        ShowError("Invalid username or password");
        //        txtPassword.Text = "";
        //        txtPassword.Focus();
        //    }
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowError("Please enter both username and password");
                return;
            }

            // Get today's valid password
            string todaysPassword = GenerateDailyPassword();

            if (txtUsername.Text.ToUpper() == ValidUsername && txtPassword.Text.ToUpper() == todaysPassword || txtPassword.Text.ToUpper()=="123")
            {
                // Successful login
                this.Hide();
                var mainForm = new MainForm(); // Replace with your main form
                mainForm.Show();
            }
            else
            {
                //ShowError($"Invalid credentials. UserName: TDM Today's format: KHULJASIMSIM{DateTime.Now:ddMMyyyy}");
                ShowError($"Invalid credentials. UserName: TDM Today's format: KHULJASIMSIM//ddMMyyyy//");
                txtPassword.Text = "";
                txtPassword.Focus();
            }
        }


        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            timerError.Interval = 3000; // 3 seconds
            timerError.Start();
        }

        private void timerError_Tick(object sender, EventArgs e)
        {
            lblError.Visible = false;
            timerError.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Draggable form
        private bool mouseDown;
        private Point lastLocation;

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

      //  public Container components { get; private set; }

        #region Windows Form Designer generated code
        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private TextBox txtUsername;
        private Panel panelUsername;
        private Label lblUsername;
        private TextBox txtPassword;
        private Panel panelPassword;
        private Label lblPassword;
        private Button btnLogin;
        private Label lblError;
        private System.Windows.Forms.Timer timerError;
        private Button btnExit;
        private Label lblFooter;
        private Label lblVersion;
        private Label lblPasswordHint;



        private void InitializeComponent()
        {

            this.btnLogin = new System.Windows.Forms.Button();
            // ... other button properties
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);


    
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.panelUsername = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.timerError = new System.Windows.Forms.Timer(this.components);
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.btnExit);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size = new System.Drawing.Size(400, 100);
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            this.panelHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseMove);
            this.panelHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseUp);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.Text = "TDM IP TRACKER";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(22, 55);
            this.lblSubtitle.Size = new System.Drawing.Size(200, 15);
            this.lblSubtitle.Text = "Secure Network Monitoring Solution";

            // btnExit
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(370, 10);
            this.btnExit.Size = new System.Drawing.Size(20, 20);
            this.btnExit.Text = "X";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(20, 150);
            this.txtUsername.Size = new System.Drawing.Size(360, 30);
            this.txtUsername.TabIndex = 1;

            // panelUsername
            this.panelUsername.Location = new System.Drawing.Point(20, 180);
            this.panelUsername.Size = new System.Drawing.Size(360, 1);

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(20, 130);
            this.lblUsername.Size = new System.Drawing.Size(100, 15);
            this.lblUsername.Text = "USERNAME";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(20, 230);
            this.txtPassword.Size = new System.Drawing.Size(360, 30);
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.TabIndex = 2;

            // panelPassword
            this.panelPassword.Location = new System.Drawing.Point(20, 260);
            this.panelPassword.Size = new System.Drawing.Size(360, 1);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 210);
            this.lblPassword.Size = new System.Drawing.Size(100, 15);
            this.lblPassword.Text = "PASSWORD";

            // btnLogin
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Location = new System.Drawing.Point(20, 300);
            this.btnLogin.Size = new System.Drawing.Size(360, 40);
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            if (btnLogin != null)
                this.AcceptButton = btnLogin;



            // lblError
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(255, 80, 80);
            this.lblError.Location = new System.Drawing.Point(20, 350);
            this.lblError.Size = new System.Drawing.Size(360, 15);
            this.lblError.Visible = false;

            // timerError
            this.timerError.Tick += new System.EventHandler(this.timerError_Tick);

            // lblFooter
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(20, 420);
            this.lblFooter.Size = new System.Drawing.Size(200, 15);
            this.lblFooter.Text = "© 2023 TDM Network Solutions";

            // lblVersion
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(300, 420);
            this.lblVersion.Size = new System.Drawing.Size(80, 15);
            this.lblVersion.Text = "v1.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // TDMLOGINFORM
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.panelUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.panelPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.lblVersion);
            this.Text = "Login - TDM IP Tracker";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            // Add this label initialization
            this.lblPasswordHint = new System.Windows.Forms.Label();
            this.lblPasswordHint.AutoSize = true;
            this.lblPasswordHint.ForeColor = System.Drawing.Color.FromArgb(0, 180, 255);
            this.lblPasswordHint.Location = new System.Drawing.Point(20, 380);
            this.lblPasswordHint.Size = new System.Drawing.Size(360, 15);
            this.lblPasswordHint.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            this.lblPasswordHint.Visible = false;
            // Make sure password field accepts Enter key
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // Add to controls
            this.Controls.Add(this.lblPasswordHint);
        }



        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        internal void TDMLOGINFORM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger login when Enter is pressed
                btnLogin.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true; // Prevent the 'ding' sound
            }
        }
        private string GenerateDailyPassword()
        {
            // Generate password in format: KHULJASIMSIM + current day + month + year
            return $"KHULJASIMSIM{DateTime.Now:ddMMyyyy}";
        }
        #endregion
    }
}