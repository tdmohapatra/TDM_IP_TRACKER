using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class PacketTrackerForm : Form
    {
        private CaptureDeviceList devices;
        private ICaptureDevice selectedDevice;

        private int packetCount = 0;
        private long totalBytes = 0;
        private int tcpCount = 0, udpCount = 0, icmpCount = 0;

        public PacketTrackerForm()
        {
            InitializeComponent();
            InitializeListViewColumns();
            SetupInitialState();
            LoadNetworkAdapters();
            WireUpEvents();
        }

        private void InitializeListViewColumns()
        {
            listViewPackets.Columns.Add("Time", 120);
            listViewPackets.Columns.Add("Source IP", 120);
            listViewPackets.Columns.Add("Destination IP", 120);
            listViewPackets.Columns.Add("Protocol", 80);
            listViewPackets.Columns.Add("Source Port", 80);
            listViewPackets.Columns.Add("Dest Port", 80);
            listViewPackets.Columns.Add("Length", 80);
        }

        private void SetupInitialState()
        {
            btnStop.Enabled = false;
            toolStripStatusLabel.Text = "Ready";
            lblStatus.Text = "Ready";
            txtPacketDetails.Font = new Font("Consolas", 9F);
            txtPayload.Font = new Font("Consolas", 9F);
        }

        private void WireUpEvents()
        {
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            btnFilter.Click += BtnFilter_Click;
            btnExport.Click += BtnExport_Click;
            listViewPackets.SelectedIndexChanged += ListViewPackets_SelectedIndexChanged;
        }

        private void LoadNetworkAdapters()
        {
            devices = CaptureDeviceList.Instance;
            cmbAdapters.Items.Clear();

            if (devices == null || devices.Count < 1)
            {
                MessageBox.Show("No network adapters found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = false;
                return;
            }

            foreach (var dev in devices)
            {
                if (dev?.Description != null)
                    cmbAdapters.Items.Add(dev.Description);
            }

            if (cmbAdapters.Items.Count > 0)
            {
                cmbAdapters.SelectedIndex = 0;
            }
            else
            {
                btnStart.Enabled = false;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (cmbAdapters.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a network adapter.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var warningResult = ShowResourceWarning();
            if (warningResult != DialogResult.Yes)
            {
                return;
            }

            selectedDevice = devices[cmbAdapters.SelectedIndex];

            try
            {
                if (selectedDevice == null)
                {
                    MessageBox.Show("Selected device is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CheckDiskSpace();

                selectedDevice.OnPacketArrival += OnPacketArrival;
                selectedDevice.Open(new DeviceConfiguration()
                {
                    Mode = DeviceModes.Promiscuous,
                    ReadTimeout = 1000
                });

                selectedDevice.StartCapture();

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                UpdateStatus("Capture running...", Color.Green);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening device: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Capture failed", Color.Red);
            }
        }

        private DialogResult ShowResourceWarning()
        {
            var warningMessage = new StringBuilder();
            warningMessage.AppendLine("WARNING: Packet capture requires significant system resources.");
            warningMessage.AppendLine();
            warningMessage.AppendLine("System Requirements:");
            warningMessage.AppendLine("- Minimum: 2GB RAM, 100MB free disk space");
            warningMessage.AppendLine("- Recommended: 4GB+ RAM, 1GB+ free disk space");
            warningMessage.AppendLine();
            warningMessage.AppendLine("Potential Impacts:");
            warningMessage.AppendLine("- Increased CPU and memory usage");
            warningMessage.AppendLine("- Network performance may be affected");
            warningMessage.AppendLine("- Large captures will consume disk space");
            warningMessage.AppendLine();
            warningMessage.AppendLine("Do you want to continue?");

            return MessageBox.Show(warningMessage.ToString(),
                                 "Packet Capture Warning",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning);
        }

        private void CheckDiskSpace()
        {
            var drive = new DriveInfo(Application.StartupPath);
            if (drive.AvailableFreeSpace < 100 * 1024 * 1024) // Less than 100MB free
            {
                MessageBox.Show($"Warning: Low disk space available ({drive.AvailableFreeSpace / 1024 / 1024}MB). " +
                              "Packet capture may fail if disk space runs out.",
                              "Low Resources Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedDevice != null)
                {
                    selectedDevice.StopCapture();
                    selectedDevice.Close();
                    selectedDevice.OnPacketArrival -= OnPacketArrival;
                    selectedDevice = null;
                }

                UpdateStatus("Capture stopped", Color.Blue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping capture: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Error stopping capture", Color.Red);
            }
            finally
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        private void OnPacketArrival(object sender, PacketCapture e)
        {
            var rawPacket = e.GetPacket();
            if (rawPacket == null) return;

            var linkType = rawPacket.LinkLayerType;
            var data = rawPacket.Data;
            var time = rawPacket.Timeval.Date;

            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    if (data == null || data.Length == 0)
                        return;

                    var packet = Packet.ParsePacket(linkType, data);
                    if (packet == null) return;

                    var ipPacket = packet.Extract<IPPacket>();
                    if (ipPacket == null) return;

                    var protocol = ipPacket.Protocol.ToString();
                    var srcIp = ipPacket.SourceAddress?.ToString() ?? "N/A";
                    var dstIp = ipPacket.DestinationAddress?.ToString() ?? "N/A";
                    var length = data.Length.ToString();
                    var srcPort = "";
                    var dstPort = "";

                    var tcpPacket = packet.Extract<TcpPacket>();
                    var udpPacket = packet.Extract<UdpPacket>();

                    if (tcpPacket != null)
                    {
                        srcPort = tcpPacket.SourcePort.ToString();
                        dstPort = tcpPacket.DestinationPort.ToString();
                    }
                    else if (udpPacket != null)
                    {
                        srcPort = udpPacket.SourcePort.ToString();
                        dstPort = udpPacket.DestinationPort.ToString();
                    }

                    var lvi = new ListViewItem(new[] {
                        time.ToString("HH:mm:ss.fff"),
                        srcIp,
                        dstIp,
                        protocol,
                        srcPort,
                        dstPort,
                        length
                    })
                    {
                        Tag = packet,
                        BackColor = GetProtocolColor(ipPacket.Protocol)
                    };

                    listViewPackets.Items.Add(lvi);
                    UpdatePacketStatistics(data.Length, ipPacket.Protocol);

                    if (listViewPackets.Items.Count > 0)
                        listViewPackets.EnsureVisible(listViewPackets.Items.Count - 1);
                }
                catch (Exception ex)
                {
                    // Log or ignore parse errors
                    Console.WriteLine($"Packet processing error: {ex.Message}");
                }
            });
        }

        private Color GetProtocolColor(ProtocolType protocol)
        {
            switch (protocol)
            {
                case ProtocolType.Tcp: return Color.Lavender;
                case ProtocolType.Udp: return Color.Honeydew;
                case ProtocolType.Icmp: return Color.MistyRose;
                default: return Color.White;
            }
        }

        private void UpdatePacketStatistics(int packetLength, ProtocolType protocol)
        {
            packetCount++;
            totalBytes += packetLength;

            switch (protocol)
            {
                case ProtocolType.Tcp: tcpCount++; break;
                case ProtocolType.Udp: udpCount++; break;
                case ProtocolType.Icmp: icmpCount++; break;
            }

            lblTotalPackets.Text = $"Packets: {packetCount}";
            lblBandwidth.Text = $"Bandwidth: {totalBytes / 1024} KB";
            lblProtocolStats.Text = $"TCP: {tcpCount}, UDP: {udpCount}, ICMP: {icmpCount}";

            // Update status bar
            toolStripStatusLabel1.Text = $"Packets: {packetCount}";
            toolStripStatusLabel2.Text = $"Bandwidth: {totalBytes / 1024} KB";
            toolStripStatusLabel3.Text = $"TCP: {tcpCount}, UDP: {udpCount}, ICMP: {icmpCount}";
        }

        //private void ListViewPackets_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (listViewPackets.SelectedItems.Count == 0)
        //        {
        //            txtPacketDetails.Clear();
        //            txtPayload.Clear();
        //            return;
        //        }

        //        var selectedItem = listViewPackets.SelectedItems[0];
        //        if (selectedItem.Tag is Packet packet)
        //        {
        //            txtPacketDetails.Text = GetFormattedPacketDetails(packet);
        //            txtPayload.Text = GetFormattedPacketPayload(packet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error displaying packet details: {ex.Message}");
        //    }
        //}

        private void ListViewPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPackets.SelectedItems.Count == 0)
            {
                txtPacketDetails.Clear();
                txtPayload.Clear();
                return;
            }

            var selectedItem = listViewPackets.SelectedItems[0];
            if (selectedItem.Tag is Packet packet)
            {
                txtPacketDetails.Text = GetFormattedPacketDetails(packet);
                DisplayPayload(packet);
            }
        }

        private void DisplayPayload(Packet packet)
        {
            var bytes = packet.Bytes ?? Array.Empty<byte>();
            var sb = new StringBuilder();

            // Hex view header
            sb.AppendLine("HEX VIEW:");
            sb.AppendLine("Offset  00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F   ASCII");
            sb.AppendLine("------  -----------------------------------------------  ----------------");

            // Process 16 bytes per line
            for (int i = 0; i < bytes.Length; i += 16)
            {
                var lineBytes = bytes.Skip(i).Take(16).ToArray();

                // Offset
                sb.Append($"{i:X6}  ");

                // Hex bytes
                for (int j = 0; j < 16; j++)
                {
                    if (j < lineBytes.Length)
                        sb.Append($"{lineBytes[j]:X2} ");
                    else
                        sb.Append("   ");

                    if (j == 7) sb.Append(" "); // Extra space after 8 bytes
                }

                sb.Append(" ");

                // ASCII representation
                for (int j = 0; j < lineBytes.Length; j++)
                {
                    char c = (char)lineBytes[j];
                    sb.Append(char.IsControl(c) || char.IsWhiteSpace(c) ? '.' : c);
                }

                sb.AppendLine();
            }

            txtPayload.Text = sb.ToString();
            txtPayload.Font = new Font("Consolas", 9); // Monospaced font for alignment
        }

        private string GetFormattedPacketDetails(Packet packet)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Packet Overview ===");
            sb.AppendLine($"Captured At:   {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"Total Length:  {packet.Bytes?.Length ?? 0} bytes");
            sb.AppendLine();

            var eth = packet.Extract<EthernetPacket>();
            if (eth != null)
            {
                sb.AppendLine("=== Ethernet Layer ===");
                sb.AppendLine($"  Source MAC:      {eth.SourceHardwareAddress}");
                sb.AppendLine($"  Destination MAC: {eth.DestinationHardwareAddress}");
                sb.AppendLine($"  EtherType:       {eth.Type}");
                sb.AppendLine();
            }

            var ip = packet.Extract<IPPacket>();
            if (ip != null)
            {
                sb.AppendLine("=== IP Layer ===");
                sb.AppendLine($"  Version:          {ip.Version}");
                sb.AppendLine($"  Header Length:    {ip.HeaderLength} bytes");
                sb.AppendLine($"  Total Length:     {ip.TotalLength}");
                sb.AppendLine($"  TTL:              {ip.TimeToLive}");
                sb.AppendLine($"  Protocol:         {ip.Protocol}");
                sb.AppendLine($"  Source IP:        {ip.SourceAddress}");
                sb.AppendLine($"  Destination IP:   {ip.DestinationAddress}");
                sb.AppendLine();
            }

            var tcp = packet.Extract<TcpPacket>();
            if (tcp != null)
            {
                sb.AppendLine("=== TCP Layer ===");
                sb.AppendLine($"  Src Port:         {tcp.SourcePort}");
                sb.AppendLine($"  Dst Port:         {tcp.DestinationPort}");
                sb.AppendLine($"  Seq #:            {tcp.SequenceNumber}");
                sb.AppendLine($"  Ack #:            {tcp.AcknowledgmentNumber}");
                sb.AppendLine($"  Header Size:      {tcp.DataOffset * 4} bytes");
                sb.AppendLine($"  Flags:            {GetTcpFlags(tcp)}");
                sb.AppendLine($"  Window Size:      {tcp.WindowSize}");
                sb.AppendLine($"  Checksum:         0x{tcp.Checksum:X4}");
                sb.AppendLine($"  Urgent Pointer:   {tcp.UrgentPointer}");
                sb.AppendLine();
            }

            var udp = packet.Extract<UdpPacket>();
            if (udp != null)
            {
                sb.AppendLine("=== UDP Layer ===");
                sb.AppendLine($"  Src Port:         {udp.SourcePort}");
                sb.AppendLine($"  Dst Port:         {udp.DestinationPort}");
                sb.AppendLine($"  Length:           {udp.Length}");
                sb.AppendLine($"  Checksum:         0x{udp.Checksum:X4}");
                sb.AppendLine();
            }

            var icmp = packet.Extract<IcmpV4Packet>();
            if (icmp != null)
            {
                sb.AppendLine("=== ICMP Layer ===");
                sb.AppendLine($"  Type:             {icmp.TypeCode}");
                sb.AppendLine($"  Checksum:         0x{icmp.Checksum:X4}");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string GetTcpFlags(TcpPacket tcp)
        {
            var flags = (TcpFlags)tcp.Flags;
            var sb = new StringBuilder();

            if (flags.HasFlag(TcpFlags.Syn)) sb.Append("SYN ");
            if (flags.HasFlag(TcpFlags.Ack)) sb.Append("ACK ");
            if (flags.HasFlag(TcpFlags.Fin)) sb.Append("FIN ");
            if (flags.HasFlag(TcpFlags.Rst)) sb.Append("RST ");
            if (flags.HasFlag(TcpFlags.Psh)) sb.Append("PSH ");
            if (flags.HasFlag(TcpFlags.Urg)) sb.Append("URG ");
            if (flags.HasFlag(TcpFlags.Ece)) sb.Append("ECE ");
            if (flags.HasFlag(TcpFlags.Cwr)) sb.Append("CWR ");

            return sb.Length > 0 ? sb.ToString().Trim() : "None";
        }

        private string GetFormattedPacketPayload(Packet packet)
        {
            var bytes = packet.Bytes ?? Array.Empty<byte>();
            int maxBytes = Math.Min(64, bytes.Length);

            var sbHex = new StringBuilder();
            var sbAscii = new StringBuilder();

            for (int i = 0; i < maxBytes; i++)
            {
                byte b = bytes[i];
                sbHex.AppendFormat("{0:X2} ", b);
                sbAscii.Append((b >= 32 && b <= 126) ? (char)b : '.');

                if ((i + 1) % 8 == 0)
                {
                    sbHex.Append("  ");
                    sbAscii.Append("  ");
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Payload (first {maxBytes} bytes):");
            sb.AppendLine(sbHex.ToString());
            sb.AppendLine(sbAscii.ToString());

            return sb.ToString();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            var filterText = txtFilter.Text.Trim();
            if (string.IsNullOrEmpty(filterText))
            {
                foreach (ListViewItem item in listViewPackets.Items)
                {
                    item.BackColor = Color.White;
                }
                return;
            }

            foreach (ListViewItem item in listViewPackets.Items)
            {
                bool match = item.SubItems.Cast<ListViewItem.ListViewSubItem>()
                    .Any(sub => sub.Text.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0);

                item.BackColor = match ? Color.LightYellow : Color.White;
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (listViewPackets.Items.Count == 0)
            {
                MessageBox.Show("No packets to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv|PCAP files (*.pcap)|*.pcap";
                sfd.Title = "Export Packet Data";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (Path.GetExtension(sfd.FileName).ToLower() == ".csv")
                        {
                            ExportToCsv(sfd.FileName);
                        }
                        else
                        {
                            ExportToPcap(sfd.FileName);
                        }
                        MessageBox.Show("Export successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine("Time,Source IP,Destination IP,Protocol,Source Port,Destination Port,Size");

                foreach (ListViewItem item in listViewPackets.Items)
                {
                    var values = item.SubItems.Cast<ListViewItem.ListViewSubItem>()
                        .Select(sub => sub.Text.Replace(",", ";"));
                    sw.WriteLine(string.Join(",", values));
                }
            }
        }

        private void ExportToPcap(string filePath)
        {
            // This would require additional implementation using SharpPcap's save capabilities
            MessageBox.Show("PCAP export not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStatus(string message, Color? color = null)
        {
            lblStatus.Text = message;
            toolStripStatusLabel.Text = message;

            if (color.HasValue)
            {
                lblStatus.ForeColor = color.Value;
            }
        }
    }

    [Flags]
    public enum TcpFlags : byte
    {
        Fin = 0x01,
        Syn = 0x02,
        Rst = 0x04,
        Psh = 0x08,
        Ack = 0x10,
        Urg = 0x20,
        Ece = 0x40,
        Cwr = 0x80
    }
}
