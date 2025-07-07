using System;
using System.Drawing;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class SettingsForm : Form
    {
        public int AutoCheckInterval { get; private set; }
        public Color ThemeColor { get; private set; }

        public SettingsForm(int currentInterval, Color currentColor)
        {
           // InitializeComponent();
            AutoCheckInterval = currentInterval;
            ThemeColor = currentColor;
            InitializeControls();
        }

        private void InitializeControls()
        {
            this.Text = "Settings";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblInterval = new Label();
            lblInterval.Text = "Auto-check interval (ms):";
            lblInterval.Location = new Point(20, 20);
            lblInterval.AutoSize = true;

            NumericUpDown numInterval = new NumericUpDown();
            numInterval.Minimum = 1000;
            numInterval.Maximum = 60000;
            numInterval.Increment = 1000;
            numInterval.Value = AutoCheckInterval;
            numInterval.Location = new Point(180, 20);
            numInterval.Width = 150;

            Label lblColor = new Label();
            lblColor.Text = "Theme color:";
            lblColor.Location = new Point(20, 60);
            lblColor.AutoSize = true;

            Button btnColor = new Button();
            btnColor.Text = "Select Color";
            btnColor.Location = new Point(180, 60);
            btnColor.Width = 150;
            btnColor.Click += (s, e) =>
            {
                ColorDialog colorDialog = new ColorDialog();
                colorDialog.Color = ThemeColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ThemeColor = colorDialog.Color;
                }
            };

            Button btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(120, 220);
            btnOK.Width = 80;

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(220, 220);
            btnCancel.Width = 80;

            this.Controls.Add(lblInterval);
            this.Controls.Add(numInterval);
            this.Controls.Add(lblColor);
            this.Controls.Add(btnColor);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            // Update properties when OK is clicked
            btnOK.Click += (s, e) =>
            {
                AutoCheckInterval = (int)numInterval.Value;
            };
        }
    }
}