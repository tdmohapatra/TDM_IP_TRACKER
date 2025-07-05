using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class SingleIPForm : Form
    {
        private List<PingResult> pingHistory = new List<PingResult>();
        private bool isAutoPinging = false;
        private int successfulPings = 0;
        private int totalPings = 0;

        public SingleIPForm()
        {
            InitializeComponent();
            InitializeDataGrid();
            cmbPingCount.SelectedIndex = 0; // Default to 1 ping
        }

        private void InitializeDataGrid()
        {
            dataGridPingResults.Columns.Clear();
            dataGridPingResults.Columns.Add("Timestamp", "Timestamp");
            dataGridPingResults.Columns.Add("Status", "Status");
            dataGridPingResults.Columns.Add("ResponseTime", "Response (ms)");
            dataGridPingResults.Columns.Add("TTL", "TTL");
            dataGridPingResults.Columns.Add("BufferSize", "Buffer Size");

            dataGridPingResults.Columns["Timestamp"].Width = 120;
            dataGridPingResults.Columns["Status"].Width = 100;
            dataGridPingResults.Columns["ResponseTime"].Width = 80;
            dataGridPingResults.Columns["TTL"].Width = 60;
            dataGridPingResults.Columns["BufferSize"].Width = 80;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtIP.Text))
            //{
            //    MessageBox.Show("Please enter an IP address or hostname", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //PingHost(txtIP.Text, (int)cmbPingCount.SelectedItem);
            if (int.TryParse(cmbPingCount.SelectedItem?.ToString(), out int pingCount))
            {
                PingHost(txtIP.Text, pingCount);
            }
            else
            {
                MessageBox.Show("Invalid ping count selected.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void PingHost(string host, int count)
        {
            btnCheck.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                using (Ping ping = new Ping())
                {
                    PingOptions options = new PingOptions(64, true);
                    byte[] buffer = Encoding.ASCII.GetBytes(new string('a', 32));

                    for (int i = 0; i < count; i++)
                    {
                        if (!isAutoPinging && i > 0) break; // For manual ping, just do one

                        try
                        {
                            PingReply reply = await ping.SendPingAsync(host, 1000, buffer, options);
                            ProcessPingReply(reply);
                        }
                        catch (PingException pe)
                        {
                            AddPingResult(new PingResult
                            {
                                Timestamp = DateTime.Now,
                                Status = "Error",
                                ResponseTime = 0,
                                TTL = 0,
                                BufferSize = 0,
                                ErrorMessage = pe.InnerException?.Message ?? pe.Message
                            });
                        }

                        if (i < count - 1) await System.Threading.Tasks.Task.Delay(500);
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.BackColor = Color.LightCoral;
            }
            finally
            {
                btnCheck.Enabled = true;
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 0;
            }
        }

        private void ProcessPingReply(PingReply reply)
        {
            totalPings++;

            var result = new PingResult
            {
                Timestamp = DateTime.Now,
                ResponseTime = (int)reply.RoundtripTime,
                TTL = reply.Options?.Ttl ?? 0,
                BufferSize = reply.Buffer?.Length ?? 0
            };

            if (reply.Status == IPStatus.Success)
            {
                successfulPings++;
                result.Status = "Success";
                lblStatus.Text = $"Reply from {reply.Address}: time={reply.RoundtripTime}ms TTL={reply.Options?.Ttl}";
                lblStatus.BackColor = Color.LightGreen;
            }
            else
            {
                result.Status = reply.Status.ToString();
                result.ErrorMessage = reply.Status.ToString();
                lblStatus.Text = $"Ping failed: {reply.Status}";
                lblStatus.BackColor = Color.LightCoral;
            }

            AddPingResult(result);
            UpdateStatistics();
        }

        private void AddPingResult(PingResult result)
        {
            pingHistory.Add(result);

            // Add to DataGridView
            dataGridPingResults.Rows.Insert(0,
                result.Timestamp.ToString("HH:mm:ss.fff"),
                result.Status,
                result.Status == "Success" ? result.ResponseTime.ToString() : "-",
                result.TTL > 0 ? result.TTL.ToString() : "-",
                result.BufferSize > 0 ? result.BufferSize.ToString() : "-");

            // Keep only the last 100 results
            if (dataGridPingResults.Rows.Count > 100)
            {
                dataGridPingResults.Rows.RemoveAt(dataGridPingResults.Rows.Count - 1);
            }
        }

        private void UpdateStatistics()
        {
            double packetLoss = totalPings > 0 ?
                (100.0 - ((double)successfulPings / totalPings * 100.0)) : 0;

            lblResponseTime.Text = $"Response: {pingHistory.LastOrDefault()?.ResponseTime ?? 0} ms";
            lblPacketLoss.Text = $"Loss: {packetLoss:F2}%";
            lblLastSeen.Text = $"Last seen: {pingHistory.LastOrDefault()?.Timestamp.ToString("HH:mm:ss") ?? "Never"}";

            // Calculate average response time from successful pings
            var successfulResults = pingHistory.Where(p => p.Status == "Success").ToList();
            if (successfulResults.Any())
            {
                double avgResponse = successfulResults.Average(p => p.ResponseTime);
                lblResponseTime.Text += $" (Avg: {avgResponse:F1} ms)";
            }
        }

        private void chkAutoPing_CheckedChanged(object sender, EventArgs e)
        {
            isAutoPinging = chkAutoPing.Checked;
            numPingInterval.Enabled = !isAutoPinging;

            if (isAutoPinging)
            {
                if (string.IsNullOrWhiteSpace(txtIP.Text))
                {
                    chkAutoPing.Checked = false;
                    MessageBox.Show("Please enter an IP address first", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                autoPingTimer.Interval = (int)numPingInterval.Value * 1000;
                autoPingTimer.Start();
                btnCheck.Text = "Stop";
            }
            else
            {
                autoPingTimer.Stop();
                btnCheck.Text = "Ping Now";
            }
        }

        private void autoPingTimer_Tick(object sender, EventArgs e)
        {
            if (!isAutoPinging) return;
            PingHost(txtIP.Text, 1);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (pingHistory.Count == 0)
            {
                MessageBox.Show("No ping history available", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var lastResult = pingHistory.Last();
            var details = new StringBuilder();
            details.AppendLine($"Host: {txtIP.Text}");
            details.AppendLine($"Last Status: {lastResult.Status}");

            if (lastResult.Status == "Success")
            {
                details.AppendLine($"Response Time: {lastResult.ResponseTime} ms");
                details.AppendLine($"TTL: {lastResult.TTL}");
                details.AppendLine($"Buffer Size: {lastResult.BufferSize} bytes");
            }
            else
            {
                details.AppendLine($"Error: {lastResult.ErrorMessage}");
            }

            details.AppendLine();
            details.AppendLine($"Successful Pings: {successfulPings} of {totalPings}");
            details.AppendLine($"Packet Loss: {lblPacketLoss.Text}");

            MessageBox.Show(details.ToString(), "Ping Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (pingHistory.Count == 0)
            {
                MessageBox.Show("No data to export", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                sfd.FileName = $"PingResults_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            // Write header
                            sw.WriteLine("Timestamp,Status,ResponseTime(ms),TTL,BufferSize,Error");

                            // Write data
                            foreach (var result in pingHistory)
                            {
                                sw.WriteLine($"{result.Timestamp:yyyy-MM-dd HH:mm:ss.fff}," +
                                    $"{result.Status}," +
                                    $"{(result.Status == "Success" ? result.ResponseTime.ToString() : "-")}," +
                                    $"{(result.TTL > 0 ? result.TTL.ToString() : "-")}," +
                                    $"{(result.BufferSize > 0 ? result.BufferSize.ToString() : "-")}," +
                                    $"{result.ErrorMessage ?? ""}");
                            }
                        }

                        MessageBox.Show($"Data exported successfully to {sfd.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {
            // Basic validation - change border color if not empty and not valid IP/hostname
            if (!string.IsNullOrWhiteSpace(txtIP.Text))
            {
                bool isValid = ValidateIPOrHostname(txtIP.Text);
                txtIP.BackColor = isValid ? Color.White : Color.LightPink;
            }
            else
            {
                txtIP.BackColor = Color.White;
            }
        }

        private bool ValidateIPOrHostname(string input)
        {
            // Check if it's a valid IP address
            if (IPAddress.TryParse(input, out _))
                return true;

            // Basic hostname validation
            if (Uri.CheckHostName(input) != UriHostNameType.Unknown)
                return true;

            return false;
        }

        private void SingleIPForm_Load(object sender, EventArgs e)
        {
            // Load default IP (could be last used IP from settings)
            txtIP.Text = "8.8.8.8"; // Default to Google DNS
        }

        private void cmbPingCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the selected ping count
            if (cmbPingCount.SelectedItem != null)
            {
                // Value is already set in the combo box items
            }
        }
    }

    public class PingResult
    {
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public int ResponseTime { get; set; }
        public int TTL { get; set; }
        public int BufferSize { get; set; }
        public string ErrorMessage { get; set; }
    }
}