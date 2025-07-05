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
using System.Management;
using System.Net.Http;
using System.Threading;

namespace TDM_IP_Tracker
{
    public partial class NetworkMonitorForm : Form
    {
        private PerformanceCounter sentCounter;
        private PerformanceCounter receivedCounter;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        private NetworkInterface selectedInterface;
        private long lastBytesSent;
        private long lastBytesReceived;

        private DateTime lastUpdateTime;

        private HttpClient httpClient = new HttpClient();

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
            // Network Traffic Chart Setup
            chartNetworkTraffic.Series.Clear();
            chartNetworkTraffic.ChartAreas.Clear();
            var area = new ChartArea("NetworkArea");
            area.AxisX.Title = "Time (s)";
            area.AxisY.Title = "Bytes/s";
            area.AxisY.Minimum = 0;
            chartNetworkTraffic.ChartAreas.Add(area);

            var seriesDownload = new Series("Download");
            seriesDownload.ChartType = SeriesChartType.Line;
            seriesDownload.Color = Color.Blue;
            seriesDownload.BorderWidth = 2;

            var seriesUpload = new Series("Upload");
            seriesUpload.ChartType = SeriesChartType.Line;
            seriesUpload.Color = Color.Red;
            seriesUpload.BorderWidth = 2;

            chartNetworkTraffic.Series.Add(seriesDownload);
            chartNetworkTraffic.Series.Add(seriesUpload);

            // System Usage Chart Setup
            chartSystemUsage.Series.Clear();
            chartSystemUsage.ChartAreas.Clear();
            var sysArea = new ChartArea("SystemArea");
            sysArea.AxisX.Title = "Time (s)";
            sysArea.AxisY.Title = "% Usage";
            sysArea.AxisY.Minimum = 0;
            sysArea.AxisY.Maximum = 100;
            chartSystemUsage.ChartAreas.Add(sysArea);

            var seriesCPU = new Series("CPU");
            seriesCPU.ChartType = SeriesChartType.Line;
            seriesCPU.Color = Color.Green;
            seriesCPU.BorderWidth = 2;

            var seriesRAM = new Series("RAM");
            seriesRAM.ChartType = SeriesChartType.Line;
            seriesRAM.Color = Color.Orange;
            seriesRAM.BorderWidth = 2;

            chartSystemUsage.Series.Add(seriesCPU);
            chartSystemUsage.Series.Add(seriesRAM);
        }

