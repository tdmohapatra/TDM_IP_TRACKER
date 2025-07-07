using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class NetworkCommandReference : Form
    {
        private System.Windows.Forms.Button btnStop;
        private Process runningProcess; // to hold current process
        public NetworkCommandReference()
        {
            InitializeComponent();
        }

        private void NetworkCommandReference_Load(object sender, EventArgs e)
        {
            
            
            
            
            
            
            cmbCommands.SelectedIndex = 0;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string cmd = cmbCommands.SelectedItem.ToString();
            ShowCommandInfo(cmd);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string cmd = cmbCommands.SelectedItem.ToString();
            ExecuteCommand(cmd);
        }

        private void ShowCommandInfo(string command)
        {
            rtbOutput.Clear();

            void Append(string text, Color color, bool bold = false)
            {
                rtbOutput.SelectionStart = rtbOutput.TextLength;
                rtbOutput.SelectionLength = 0;
                rtbOutput.SelectionColor = color;
                rtbOutput.SelectionFont = new Font("Consolas", 10, bold ? FontStyle.Bold : FontStyle.Regular);
                rtbOutput.AppendText(text + Environment.NewLine);
                rtbOutput.SelectionColor = rtbOutput.ForeColor;
            }

            Append($"Command: {command}", Color.DarkCyan, true);

            switch (command)
            {
                case "ipconfig":
                    Append("Displays current network configuration including IP, subnet, and gateway.", Color.Black);
                    Append("Usage: ipconfig /all", Color.DarkGreen);
                    Append("Tip: Use to diagnose DHCP or IP conflict issues.", Color.Gray);
                    break;
                case "ping":
                    Append("Sends ICMP Echo Requests to test host availability and latency.", Color.Black);
                    Append("Usage: ping 8.8.8.8", Color.DarkGreen);
                    Append("Tip: Good for testing basic connectivity.", Color.Gray);
                    break;
                case "tracert":
                    Append("Displays the path packets take to a network host.", Color.Black);
                    Append("Usage: tracert google.com", Color.DarkGreen);
                    Append("Tip: Useful for spotting delays in routing paths.", Color.Gray);
                    break;
                case "netstat":
                    Append("Shows all active connections, ports, and protocols.", Color.Black);
                    Append("Usage: netstat -an", Color.DarkGreen);
                    Append("Tip: Use to detect open ports or unwanted network activity.", Color.Gray);
                    break;
                case "netstat -b":
                    Append("Shows programs associated with each network connection.", Color.Black);
                    Append("Usage: netstat -b", Color.DarkGreen);
                    Append("Tip: Run as admin to view executable names.", Color.Gray);
                    break;
                case "nslookup":
                    Append("Queries DNS servers for domain name/IP information.", Color.Black);
                    Append("Usage: nslookup openai.com", Color.DarkGreen);
                    Append("Tip: Great for resolving DNS issues or verifying MX records.", Color.Gray);
                    break;
                case "arp":
                    Append("Displays or modifies the ARP cache.", Color.Black);
                    Append("Usage: arp -a", Color.DarkGreen);
                    Append("Tip: Shows MAC-to-IP mappings; useful in LAN troubleshooting.", Color.Gray);
                    break;
                case "route":
                    Append("Displays or modifies the routing table.", Color.Black);
                    Append("Usage: route print", Color.DarkGreen);
                    Append("Tip: Helps understand how traffic is routed across interfaces.", Color.Gray);
                    break;
                case "hostname":
                    Append("Displays the computer's hostname.", Color.Black);
                    Append("Usage: hostname", Color.DarkGreen);
                    Append("Tip: Simple way to verify system identity in network scripts.", Color.Gray);
                    break;
                case "whoami":
                    Append("Shows the current user context (domain\\user).", Color.Black);
                    Append("Usage: whoami", Color.DarkGreen);
                    Append("Tip: Useful in Active Directory or permission debugging.", Color.Gray);
                    break;
                case "getmac":
                    Append("Displays the MAC addresses of local network interfaces.", Color.Black);
                    Append("Usage: getmac", Color.DarkGreen);
                    Append("Tip: Good for identifying physical addresses or for MAC filtering.", Color.Gray);
                    break;
                case "netsh":
                    Append("A powerful utility to configure network interfaces, firewalls, etc.", Color.Black);
                    Append("Usage: netsh interface ip show config", Color.DarkGreen);
                    Append("Tip: Use carefully – many advanced network settings are controlled here.", Color.Gray);
                    break;
                case "ipconfig /flushdns":
                    Append("Flushes the DNS Resolver Cache.", Color.Black);
                    Append("Usage: ipconfig /flushdns", Color.DarkGreen);
                    Append("Tip: Helps resolve DNS-related browsing issues.", Color.Gray);
                    break;
                case "net use":
                    Append("Displays or manages network shares (drives, printers).", Color.Black);
                    Append("Usage: net use", Color.DarkGreen);
                    Append("Tip: Use to list connected network drives.", Color.Gray);
                    break;
                case "tasklist":
                    Append("Lists all running processes.", Color.Black);
                    Append("Usage: tasklist", Color.DarkGreen);
                    Append("Tip: Helps find networked apps or suspicious activity.", Color.Gray);
                    break;
                case "systeminfo":
                    Append("Displays detailed system information.", Color.Black);
                    Append("Usage: systeminfo", Color.DarkGreen);
                    Append("Tip: Includes network card data, system boot time, and more.", Color.Gray);
                    break;
                default:
                    Append("No data available for the selected command.", Color.Red, true);
                    break;
            }
        }

        private async void ExecuteCommand(string command)
        {
            rtbOutput.Clear();
            btnExecute.Enabled = false;
            btnStop.Enabled = true;

            string fullCommand = command;
            string args = "";

            if (command.Contains(" "))
            {
                var parts = command.Split(new[] { ' ' }, 2);
                fullCommand = parts[0];
                args = parts[1];
            }
            else
            {
                args = command switch
                {
                    "ipconfig" => "/all",
                    "ping" => "8.8.8.8",
                    "tracert" => "google.com",
                    "netstat" => "-an",
                    "netstat -b" => "",
                    "nslookup" => "openai.com",
                    "arp" => "-a",
                    "route" => "print",
                    "netsh" => "interface ip show config",
                    _ => ""
                };
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = fullCommand,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                runningProcess = new Process();
                runningProcess.StartInfo = psi;

                runningProcess.Start();

                // Read output asynchronously
                string output = await runningProcess.StandardOutput.ReadToEndAsync();
                string error = await runningProcess.StandardError.ReadToEndAsync();

                await runningProcess.WaitForExitAsync();

                string result = string.IsNullOrWhiteSpace(output) ? error : output;
                HighlightOutput(result);
            }
            catch (Exception ex)
            {
                rtbOutput.SelectionColor = Color.Red;
                rtbOutput.AppendText("Error: " + ex.Message);
            }
            finally
            {
                runningProcess?.Dispose();
                runningProcess = null;
                btnExecute.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (runningProcess != null && !runningProcess.HasExited)
            {
                try
                {
                    runningProcess.Kill(true);
                    rtbOutput.SelectionColor = Color.Red;
                    rtbOutput.AppendText(Environment.NewLine + "*** Process stopped by user ***" + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    rtbOutput.SelectionColor = Color.Red;
                    rtbOutput.AppendText("Error stopping process: " + ex.Message);
                }
                finally
                {
                    btnExecute.Enabled = true;
                    btnStop.Enabled = false;
                    runningProcess = null;
                }
            }
        }

        private void HighlightOutput(string text)
        {
            rtbOutput.Clear();

            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                Color color = Color.Black;

                if (line.Contains("error") || line.Contains("failed") || line.Contains("denied"))
                    color = Color.Red;
                else if (Regex.IsMatch(line, @"\d+\.\d+\.\d+\.\d+")) // IPv4
                    color = Color.DarkGreen;
                else if (line.Contains(":") && line.Contains(" "))
                    color = Color.Blue; // Looks like a title or header

                AppendColoredLine(line, color);
            }
        }

        private void AppendColoredLine(string line, Color color)
        {
            rtbOutput.SelectionStart = rtbOutput.TextLength;
            rtbOutput.SelectionLength = 0;
            rtbOutput.SelectionColor = color;
            rtbOutput.SelectionFont = new Font("Consolas", 10, FontStyle.Regular);
            rtbOutput.AppendText(line + Environment.NewLine);
            rtbOutput.SelectionColor = rtbOutput.ForeColor;
        }
    }
}
