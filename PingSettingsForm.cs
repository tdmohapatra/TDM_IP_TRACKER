using System;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class PingSettingsForm : Form
    {
        // Properties to expose settings to the main app
        public int Timeout { get; private set; }
        public int PingCount { get; private set; }
        public int PacketSize { get; private set; }
        public int TTL { get; private set; }
        public bool DontFragment { get; private set; }
        public int Interval { get; private set; }
        public bool ResolveHostnames { get; private set; }

        public PingSettingsForm(int timeout = 4000, int pingCount = 4, int packetSize = 32, int ttl = 128, bool dontFragment = false, int interval = 1000, bool resolveHostnames = true)
        {
            InitializeComponent();

            // Set control values from arguments
            numTimeout.Value = timeout;
            numPingCount.Value = pingCount;
            numPacketSize.Value = packetSize;
            numTTL.Value = ttl;
            chkDontFragment.Checked = dontFragment;
            numInterval.Value = interval;
            chkResolveHostnames.Checked = resolveHostnames;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // You could add input validation here if needed

            // Save values from controls to properties
            Timeout = (int)numTimeout.Value;
            PingCount = (int)numPingCount.Value;
            PacketSize = (int)numPacketSize.Value;
            TTL = (int)numTTL.Value;
            DontFragment = chkDontFragment.Checked;
            Interval = (int)numInterval.Value;
            ResolveHostnames = chkResolveHostnames.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
