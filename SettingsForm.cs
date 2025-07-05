using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDM_IP_Tracker
{
    public class SettingsForm : Form
    {
        private int _pingTimeout; // Backing field for PingTimeout
        private int _maxConcurrentPings; // Backing field for MaxConcurrentPings

        public int PingTimeout
        {
            get => _pingTimeout;
            private set => _pingTimeout = value;
        }

        public int MaxConcurrentPings
        {
            get => _maxConcurrentPings;
            private set => _maxConcurrentPings = value;
        }

        public SettingsForm(int currentTimeout, int currentConcurrency)
        {
            this.Text = "Ping Settings";
            this.Size = new Size(350, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblTimeout = new Label() { Text = "Ping Timeout (ms):", Left = 20, Top = 20, AutoSize = true };
            NumericUpDown numTimeout = new NumericUpDown()
            {
                Minimum = 100,
                Maximum = 10000,
                Value = currentTimeout,
                Increment = 100,
                Left = 180,
                Top = 18,
                Width = 120
            };

            Label lblConcurrency = new Label() { Text = "Max Concurrent Pings:", Left = 20, Top = 60, AutoSize = true };
            NumericUpDown numConcurrency = new NumericUpDown()
            {
                Minimum = 1,
                Maximum = 100,
                Value = currentConcurrency,
                Left = 180,
                Top = 58,
                Width = 120
            };

            Button btnOk = new Button() { Text = "OK", DialogResult = DialogResult.OK, Left = 180, Top = 100 };
            Button btnCancel = new Button() { Text = "Cancel", DialogResult = DialogResult.Cancel, Left = 260, Top = 100 };

            btnOk.Click += (s, e) =>
            {
                PingTimeout = (int)numTimeout.Value;
                MaxConcurrentPings = (int)numConcurrency.Value;
            };

            Controls.AddRange(new Control[] { lblTimeout, numTimeout, lblConcurrency, numConcurrency, btnOk, btnCancel });

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }

}
