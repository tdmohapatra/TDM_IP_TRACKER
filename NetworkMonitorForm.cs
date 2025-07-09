using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net.Http;

namespace TDM_IP_Tracker
{
    public partial class NetworkMonitorForm : Form
    {
        private PerformanceCounter sentCounter;
        private PerformanceCounter receivedCounter;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        private NetworkInterface selectedInterface;
        private DateTime lastUpdateTime;

        private readonly HttpClient httpClient = new();

        public NetworkMonitorForm()
        {
            InitializeComponent();
            InitializeCounters();
            InitializeCharts();
            LoadNetworkInterfaces();
            lastUpdateTime = DateTime.Now;
        }

        private void InitializeCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }

        private void InitializeCharts()
        {
            // Clear existing setup
            chartNetworkTraffic.Series.Clear();
            chartNetworkTraffic.ChartAreas.Clear();

            var networkArea = new ChartArea("NetworkArea");
            networkArea.AxisX.Title = "Time (s)";
            networkArea.AxisY.Title = "Bytes/s";
            networkArea.AxisY.Minimum = 0;
            chartNetworkTraffic.ChartAreas.Add(networkArea);

            var downloadSeries = new Series("Download")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2
            };

            var uploadSeries = new Series("Upload")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2
            };

            chartNetworkTraffic.Series.Add(downloadSeries);
            chartNetworkTraffic.Series.Add(uploadSeries);

            // System usage chart
            chartSystemUsage.Series.Clear();
            chartSystemUsage.ChartAreas.Clear();

            var systemArea = new ChartArea("SystemArea");
            systemArea.AxisX.Title = "Time (s)";
            systemArea.AxisY.Title = "% Usage";
            systemArea.AxisY.Minimum = 0;
            systemArea.AxisY.Maximum = 100;
            chartSystemUsage.ChartAreas.Add(systemArea);

            var cpuSeries = new Series("CPU")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2
            };

            var ramSeries = new Series("RAM")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Orange,
                BorderWidth = 2
            };

            chartSystemUsage.Series.Add(cpuSeries);
            chartSystemUsage.Series.Add(ramSeries);
        }

        private void LoadNetworkInterfaces()
        {
            cmbInterfaces.Items.Clear();

            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up).ToList();

            foreach (var nic in interfaces)
            {
                cmbInterfaces.Items.Add(nic.Name);
            }

            if (cmbInterfaces.Items.Count > 0)
            {
                cmbInterfaces.SelectedIndex = 0;
            }
        }

        private void cmbInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedName = cmbInterfaces.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedName)) return;

            selectedInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(nic => nic.Name == selectedName);

            if (selectedInterface != null)
            {
                UpdateInterfaceDetails();
                SetupPerformanceCounters();
            }
        }

        private void UpdateInterfaceDetails()
        {
            var ipProps = selectedInterface.GetIPProperties();
            var ipv4Address = ipProps.UnicastAddresses
                .FirstOrDefault(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)?.Address.ToString() ?? "N/A";

            lblInternalIP.Text = $"Internal IP: {ipv4Address}";
            lblMAC.Text = $"MAC: {FormatMacAddress(selectedInterface.GetPhysicalAddress())}";
            lblSpeed.Text = $"Speed: {selectedInterface.Speed / 1_000_000} Mbps";
            lblStatus.Text = $"Status: {selectedInterface.OperationalStatus}";

            _ = UpdateExternalIPAsync();
            _ = UpdatePingAsync("8.8.8.8"); // Ping Google DNS
        }

        private static string FormatMacAddress(PhysicalAddress address)
        {
            return string.Join(":", address.GetAddressBytes().Select(b => b.ToString("X2")));
        }

        private async Task UpdateExternalIPAsync()
        {
            try
            {
                var externalIP = await httpClient.GetStringAsync("https://api.ipify.org");
                lblExternalIP.Text = $"External IP: {externalIP}";
            }
            catch
            {
                lblExternalIP.Text = "External IP: N/A";
            }
        }

        private async Task UpdatePingAsync(string host)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(host, 1000);
                lblPing.Text = reply.Status == IPStatus.Success
                    ? $"Ping: {reply.RoundtripTime} ms"
                    : "Ping: Timeout";
            }
            catch
            {
                lblPing.Text = "Ping: Error";
            }
        }

        private void SetupPerformanceCounters()
        {
            try
            {
                sentCounter?.Dispose();
                receivedCounter?.Dispose();

                var instanceName = GetPerformanceCounterInstanceName(selectedInterface);
                sentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instanceName);
                receivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", instanceName);
            }
            catch
            {
                sentCounter = null;
                receivedCounter = null;
            }
        }

        private string GetPerformanceCounterInstanceName(NetworkInterface nic)
        {
            var category = new PerformanceCounterCategory("Network Interface");
            var instances = category.GetInstanceNames();

            // Match by interface Id substring (usually first 12 chars)
            var idFragment = nic.Id.Substring(0, Math.Min(12, nic.Id.Length));

            foreach (var instance in instances)
            {
                if (instance.Contains(idFragment))
                {
                    return instance;
                }
            }

            // fallback
            return instances.FirstOrDefault() ?? string.Empty;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblRealTimeClock.Text = DateTime.Now.ToString("HH:mm:ss");

            if (sentCounter == null || receivedCounter == null) return;

            float bytesSentPerSec = sentCounter.NextValue();
            float bytesReceivedPerSec = receivedCounter.NextValue();

            toolStripStatusLabelUpload.Text = $"Upload: {FormatBytes(bytesSentPerSec)}/s";
            toolStripStatusLabelDownload.Text = $"Download: {FormatBytes(bytesReceivedPerSec)}/s";

            UpdateNetworkTrafficChart(bytesSentPerSec, bytesReceivedPerSec);

            toolStripStatusLabelConnections.Text = $"Connections: {dgvConnections.Rows.Count}";

            float cpuUsage = cpuCounter.NextValue();
            float ramUsage = ramCounter.NextValue();

            toolStripStatusLabelCPU.Text = $"CPU: {cpuUsage:0.0}%";
            toolStripStatusLabelRAM.Text = $"RAM: {ramUsage:0.0}%";

            UpdateSystemUsageChart(cpuUsage, ramUsage);
        }

        private void UpdateNetworkTrafficChart(float upload, float download)
        {
            var uploadSeries = chartNetworkTraffic.Series["Upload"];
            var downloadSeries = chartNetworkTraffic.Series["Download"];

            if (uploadSeries.Points.Count > 60)
            {
                uploadSeries.Points.RemoveAt(0);
                downloadSeries.Points.RemoveAt(0);
            }

            uploadSeries.Points.AddY(upload);
            downloadSeries.Points.AddY(download);
        }

        private void UpdateSystemUsageChart(float cpu, float ram)
        {
            var cpuSeries = chartSystemUsage.Series["CPU"];
            var ramSeries = chartSystemUsage.Series["RAM"];

            if (cpuSeries.Points.Count > 60)
            {
                cpuSeries.Points.RemoveAt(0);
                ramSeries.Points.RemoveAt(0);
            }

            cpuSeries.Points.AddY(cpu);
            ramSeries.Points.AddY(ram);
        }

        private static string FormatBytes(float bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadNetworkInterfaces();
        }

        private void LoadActiveConnections()
        {
            dgvConnections.Rows.Clear();
            try
            {
                var tcpConnections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();

                foreach (var conn in tcpConnections)
                {
                    string procName = "N/A";
                    try
                    {
                        var pid = GetPidFromTcpConn(conn);
                        if (pid != 0)
                        {
                            var proc = Process.GetProcessById(pid);
                            procName = proc.ProcessName;
                        }
                    }
                    catch { /* Ignore errors */ }

                    dgvConnections.Rows.Add("TCP", conn.LocalEndPoint.ToString(), conn.RemoteEndPoint.ToString(), conn.State.ToString(), procName);
                }
            }
            catch
            {
                // Optional: log or handle errors
            }
        }

        private int GetPidFromTcpConn(TcpConnectionInformation conn)
        {
            // Getting PID from TcpConnectionInformation is non-trivial and requires PInvoke or third-party libraries.
            // Return 0 as placeholder.
            return 0;
        }

        private void btnStopAction_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Monitoring stopped.");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            timer1.Start();
            LoadActiveConnections();
        }
    }
}
