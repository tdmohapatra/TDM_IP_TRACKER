using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class PortScannerForm : Form
    {
        private CancellationTokenSource cts;
        private int portsScanned = 0;
        private int totalPorts = 0;
        private bool scanning = false;
        private readonly ConcurrentBag<PortResult> openPorts = new ConcurrentBag<PortResult>();

        private readonly Dictionary<int, string> commonPorts = new Dictionary<int, string>()
        {
            { 20, "FTP Data" }, { 21, "FTP Control" }, { 22, "SSH" },
            { 23, "Telnet" }, { 25, "SMTP" }, { 53, "DNS" },
            { 67, "DHCP Server" }, { 68, "DHCP Client" }, { 69, "TFTP" },
            { 80, "HTTP" }, { 110, "POP3" }, { 119, "NNTP" },
            { 123, "NTP" }, { 137, "NetBIOS" }, { 138, "NetBIOS" },
            { 139, "NetBIOS" }, { 143, "IMAP" }, { 161, "SNMP" },
            { 162, "SNMP Trap" }, { 389, "LDAP" }, { 443, "HTTPS" },
            { 445, "SMB" }, { 587, "SMTP SSL" }, { 636, "LDAP SSL" },
            { 993, "IMAP SSL" }, { 995, "POP3 SSL" }, { 1433, "SQL Server" },
            { 1521, "Oracle" }, { 1723, "PPTP" }, { 3306, "MySQL" },
            { 3389, "RDP" }, { 5900, "VNC" }, { 8080, "HTTP Alt" },
            { 8443, "HTTPS Alt" }, { 27017, "MongoDB" }, { 5000, "UPnP" }
        };

        public PortScannerForm()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!scanning)
            {
                StartScan();
            }
            else
            {
                StopScan();
            }
        }

        private async void StartScan()
        {
            if (!ValidateInputs())
                return;

            // Clear previous results
            lvResults.BeginUpdate();
            lvResults.Items.Clear();
            lvResults.EndUpdate();

            portsScanned = 0;
            openPorts.Clear();
            progressBar.Value = 0;
            cts = new CancellationTokenSource();

            // Calculate total ports to scan
            int startPort = int.Parse(txtStartPort.Text);
            int endPort = int.Parse(txtEndPort.Text);
            totalPorts = endPort - startPort + 1;

            // Update UI
            btnScan.Text = "Stop Scan";
            scanning = true;
            lblStatus.Text = "Scanning in progress...";
            lblStatus.ForeColor = Color.Blue;

            try
            {
                var parameters = new ScanParameters()
                {
                    Host = txtHost.Text,
                    StartPort = startPort,
                    EndPort = endPort,
                    Threads = (int)numThreads.Value,
                    CommonPortsOnly = chkCommonPorts.Checked,
                    Timeout = (int)numTimeout.Value,
                    ShowClosed = chkShowClosed.Checked
                };

                await Task.Run(() => PerformScan(parameters, cts.Token), cts.Token);

                if (!cts.IsCancellationRequested)
                {
                    lblStatus.Text = $"Scan completed. Found {openPorts.Count} open ports out of {portsScanned} scanned.";
                    lblStatus.ForeColor = Color.Green;
                }
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = $"Scan cancelled. Found {openPorts.Count} open ports out of {portsScanned} scanned.";
                lblStatus.ForeColor = Color.Orange;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Scan failed: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                scanning = false;
                btnScan.Text = "Start Scan";
                progressBar.Value = 100;
                cts.Dispose();
            }
        }

        private void StopScan()
        {
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }

        private bool ValidateInputs()
        {
            // Validate host
            if (string.IsNullOrWhiteSpace(txtHost.Text))
            {
                MessageBox.Show("Please enter a host name or IP address", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate port range
            if (!int.TryParse(txtStartPort.Text, out int startPort) ||
                !int.TryParse(txtEndPort.Text, out int endPort))
            {
                MessageBox.Show("Please enter valid port numbers", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (startPort < 1 || startPort > 65535 || endPort < 1 || endPort > 65535)
            {
                MessageBox.Show("Port numbers must be between 1 and 65535", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (startPort > endPort)
            {
                MessageBox.Show("Start port cannot be greater than end port", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void PerformScan(ScanParameters parameters, CancellationToken ct)
        {
            var portsToScan = GetPortsToScan(parameters);
            totalPorts = portsToScan.Count;

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = parameters.Threads,
                CancellationToken = ct
            };

            try
            {
                Parallel.ForEach(portsToScan, options, port =>
                {
                    ct.ThrowIfCancellationRequested();

                    var result = CheckPort(parameters.Host, port, parameters.Timeout);
                    Interlocked.Increment(ref portsScanned);

                    if (result.IsOpen || parameters.ShowClosed)
                    {
                        if (result.IsOpen)
                        {
                            openPorts.Add(result);
                        }

                        // Update UI on the main thread
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateResults(result);
                            UpdateProgress();
                        });
                    }
                    else
                    {
                        // Just update progress for closed ports we're not showing
                        this.Invoke((MethodInvoker)UpdateProgress);
                    }
                });
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = $"Error during scan: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                });
                throw;
            }
        }

        private List<int> GetPortsToScan(ScanParameters parameters)
        {
            if (parameters.CommonPortsOnly)
            {
                return commonPorts.Keys
                    .Where(port => port >= parameters.StartPort && port <= parameters.EndPort)
                    .OrderBy(port => port)
                    .ToList();
            }

            var ports = new List<int>();
            for (int port = parameters.StartPort; port <= parameters.EndPort; port++)
            {
                ports.Add(port);
            }
            return ports;
        }

        private PortResult CheckPort(string host, int port, int timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (success)
                    {
                        client.EndConnect(result);
                        string service = commonPorts.ContainsKey(port) ? commonPorts[port] : "Unknown";
                        return new PortResult { Port = port, IsOpen = true, Service = service };
                    }
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.ConnectionRefused)
            {
                // Expected for closed ports
            }
            catch (Exception ex)
            {
                // For other errors, consider logging them
                Debug.WriteLine($"Error scanning port {port}: {ex.Message}");
            }

            return new PortResult { Port = port, IsOpen = false, Service = "Closed" };
        }

        private void UpdateResults(PortResult result)
        {
            lvResults.BeginUpdate();

            if (result.IsOpen || chkShowClosed.Checked)
            {
                var item = new ListViewItem(result.Port.ToString());
                item.SubItems.Add(result.IsOpen ? "Open" : "Closed");
                item.SubItems.Add(result.Service);
                item.BackColor = result.IsOpen ? Color.LightGreen : Color.LightPink;
                lvResults.Items.Add(item);
            }

            // Auto-scroll only if we're near the bottom
            if (lvResults.Items.Count > 0 &&
                lvResults.Items[lvResults.Items.Count - 1].Bounds.Bottom <= lvResults.ClientRectangle.Bottom)
            {
                lvResults.EnsureVisible(lvResults.Items.Count - 1);
            }

            lvResults.EndUpdate();
        }

        private void UpdateProgress()
        {
            if (totalPorts > 0)
            {
                int progress = (int)((double)portsScanned / totalPorts * 100);
                progressBar.Value = Math.Min(progress, 100);
                lblStatus.Text = $"Scanning... {portsScanned} of {totalPorts} ports checked ({openPorts.Count} open)";
            }
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";
            saveFileDialog.Title = "Save Scan Results";
            saveFileDialog.FileName = $"PortScan_{txtHost.Text}_{DateTime.Now:yyyyMMdd_HHmmss}";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Port,Status,Service");
                        foreach (ListViewItem item in lvResults.Items)
                        {
                            writer.WriteLine($"{item.Text},{item.SubItems[1].Text},\"{item.SubItems[2].Text}\"");
                        }
                    }
                    lblStatus.Text = $"Results saved to {saveFileDialog.FileName}";
                    lblStatus.ForeColor = Color.Blue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkShowClosed_CheckedChanged(object sender, EventArgs e)
        {
            if (scanning)
            {
                MessageBox.Show("Setting will take effect on the next scan", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private struct ScanParameters
        {
            public string Host { get; set; }
            public int StartPort { get; set; }
            public int EndPort { get; set; }
            public int Threads { get; set; }
            public bool CommonPortsOnly { get; set; }
            public int Timeout { get; set; }
            public bool ShowClosed { get; set; }
        }

        private struct PortResult
        {
            public int Port { get; set; }
            public bool IsOpen { get; set; }
            public string Service { get; set; }
        }
    }
}