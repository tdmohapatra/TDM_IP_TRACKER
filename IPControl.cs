using System;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class IPControl : UserControl
    {
        private string ipAddress;
        private string hostName;
        private string section = "General";
        private IPStatus lastStatus;
        private long lastResponseTime;
        private DateTime lastChecked;
        private bool isChecked = true;

        public event EventHandler StatusChanged;

        public IPControl(string ipAddress, string section = "General")
        {
            this.ipAddress = ipAddress;
            this.section = section;
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            SuspendLayout();

            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Padding = new Padding(5);
            this.Size = new Size(200, 110);

            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                AutoSize = true,
            };

            lblSection = CreateLabel($"Section: {section}", FontStyle.Bold);
            chkSelected = new CheckBox { Text = ipAddress, Checked = true, Dock = DockStyle.Top };
            lblStatus = CreateLabel("Status: Unknown");
            lblResponse = CreateLabel("Response: N/A");
            lblChecked = CreateLabel("Last checked: Never");

            tableLayout.Controls.Add(lblSection, 0, 0);
            tableLayout.Controls.Add(chkSelected, 0, 1);
            tableLayout.Controls.Add(lblStatus, 0, 2);
            tableLayout.Controls.Add(lblResponse, 0, 3);
            tableLayout.Controls.Add(lblChecked, 0, 4);

            this.Controls.Add(tableLayout);

            chkSelected.CheckedChanged += (s, e) => Checked = chkSelected.Checked;

            ResumeLayout();
        }

        private Label CreateLabel(string text, FontStyle style = FontStyle.Regular)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Top,
                Padding = new Padding(5, 2, 0, 0),
                Font = new Font(SystemFonts.DefaultFont, style),
                AutoSize = true
            };
        }

        public async Task PingAsync()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(ipAddress, 1000);
                    lastStatus = reply.Status;
                    lastResponseTime = reply.RoundtripTime;
                    lastChecked = DateTime.Now;

                    if (reply.Status == IPStatus.Success && string.IsNullOrEmpty(hostName))
                    {
                        try
                        {
                            IPHostEntry hostEntry = await Dns.GetHostEntryAsync(ipAddress);
                            hostName = hostEntry.HostName;
                        }
                        catch
                        {
                            hostName = "Unknown";
                        }
                    }
                }
            }
            catch
            {
                lastStatus = IPStatus.Unknown;
                lastResponseTime = -1;
                lastChecked = DateTime.Now;
            }

            UpdateUI();
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateUI()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUI));
                return;
            }

            chkSelected.Text = $"{ipAddress} {(string.IsNullOrEmpty(hostName) ? "" : $"({hostName})")}";
            lblSection.Text = $"Section: {section}";

            if (lastStatus == IPStatus.Success)
            {
                lblStatus.Text = $"Status: Online";
                lblStatus.ForeColor = Color.Green;
                lblResponse.Text = $"Response: {lastResponseTime} ms";
                this.BackColor = Color.FromArgb(230, 245, 230);
            }
            else
            {
                lblStatus.Text = $"Status: Offline ({lastStatus})";
                lblStatus.ForeColor = Color.Red;
                lblResponse.Text = "Response: N/A";
                this.BackColor = Color.FromArgb(255, 230, 230);
            }

            lblChecked.Text = $"Last checked: {lastChecked:g}";
        }

        #region Properties

        public string IPAddress
        {
            get => ipAddress;
            set { ipAddress = value; UpdateUI(); }
        }

        public string HostName => hostName;

        public string Section
        {
            get => section;
            set
            {
                section = value;
                if (lblSection != null)
                    lblSection.Text = $"Section: {value}";
            }
        }

        public IPStatus LastStatus => lastStatus;

        public long LastResponseTime => lastResponseTime;

        public DateTime LastChecked => lastChecked;

        public bool Checked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    chkSelected.Checked = value;
                    StatusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Component Designer generated code

        private CheckBox chkSelected;
        private Label lblStatus;
        private Label lblResponse;
        private Label lblChecked;
        private Label lblSection;

        private void InitializeComponent()
        {
            SuspendLayout();
            Name = "IPControl";
            Load += IPControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private void IPControl_Load(object sender, EventArgs e) { }
    }
}
