using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TDM_IP_Tracker
{
    public class IPControlnNew: UserControl
    {
        private string ipAddress;
        private string hostName;
        private string section = "General";  // Added section field
        private IPStatus lastStatus;
        private long lastResponseTime;
        private DateTime lastChecked;
        private bool isChecked = true;

        public event EventHandler StatusChanged;

        // Modified constructor to accept optional section parameter
        public IPControlnNew(string ipAddress, string section = "General")
        {
            InitializeComponent();
            this.ipAddress = ipAddress;
            this.section = section;
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Padding = new Padding(5);
            this.Size = new Size(200, 120);  // Increased height to accommodate section

            // Create a table layout panel for better organization
            TableLayoutPanel tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.ColumnCount = 1;
            tableLayout.RowCount = 5;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Section label (new addition)
            lblSection = new Label();
            lblSection.Text = $"Section: {section}";
            lblSection.Dock = DockStyle.Top;
            lblSection.Padding = new Padding(5, 5, 0, 0);
            lblSection.Font = new Font(lblSection.Font, FontStyle.Bold);
            tableLayout.Controls.Add(lblSection, 0, 0);

            chkSelected = new CheckBox();
            chkSelected.Text = ipAddress;
            chkSelected.Checked = true;
            chkSelected.Dock = DockStyle.Top;
            tableLayout.Controls.Add(chkSelected, 0, 1);

            lblStatus = new Label();
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Text = "Status: Unknown";
            lblStatus.Padding = new Padding(5, 5, 0, 0);
            tableLayout.Controls.Add(lblStatus, 0, 2);

            lblResponse = new Label();
            lblResponse.Dock = DockStyle.Top;
            lblResponse.Text = "Response: N/A";
            lblResponse.Padding = new Padding(5, 5, 0, 0);
            tableLayout.Controls.Add(lblResponse, 0, 3);

            lblChecked = new Label();
            lblChecked.Dock = DockStyle.Top;
            lblChecked.Text = "Last checked: Never";
            lblChecked.Padding = new Padding(5, 5, 0, 5);
            tableLayout.Controls.Add(lblChecked, 0, 4);

            this.Controls.Add(tableLayout);

            chkSelected.CheckedChanged += (s, e) =>
            {
                Checked = chkSelected.Checked;
            };
        }

        public void Ping()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipAddress, 1000);
                    lastStatus = reply.Status;
                    lastResponseTime = reply.RoundtripTime;
                    lastChecked = DateTime.Now;

                    if (reply.Status == IPStatus.Success)
                    {
                        try
                        {
                            IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
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
            lblSection.Text = $"Section: {section}";  // Update section display

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

            lblChecked.Text = $"Last checked: {lastChecked.ToString("g")}";
        }

        #region Properties

        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; UpdateUI(); }
        }

        public string HostName
        {
            get { return hostName; }
        }

        public string Section  // New Section property
        {
            get { return section; }
            set
            {
                section = value;
                if (lblSection != null)
                    lblSection.Text = $"Section: {value}";
            }
        }

        public IPStatus LastStatus
        {
            get { return lastStatus; }
        }

        public long LastResponseTime
        {
            get { return lastResponseTime; }
        }

        public DateTime LastChecked
        {
            get { return lastChecked; }
        }

        public bool Checked
        {
            get { return isChecked; }
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
        private Label lblSection;  // Added for section display

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // IPControl
            // 
            Name = "IPControl";
            Load += IPControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private void IPControl_Load(object sender, EventArgs e)
        {

        }
    }
}