        private void LoadNetworkInterfaces()
        {
            cmbInterfaces.Items.Clear();
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up))
            {
                cmbInterfaces.Items.Add(ni.Name);
            }

            if (cmbInterfaces.Items.Count > 0)
            {
                cmbInterfaces.SelectedIndex = 0;
            }
        }

        private void cmbInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            var interfaceName = cmbInterfaces.SelectedItem.ToString();
            selectedInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(n => n.Name == interfaceName);

            if (selectedInterface != null)
            {
                UpdateInterfaceDetails();
                SetupPerformanceCounters();
                lastBytesSent = 0;
                lastBytesReceived = 0;
            }
        }

        private void UpdateInterfaceDetails()
        {
            var ipProps = selectedInterface.GetIPProperties();
            var ipv4Addr = ipProps.UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork)?.Address.ToString() ?? "N/A";

            lblInternalIP.Text = $"Internal IP: {ipv4Addr}";
            lblMAC.Text = $"MAC: {selectedInterface.GetPhysicalAddress()}";
            lblSpeed.Text = $"Speed: {selectedInterface.Speed / 1_000_000} Mbps";
            lblStatus.Text = $"Status: {selectedInterface.OperationalStatus}";

            _ = UpdateExternalIPAsync();

            UpdatePingAsync("8.8.8.8"); // Ping Google DNS
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

        private async void UpdatePingAsync(string host)
        {
            try
            {
                var ping = new Ping();
                var reply = await ping.SendPingAsync(host, 1000);
                if (reply.Status == IPStatus.Success)
                    lblPing.Text = $"Ping: {reply.RoundtripTime} ms";
                else
                    lblPing.Text = $"Ping: Timeout";
            }
            catch
            {
                lblPing.Text = $"Ping: Error";
            }
        }

        private void SetupPerformanceCounters()
        {
            try
            {
                if (sentCounter != null) sentCounter.Dispose();
                if (receivedCounter != null) receivedCounter.Dispose();

                string instanceName = GetPerformanceCounterInstanceName(selectedInterface);

                sentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instanceName);
                receivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", instanceName);
            }
            catch
            {
                sentCounter = null;
                receivedCounter = null;
            }
        }

        private string GetPerformanceCounterInstanceName(NetworkInterface ni)
        {
            var category = new PerformanceCounterCategory("Network Interface");
            var instances = category.GetInstanceNames();

            foreach (var name in instances)
            {
                if (name.Contains(ni.Id.Substring(0, 12))) // Partial match
                {
                    return name;
                }
            }
            // fallback to first instance if no match
            return instances.FirstOrDefault() ?? "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblRealTimeClock.Text = DateTime.Now.ToString("HH:mm:ss");

            UpdateSystemUsage();

            if (sentCounter == null || receivedCounter == null)
                return;

            float bytesSentPerSec = sentCounter.NextValue();
            float bytesReceivedPerSec = receivedCounter.NextValue();

            toolStripStatusLabelUpload.Text = $"Upload: {FormatBytes(bytesSentPerSec)}/s";
            toolStripStatusLabelDownload.Text = $"Download: {FormatBytes(bytesReceivedPerSec)}/s";

            UpdateNetworkTrafficChart(bytesSentPerSec, bytesReceivedPerSec);

            // Update connection count
            int totalConnections = dgvConnections.Rows.Count;
            toolStripStatusLabelConnections.Text = $"Connections: {totalConnections}";

            // Update system CPU and RAM on status bar
            float cpuUsage = cpuCounter.NextValue();
            float ramUsage = ramCounter.NextValue();
            toolStripStatusLabelCPU.Text = $"CPU: {cpuUsage:0.0}%";
            toolStripStatusLabelRAM.Text = $"RAM: {ramUsage:0.0}%";

            UpdateSystemUsageChart(cpuUsage, ramUsage);
        }

        private void UpdateSystemUsage()
        {
            // Optional: Additional system metrics
        }

        private void UpdateNetworkTrafficChart(float upload, float download)
        {
            var seriesUpload = chartNetworkTraffic.Series["Upload"];
            var seriesDownload = chartNetworkTraffic.Series["Download"];

            var now = DateTime.Now;
            double xValue = (now - lastUpdateTime).TotalSeconds;

            if (seriesUpload.Points.Count > 60)
            {
                seriesUpload.Points.RemoveAt(0);
                seriesDownload.Points.RemoveAt(0);
            }

            seriesUpload.Points.AddY(upload);
            seriesDownload.Points.AddY(download);

            lastUpdateTime = now;
        }

        private void UpdateSystemUsageChart(float cpu, float ram)
        {
            var seriesCPU = chartSystemUsage.Series["CPU"];
            var seriesRAM = chartSystemUsage.Series["RAM"];

            if (seriesCPU.Points.Count > 60)
            {
                seriesCPU.Points.RemoveAt(0);
                seriesRAM.Points.RemoveAt(0);
            }

            seriesCPU.Points.AddY(cpu);
            seriesRAM.Points.AddY(ram);
        }

        private string FormatBytes(float bytes)
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

        // Add connection fetching and DataGridView population here with detailed info, e.g.:

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
                    catch { }

                    dgvConnections.Rows.Add("TCP", conn.LocalEndPoint.ToString(), conn.RemoteEndPoint.ToString(), conn.State.ToString(), procName);
                }
            }
            catch
            {
                // Handle exceptions silently or log
            }
        }

        private int GetPidFromTcpConn(TcpConnectionInformation conn)
        {
            // Windows does not expose PID easily in managed code,
            // You can use external libraries or PInvoke (too complex here).
            return 0;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            timer1.Start();
            LoadActiveConnections();
        }
    }
}
