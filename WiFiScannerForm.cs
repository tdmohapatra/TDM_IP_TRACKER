using System;
using System.Collections.Generic;
using System.Linq;
using System.Management; // For querying Wi-Fi
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class WiFiScannerForm : Form
    {
        private bool isScanning = false;
        private System.Threading.CancellationTokenSource cancellationTokenSource;

        public WiFiScannerForm()
        {
            InitializeComponent();
            InitializeComboBox();
            SetDefaultValues();
        }

        private void InitializeComboBox()
        {
            // Populate encryption types
            cmbEncryptionType.Items.Add("All");
            cmbEncryptionType.Items.Add("WEP");
            cmbEncryptionType.Items.Add("WPA");
            cmbEncryptionType.Items.Add("WPA2");
            cmbEncryptionType.SelectedIndex = 0; // Default to "All"
        }

        private void SetDefaultValues()
        {
            // Set default values
            txtMinSignalStrength.Text = "-80";  // Default minimum signal strength
            cmbEncryptionType.SelectedIndex = 0; // Default encryption type is "All"
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (isScanning)
            {
                // Stop scanning if already scanning
                StopScanning();
                return;
            }

            string minSignal = txtMinSignalStrength.Text.Trim();
            string selectedEncryption = cmbEncryptionType.SelectedItem.ToString();

            // Validate input
            if (!int.TryParse(minSignal, out int minSignalStrength))
            {
                MessageBox.Show("Please enter a valid minimum signal strength.");
                return;
            }

            // Start scanning Wi-Fi networks
            lblStatus.Text = "Scanning Wi-Fi networks...";
            progressBarScan.Value = 0;
            dgvWiFiResults.Rows.Clear();
            progressBarScan.Maximum = 100;

            // Create a new cancellation token
            cancellationTokenSource = new System.Threading.CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // Start Wi-Fi scan in background
            isScanning = true;
            btnScan.Text = "Stop Scanning"; // Update button text
            System.Threading.Tasks.Task.Run(() =>
            {
                var networks = ScanForWiFiNetworks(minSignalStrength, selectedEncryption, token);
                UpdateUI(networks);
            }, token);
        }

        private void StopScanning()
        {
            // Cancel the scan if running
            cancellationTokenSource?.Cancel();
            isScanning = false;
            btnScan.Text = "Start Scanning";
            lblStatus.Text = "Scan Stopped.";
        }

        private List<WiFiNetwork> ScanForWiFiNetworks(int minSignalStrength, string encryption, System.Threading.CancellationToken token)
        {
            List<WiFiNetwork> networks = new List<WiFiNetwork>();

            // Using WMI to query Wi-Fi networks
            var query = new ManagementObjectSearcher("SELECT * FROM MSNdis_80211_BSSIList");

            foreach (var network in query.Get())
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                string ssid = network["Ndis80211Ssid"]?.ToString();
                int signalStrength = Convert.ToInt32(network["Ndis80211Rssi"]);
                string encryptionType = network["Ndis80211AuthenticationMode"]?.ToString();
                string channel = network["Ndis80211Channel"]?.ToString();
                string macAddress = network["Ndis80211MacAddress"]?.ToString();
                string frequency = network["Ndis80211Frequency"]?.ToString();

                // Apply filters based on user input
                if (signalStrength >= minSignalStrength && (encryption == "All" || encryptionType.Contains(encryption)))
                {
                    networks.Add(new WiFiNetwork
                    {
                        SSID = ssid,
                        SignalStrength = signalStrength,
                        Encryption = encryptionType,
                        Channel = channel,
                        MACAddress = macAddress,
                        Frequency = frequency
                    });
                }
            }

            return networks;
        }

        private void UpdateUI(List<WiFiNetwork> networks)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<WiFiNetwork>>(UpdateUI), networks);
                return;
            }

            foreach (var network in networks)
            {
                int rowIndex = dgvWiFiResults.Rows.Add();
                var row = dgvWiFiResults.Rows[rowIndex];

                row.Cells[0].Value = network.SSID;
                row.Cells[1].Value = network.SignalStrength;
                row.Cells[2].Value = network.Encryption;
                row.Cells[3].Value = network.Channel;
                row.Cells[4].Value = network.MACAddress;
                row.Cells[5].Value = network.Frequency;
            }

            lblStatus.Text = "Scan Complete!";
            progressBarScan.Value = 100;
        }
    }

    public class WiFiNetwork
    {
        public string SSID { get; set; }
        public int SignalStrength { get; set; }
        public string Encryption { get; set; }
        public string Channel { get; set; }
        public string MACAddress { get; set; }
        public string Frequency { get; set; }
    }
}
