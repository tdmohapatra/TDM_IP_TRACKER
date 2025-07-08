using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class PingTestForm : Form
    {
        private CancellationTokenSource _cts;
        private static readonly HttpClient _httpClient = new HttpClient();
        private double? lastLatitude;
        private double? lastLongitude;

        public PingTestForm()
        {
            InitializeComponent();
            btnStopAction.Enabled = false;
            btnStartPing.Enabled = false;
            btnTraceRoute.Enabled = false;
            btnDnsLookup.Enabled = false;
            btnPortScan.Enabled = false;

            txtTarget.TextChanged += txtTarget_TextChanged;
            btnStopAction.Click += BtnStopAction_Click;
            btnStartPing.Click += BtnStartPing_Click;
            btnTraceRoute.Click += BtnTraceRoute_Click;
            btnDnsLookup.Click += BtnDnsLookup_Click;
            btnPortScan.Click += BtnPortScan_Click;
        }

        private bool IsValidIpOrHostname(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return IPAddress.TryParse(input, out _) || Uri.CheckHostName(input) != UriHostNameType.Unknown;
        }

        private async void txtTarget_TextChanged(object sender, EventArgs e)
        {
            string input = txtTarget.Text.Trim();
            bool valid = IsValidIpOrHostname(input);

            ToggleActionButtons(valid && !btnStopAction.Enabled);
            txtGeoLocation.Clear();

            if (!valid)
            {
                txtGeoLocation.Text = "Invalid IP or hostname.";
                return;
            }

            if (IPAddress.TryParse(input, out var ip))
            {
                //await GetGeoLocation(ip.ToString());

                await GetGeoLocation(ip.ToString(), _cts?.Token ?? CancellationToken.None);


            }
            else
            {
                try
                {
                    var hostEntry = await Dns.GetHostEntryAsync(input);
                    var firstIp = hostEntry.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
                    if (firstIp != null)
                        //await GetGeoLocation(firstIp.ToString());
                        await GetGeoLocation(ip.ToString(), _cts?.Token ?? CancellationToken.None);


                }
                catch
                {
                    txtGeoLocation.Text = "Unable to resolve hostname.";
                }
            }
        }

        private void BtnStopAction_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private async void BtnStartPing_Click(object sender, EventArgs e)
        {
            string target = txtTarget.Text.Trim();
            if (string.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please enter a valid IP or hostname.");
                return;
            }

            DisableButtonsForOperation();
            _cts = new CancellationTokenSource();
            lvPingResults.Items.Clear();
            toolStripStatusLabel.Text = $"Pinging {target}...";
            progressBar.Value = 0;

            try
            {
                await PingTargetAsync(target, 10, _cts.Token);
                toolStripStatusLabel.Text = "Ping test complete.";
            }
            catch (OperationCanceledException)
            {
                toolStripStatusLabel.Text = "Ping test cancelled.";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = $"Ping test error: {ex.Message}";
            }
            finally
            {
                progressBar.Value = 0;
                EnableButtonsAfterOperation();
            }
        }
        private async Task PingTargetAsync(string target, int count, CancellationToken token)
        {
            using Ping ping = new();
            var items = new List<ListViewItem>(count);

            for (int i = 0; i < count; i++)
            {
                token.ThrowIfCancellationRequested();
                var item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));

                try
                {
                    //using var reply = await ping.SendPingAsync(target, 1000).ConfigureAwait(false);
                    var reply = await ping.SendPingAsync(target, 1000).ConfigureAwait(false);

                    if (reply.Status == IPStatus.Success)
                        item.SubItems.AddRange(new[] { "Success", reply.RoundtripTime + " ms", reply.Options?.Ttl.ToString() ?? "N/A" });
                    else
                        item.SubItems.AddRange(new[] { reply.Status.ToString(), "-", "-" });
                }
                catch
                {
                    item.SubItems.AddRange(new[] { "Error", "-", "-" });
                }

                items.Add(item);
                UpdateProgressBar((i + 1) * 100 / count);
                await Task.Delay(500, token).ConfigureAwait(false);
            }

            InvokeIfRequired(() =>
            {
                lvPingResults.BeginUpdate();
                lvPingResults.Items.AddRange(items.ToArray());
                lvPingResults.EndUpdate();
            });
        }


        //private async Task PingTargetAsync(string target, int count, CancellationToken token)
        //{
        //    using Ping pingSender = new Ping();

        //    for (int i = 0; i < count; i++)
        //    {
        //        token.ThrowIfCancellationRequested();

        //        ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
        //        try
        //        {
        //            var reply = await pingSender.SendPingAsync(target, 1000);
        //            if (reply.Status == IPStatus.Success)
        //            {
        //                item.SubItems.Add("Success");
        //                item.SubItems.Add(reply.RoundtripTime + " ms");
        //                item.SubItems.Add(reply.Options?.Ttl.ToString() ?? "N/A");
        //            }
        //            else
        //            {
        //                item.SubItems.Add(reply.Status.ToString());
        //                item.SubItems.Add("-");
        //                item.SubItems.Add("-");
        //            }
        //        }
        //        catch
        //        {
        //            item.SubItems.Add("Error");
        //            item.SubItems.Add("-");
        //            item.SubItems.Add("-");
        //        }

        //        InvokeIfRequired(() => lvPingResults.Items.Add(item));
        //        UpdateProgressBar((i + 1) * 100 / count);
        //        await Task.Delay(500, token);
        //    }
        //}

        private async void BtnTraceRoute_Click(object sender, EventArgs e)
        {
            string target = txtTarget.Text.Trim();
            if (string.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please enter a valid IP or hostname.");
                return;
            }

            DisableButtonsForOperation();
            _cts = new CancellationTokenSource();
            txtTraceRoute.Clear();
            toolStripStatusLabel.Text = $"Performing traceroute to {target}...";
            progressBar.Value = 0;

            try
            {
                var hops = await PerformTracerouteAsync(target, _cts.Token);
                InvokeIfRequired(() => txtTraceRoute.Lines = hops.ToArray());
                toolStripStatusLabel.Text = "Traceroute complete.";
            }
            catch (OperationCanceledException)
            {
                toolStripStatusLabel.Text = "Traceroute cancelled.";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = $"Traceroute error: {ex.Message}";
            }
            finally
            {
                progressBar.Value = 0;
                EnableButtonsAfterOperation();
            }
        }

        private async Task<List<string>> PerformTracerouteAsync(string target, CancellationToken token)
        {
            List<string> hops = new List<string>();
            int maxHops = 30;
            int timeout = 3000;

            using Ping pingSender = new Ping();

            for (int ttl = 1; ttl <= maxHops; ttl++)
            {
                token.ThrowIfCancellationRequested();

                var options = new PingOptions(ttl, true);
                string hopResult = $"{ttl}: ";

                try
                {
                    var reply = await pingSender.SendPingAsync(target, timeout, new byte[32], options);
                    if (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.Success)
                    {
                        hopResult += $"{reply.Address} ({reply.Status})";
                        hops.Add(hopResult);
                        if (reply.Status == IPStatus.Success) break;
                    }
                    else
                    {
                        hops.Add(hopResult + "Request timed out");
                    }
                }
                catch
                {
                    hops.Add(hopResult + "Error");
                }

                UpdateProgressBar((ttl * 100) / maxHops);
            }

            return hops;
        }

        private async void BtnDnsLookup_Click(object sender, EventArgs e)
        {
            string target = txtTarget.Text.Trim();
            if (string.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please enter a valid IP or hostname.");
                return;
            }

            DisableButtonsForOperation();
            _cts = new CancellationTokenSource();
            txtDnsInfo.Clear();
            toolStripStatusLabel.Text = $"Performing DNS lookup for {target}...";

            try
            {
                var hostEntry = await Dns.GetHostEntryAsync(target);
                var sb = new StringBuilder();
                sb.AppendLine($"Host Name: {hostEntry.HostName}");
                sb.AppendLine("IP Addresses:");
                foreach (var ip in hostEntry.AddressList)
                    sb.AppendLine($" - {ip}");

                txtDnsInfo.Text = sb.ToString();
                toolStripStatusLabel.Text = "DNS lookup complete.";
            }
            catch (Exception ex)
            {
                txtDnsInfo.Text = "DNS lookup failed: " + ex.Message;
                toolStripStatusLabel.Text = "DNS lookup failed.";
            }
            finally
            {
                EnableButtonsAfterOperation();
            }
        }

        private async void BtnPortScan_Click(object sender, EventArgs e)
        {
            string target = txtTarget.Text.Trim();
            if (string.IsNullOrEmpty(target))
            {
                MessageBox.Show("Please enter a valid IP or hostname.");
                return;
            }

            DisableButtonsForOperation();
            _cts = new CancellationTokenSource();
            txtOpenPorts.Clear();
            toolStripStatusLabel.Text = $"Scanning ports on {target}...";
            progressBar.Value = 0;

            try
            {
                var openPorts = await ScanPortsAsync(target, 1, 1024, _cts.Token);

                if (openPorts.Count == 0)
                {
                    txtOpenPorts.Text = $"No open ports found on {target} (scanned ports 1–1024).";
                }
                else
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Open ports on {target} (scanned ports 1–1024):");
                    foreach (var port in openPorts)
                    {
                        sb.AppendLine($" - Port {port}: Open");
                    }
                    txtOpenPorts.Text = sb.ToString();
                }
                toolStripStatusLabel.Text = "Port scan complete.";
            }
            catch (OperationCanceledException)
            {
                txtOpenPorts.Text = "Port scan cancelled.";
                toolStripStatusLabel.Text = "Port scan cancelled.";
            }
            catch (Exception ex)
            {
                txtOpenPorts.Text = $"Port scan failed: {ex.Message}";
                toolStripStatusLabel.Text = "Port scan failed.";
            }
            finally
            {
                progressBar.Value = 0;
                EnableButtonsAfterOperation();
            }
        }


        //private async Task<List<int>> ScanPortsAsync(string ip, int startPort, int endPort, CancellationToken token)
        //{
        //    List<int> openPorts = new List<int>();
        //    int totalPorts = endPort - startPort + 1;

        //    for (int port = startPort; port <= endPort; port++)
        //    {
        //        token.ThrowIfCancellationRequested();

        //        try
        //        {
        //            using var client = new TcpClient();
        //            var connectTask = client.ConnectAsync(ip, port);
        //            var timeoutTask = Task.Delay(300);

        //            if (await Task.WhenAny(connectTask, timeoutTask) == connectTask && client.Connected)
        //                openPorts.Add(port);
        //        }
        //        catch { }

        //        UpdateProgressBar((port - startPort + 1) * 100 / totalPorts);
        //    }

        //    return openPorts;
        //}


        private async Task<List<int>> ScanPortsAsync(string ip, int startPort, int endPort, CancellationToken token)
        {
            var openPorts = new ConcurrentBag<int>();
            int totalPorts = endPort - startPort + 1;
            int completed = 0;
            int maxConcurrency = 100; // Limit concurrency to avoid overload
            var semaphore = new SemaphoreSlim(maxConcurrency);

            var sw = Stopwatch.StartNew();

            var tasks = Enumerable.Range(startPort, totalPorts).Select(async port =>
            {
                await semaphore.WaitAsync(token);
                try
                {
                    token.ThrowIfCancellationRequested();

                    using var client = new TcpClient();
                    var connectTask = client.ConnectAsync(ip, port);
                    var timeoutTask = Task.Delay(300, token);

                    if (await Task.WhenAny(connectTask, timeoutTask) == connectTask && client.Connected)
                        openPorts.Add(port);
                }
                catch { }
                finally
                {
                    Interlocked.Increment(ref completed);

                    // Throttle progress bar updates - update every 5%
                    if (completed % (totalPorts / 20) == 0 || completed == totalPorts)
                    {
                        int progressValue = (completed * 100) / totalPorts;
                        UpdateProgressBar(progressValue);
                    }

                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);

            sw.Stop();
            Console.WriteLine($"Port scan completed in {sw.ElapsedMilliseconds} ms.");

            return openPorts.OrderBy(p => p).ToList();
        }



        //private async Task GetGeoLocation(string ip, CancellationToken token)
        //{
        //    try
        //    {
        //        toolStripStatusLabel.Text = $"Getting geolocation for {ip}...";
        //        string url = $"https://ipinfo.io/{ip}/json";
        //        using var response = await _httpClient.GetAsync(url, token).ConfigureAwait(false);
        //        response.EnsureSuccessStatusCode();

        //        using var stream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
        //        using var json = await JsonDocument.ParseAsync(stream, cancellationToken: token).ConfigureAwait(false);

        //        var sb = new StringBuilder();
        //        if (json.RootElement.TryGetProperty("city", out var city)) sb.AppendLine("City: " + city.GetString());
        //        if (json.RootElement.TryGetProperty("region", out var region)) sb.AppendLine("Region: " + region.GetString());
        //        if (json.RootElement.TryGetProperty("country", out var country)) sb.AppendLine("Country: " + country.GetString());
        //        if (json.RootElement.TryGetProperty("org", out var org)) sb.AppendLine("Org: " + org.GetString());
        //        if (json.RootElement.TryGetProperty("loc", out var loc)) sb.AppendLine("Coords: " + loc.GetString());

        //        InvokeIfRequired(() => txtGeoLocation.Text = sb.ToString());
        //        toolStripStatusLabel.Text = "Geolocation fetched.";
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        toolStripStatusLabel.Text = "Geolocation cancelled.";
        //    }
        //    catch (Exception ex)
        //    {
        //        InvokeIfRequired(() => txtGeoLocation.Text = "Error: " + ex.Message);
        //        toolStripStatusLabel.Text = "Geolocation failed.";
        //    }
        //}

        private async Task GetGeoLocation(string ip, CancellationToken token)
        {
            try
            {
                toolStripStatusLabel.Text = $"Getting geolocation for {ip}...";
                string url = $"https://ipinfo.io/{ip}/json";

                using var response = await _httpClient.GetAsync(url, token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
                using var json = await JsonDocument.ParseAsync(stream, cancellationToken: token).ConfigureAwait(false);

                var sb = new StringBuilder();
                double latitude = 0;
                double longitude = 0;

                if (json.RootElement.TryGetProperty("city", out var city))
                    sb.AppendLine("City: " + city.GetString());
                if (json.RootElement.TryGetProperty("region", out var region))
                    sb.AppendLine("Region: " + region.GetString());
                if (json.RootElement.TryGetProperty("country", out var country))
                    sb.AppendLine("Country: " + country.GetString());
                if (json.RootElement.TryGetProperty("org", out var org))
                    sb.AppendLine("Org: " + org.GetString());
                if (json.RootElement.TryGetProperty("loc", out var loc))
                {
                    string locStr = loc.GetString(); // Format: "lat,lon"
                    sb.AppendLine("Coords: " + locStr);

                    var parts = locStr?.Split(',');
                    if (parts != null && parts.Length == 2 &&
                        double.TryParse(parts[0], out double lat) &&
                        double.TryParse(parts[1], out double lon))
                    {
                        lastLatitude = lat;
                        lastLongitude = lon;
                    }
                }


                // Show in UI
                InvokeIfRequired(() => txtGeoLocation.Text = sb.ToString());
                toolStripStatusLabel.Text = "Geolocation fetched.";
            }
            catch (OperationCanceledException)
            {
                toolStripStatusLabel.Text = "Geolocation cancelled.";
            }
            catch (Exception ex)
            {
                InvokeIfRequired(() => txtGeoLocation.Text = "Error: " + ex.Message);
                toolStripStatusLabel.Text = "Geolocation failed.";
            }
        }


        private void DisableButtonsForOperation()
        {
            ToggleActionButtons(false);
            btnStopAction.Enabled = true;
            txtTarget.Enabled = false;
        }

        private void EnableButtonsAfterOperation()
        {
            btnStopAction.Enabled = false;
            txtTarget.Enabled = true;
            ToggleActionButtons(IsValidIpOrHostname(txtTarget.Text.Trim()));
        }

        private void ToggleActionButtons(bool enabled)
        {
            btnStartPing.Enabled = enabled;
            btnTraceRoute.Enabled = enabled;
            btnDnsLookup.Enabled = enabled;
            btnPortScan.Enabled = enabled;
        }

        private void UpdateProgressBar(int value)
        {
            InvokeIfRequired(() => progressBar.Value = Math.Min(100, Math.Max(0, value)));
        }

        private void InvokeIfRequired(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
        private void txtGeoLocation_Click(object sender, EventArgs e)
        {
            if (lastLatitude.HasValue && lastLongitude.HasValue)
            {
                var mapForm = new MyMap();
                mapForm.Show();
                mapForm.AddMapMarker(lastLatitude.Value, lastLongitude.Value, "Location from IP");
            }
            else
            {
                MessageBox.Show("No geolocation data available yet. Try clicking after fetching an IP.");
            }
        }

    }
}
