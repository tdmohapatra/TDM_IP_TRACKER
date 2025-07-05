using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TDM_IP_Tracker
{
    public partial class MassIPForm : Form
    {
        private CancellationTokenSource _cts;
        private int _pingTimeout = 2000;
        private int _maxConcurrentPings = 50;
        private bool _darkMode = false;
        private List<IPCell> ipCells = new List<IPCell>();
        private int cellWidth = 200;
        private int cellHeight = 120;
        private double opacityIncrement = 0.05;
        private bool _isFirstLoad = true;

        public MassIPForm()
        {
            InitializeComponent();

            // Set initial opacity to 0 for fade-in effect
            this.Opacity = 0;

            // Configure tooltips and theme
            ConfigureToolTips();
            ApplyTheme();

            // Enable double buffering for smoother rendering
            this.DoubleBuffered = true;

            // Event handlers
            numColumns.ValueChanged += numColumns_ValueChanged;
            this.Load += MassIPForm_Load;

            // Start fade-in animation
            fadeTimer.Start();
        }

        private void MassIPForm_Load(object sender, EventArgs e)
        {
            // Add some sample IPs for demonstration
            AddIPCell("8.8.8.8");
            AddIPCell("1.1.1.1");
            AddIPCell("192.168.1.1");

            // Arrange cells after form is fully loaded
            this.BeginInvoke((MethodInvoker)delegate {
                ArrangeIPCells();
                _isFirstLoad = false;
            });
        }

        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += opacityIncrement;
            }
            else
            {
                fadeTimer.Stop();

                // Ensure form is fully visible and focused
                this.Show();
                this.Activate();
            }
        }

        private void panelToolbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HT_CAPTION, 0);
            }
        }

        private void ConfigureToolTips()
        {
            toolTip.AutomaticDelay = 500;
            toolTip.SetToolTip(btnLoadExcel, "Import IPs from Excel file");
            toolTip.SetToolTip(btnTrack, "Start pinging selected IPs");
            toolTip.SetToolTip(btnSelectAll, "Select all IPs");
            toolTip.SetToolTip(btnDeselectAll, "Deselect all IPs");
            toolTip.SetToolTip(btnAddIP, "Add single IP manually");
            toolTip.SetToolTip(btnRemoveIP, "Remove selected IPs");
            toolTip.SetToolTip(btnClear, "Clear all IPs");
            toolTip.SetToolTip(btnExport, "Export results to Excel");
            toolTip.SetToolTip(btnSettings, "Configure ping settings");
            toolTip.SetToolTip(btnTheme, "Toggle dark/light theme");
            toolTip.SetToolTip(chkAutoCheck, "Enable automatic pinging every 10 seconds");
            toolTip.SetToolTip(numColumns, "Number of columns in the grid");
        }

        private void ApplyTheme()
        {
            // Safely apply theme with null checks
            Color backColor, foreColor, panelColor, toolbarColor;

            if (_darkMode)
            {
                backColor = Color.FromArgb(32, 32, 32);
                foreColor = Color.White;
                panelColor = Color.FromArgb(40, 40, 40);
                toolbarColor = Color.FromArgb(0, 90, 180);
            }
            else
            {
                backColor = Color.White;
                foreColor = SystemColors.ControlText;
                panelColor = Color.FromArgb(240, 240, 240);
                toolbarColor = Color.FromArgb(0, 120, 215);
            }

            // Apply colors to form and controls
            this.BackColor = backColor;
            this.ForeColor = foreColor;

            // Safe control checks
            if (panelIPContainer != null)
            {
                panelIPContainer.BackColor = panelColor;
                panelIPContainer.ForeColor = foreColor;
            }

            if (panelToolbar != null) panelToolbar.BackColor = toolbarColor;
            if (panelTitle != null) panelTitle.BackColor = toolbarColor;
            if (statusStrip != null) statusStrip.BackColor = panelColor;

            // Apply theme to all IP cells
            foreach (var cell in ipCells)
            {
                cell.ApplyTheme(_darkMode);
            }
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xlsx;*.xls|CSV Files|*.csv|All Files|*.*";
                ofd.Title = "Select IP List File";

                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    ClearAllIPs();
                    statusLabel.Text = "Loading IPs...";
                    statusProgress.Visible = true;

                    Task.Run(() =>
                    {
                        using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
                        {
                            var sheet = package.Workbook.Worksheets[0];

                            for (int r = 2; r <= sheet.Dimension.Rows; r++)
                            {
                                var ip = sheet.Cells[r, 1].Text.Trim();
                                if (!string.IsNullOrWhiteSpace(ip))
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        AddIPCell(ip);
                                    });
                                }
                            }
                        }
                    }).ContinueWith(t =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            statusLabel.Text = $"Loaded {ipCells.Count} IPs";
                            statusProgress.Visible = false;
                            AnimateStatusChange("Data loaded successfully", Color.Green);
                            ArrangeIPCells();
                        });
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        statusLabel.Text = "Error loading file";
                        AnimateStatusChange($"Error: {ex.Message}", Color.Red);
                        statusProgress.Visible = false;
                    });
                }
            }
        }

        private void AddIPCell(string ipAddress)
        {
            var cell = new IPCell(ipAddress)
            {
                Width = cellWidth,
                Height = cellHeight,
                Margin = new Padding(5),
                Selected = true
            };

            cell.Click += IPCell_Click;
            ipCells.Add(cell);
            panelIPContainer.Controls.Add(cell);

            cell.ApplyTheme(_darkMode);

            if (!_isFirstLoad)
            {
                cell.Opacity = 0;
                var fadeInTimer = new Timer { Interval = 30 };
                fadeInTimer.Tick += (s, e) =>
                {
                    if (cell.Opacity < 1)
                    {
                        cell.Opacity += 0.1;
                        cell.Refresh();
                    }
                    else
                    {
                        fadeInTimer.Stop();
                        fadeInTimer.Dispose();
                    }
                };
                fadeInTimer.Start();
            }
            else
            {
                cell.Opacity = 1;
            }
        }

        private void IPCell_Click(object sender, EventArgs e)
        {
            var cell = (IPCell)sender;
            cell.Selected = !cell.Selected;

            var pulseTimer = new Timer { Interval = 50, Tag = 0 };
            pulseTimer.Tick += (s, evt) =>
            {
                int count = (int)pulseTimer.Tag;
                if (count < 5)
                {
                    cell.BorderColor = count % 2 == 0 ? Color.FromArgb(0, 120, 215) : Color.SkyBlue;
                    cell.Refresh();
                    pulseTimer.Tag = count + 1;
                }
                else
                {
                    cell.BorderColor = cell.Selected ? Color.FromArgb(0, 120, 215) : Color.Transparent;
                    cell.Refresh();
                    pulseTimer.Stop();
                    pulseTimer.Dispose();
                }
            };
            pulseTimer.Start();

            toolTip.Show($"IP: {cell.IPAddress}\nStatus: {cell.Status}\nResponse: {cell.ResponseTime}ms\nLast Seen: {cell.LastSeen}",
                        cell, 0, cell.Height, 5000);
        }

        private void btnSelectAll_Click(object sender, EventArgs e) => SetAllCheckboxes(true, sender);
        private void btnDeselectAll_Click(object sender, EventArgs e) => SetAllCheckboxes(false, sender);

        private void SetAllCheckboxes(bool val, object senderButton)
        {
            foreach (var cell in ipCells)
            {
                cell.Selected = val;
                cell.BorderColor = val ? Color.FromArgb(0, 120, 215) : Color.Transparent;
                cell.Refresh();
            }
            AnimateButton((Button)senderButton);
        }

        private void btnAddIP_Click(object sender, EventArgs e)
        {
            var ip = Prompt.ShowDialog("Enter IP or hostname:", "Add IP");
            if (string.IsNullOrWhiteSpace(ip)) return;

            AddIPCell(ip.Trim());
            ArrangeIPCells();
            AnimateButton(btnAddIP);
        }

        private void btnRemoveIP_Click(object sender, EventArgs e)
        {
            var selectedCells = ipCells.Where(c => c.Selected).ToList();
            if (selectedCells.Count == 0)
            {
                AnimateStatusChange("No IPs selected", Color.Orange);
                return;
            }

            var fadeTimers = new List<Timer>();
            foreach (var cell in selectedCells)
            {
                var fadeTimer = new Timer { Interval = 30, Tag = cell };
                fadeTimer.Tick += (s, evt) =>
                {
                    var c = (IPCell)fadeTimer.Tag;
                    if (c.Opacity > 0)
                    {
                        c.Opacity -= 0.1;
                        c.Refresh();
                    }
                    else
                    {
                        panelIPContainer.Controls.Remove(c);
                        ipCells.Remove(c);
                        fadeTimer.Stop();
                        fadeTimer.Dispose();
                    }
                };
                fadeTimer.Start();
                fadeTimers.Add(fadeTimer);
            }

            AnimateButton(btnRemoveIP);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (ipCells.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to clear all IPs?", "Confirm Clear",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearAllIPs();
                _cts?.Cancel();
                AnimateButton(btnClear);
                AnimateStatusChange("All IPs cleared", Color.Green);
            }
        }

        private void ClearAllIPs()
        {
            var fadeTimer = new Timer { Interval = 30 };
            fadeTimer.Tick += (s, e) =>
            {
                bool allInvisible = true;
                foreach (var cell in ipCells)
                {
                    if (cell.Opacity > 0)
                    {
                        cell.Opacity -= 0.10;
                        cell.Refresh();
                        allInvisible = false;
                    }
                }

                if (allInvisible)
                {
                    panelIPContainer.Controls.Clear();
                    ipCells.Clear();
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                }
            };
            fadeTimer.Start();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ipCells.Count == 0)
            {
                AnimateStatusChange("No data to export", Color.Orange);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Save Results";
                sfd.FileName = $"IP_Results_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    statusLabel.Text = "Exporting...";
                    statusProgress.Visible = true;

                    Task.Run(() =>
                    {
                        using (var package = new ExcelPackage())
                        {
                            var sheet = package.Workbook.Worksheets.Add("IP Results");

                            // Headers
                            sheet.Cells[1, 1].Value = "IP";
                            sheet.Cells[1, 2].Value = "Status";
                            sheet.Cells[1, 3].Value = "Response Time (ms)";
                            sheet.Cells[1, 4].Value = "Last Seen";

                            // Data
                            for (int i = 0; i < ipCells.Count; i++)
                            {
                                var cell = ipCells[i];
                                sheet.Cells[i + 2, 1].Value = cell.IPAddress;
                                sheet.Cells[i + 2, 2].Value = cell.Status;
                                sheet.Cells[i + 2, 3].Value = cell.ResponseTime;
                                sheet.Cells[i + 2, 4].Value = cell.LastSeen;
                            }

                            sheet.Cells.AutoFitColumns();
                            package.SaveAs(new FileInfo(sfd.FileName));
                        }
                    }).ContinueWith(t =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            statusLabel.Text = "Export completed";
                            statusProgress.Visible = false;
                            AnimateStatusChange("Data exported successfully", Color.Green);
                        });
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        statusLabel.Text = "Export failed";
                        AnimateStatusChange($"Error: {ex.Message}", Color.Red);
                        statusProgress.Visible = false;
                    });
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(_pingTimeout, _maxConcurrentPings);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                _pingTimeout = settingsForm.PingTimeout;
                _maxConcurrentPings = settingsForm.MaxConcurrentPings;
                AnimateStatusChange("Settings updated", Color.Green);
            }
            AnimateButton(btnSettings);
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            _darkMode = !_darkMode;
            ApplyTheme();
            AnimateButton(btnTheme);
            AnimateStatusChange(_darkMode ? "Dark theme enabled" : "Light theme enabled", Color.Green);
        }

        private void chkAutoCheck_CheckedChanged(object sender, EventArgs e)
        {
            autoTimer.Enabled = chkAutoCheck.Checked;
            if (!autoTimer.Enabled)
            {
                _cts?.Cancel();
                AnimateStatusChange("Auto-check disabled", Color.Orange);
            }
            else
            {
                _ = RunPingAsync();
                AnimateStatusChange("Auto-check enabled", Color.Green);
            }
        }

        private void autoTimer_Tick(object sender, EventArgs e) => _ = RunPingAsync();

        private void btnTrack_Click(object sender, EventArgs e)
        {
            AnimateButton(btnTrack);
            _ = RunPingAsync();
        }

        private async Task RunPingAsync()
        {
            if (ipCells.Count == 0)
            {
                AnimateStatusChange("No IPs to ping", Color.Orange);
                return;
            }

            ToggleControls(false);
            statusLabel.Text = "Pinging...";
            statusProgress.Style = ProgressBarStyle.Marquee;
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                var startTime = DateTime.Now;
                int successCount = 0;
                int failCount = 0;

                await Task.Run(() =>
                {
                    Parallel.ForEach(ipCells.Where(c => c.Selected),
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = _maxConcurrentPings,
                            CancellationToken = _cts.Token
                        },
                        cell =>
                        {
                            try
                            {
                                using var ping = new Ping();
                                var reply = ping.Send(cell.IPAddress, _pingTimeout);

                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (reply.Status == IPStatus.Success)
                                    {
                                        cell.Status = "Online";
                                        cell.ResponseTime = reply.RoundtripTime.ToString();
                                        cell.BackColor = _darkMode ? Color.FromArgb(0, 80, 0) : Color.FromArgb(200, 255, 200);
                                        cell.StatusImage = Properties.Resources.online_icon;
                                        successCount++;
                                    }
                                    else
                                    {
                                        cell.Status = "Offline";
                                        cell.ResponseTime = "";
                                        cell.BackColor = _darkMode ? Color.FromArgb(80, 0, 0) : Color.FromArgb(255, 200, 200);
                                        cell.StatusImage = Properties.Resources.offline_icon;
                                        failCount++;
                                    }
                                    cell.LastSeen = DateTime.Now.ToString("HH:mm:ss");

                                    var pulseTimer = new Timer { Interval = 50, Tag = 0 };
                                    pulseTimer.Tick += (s, e) =>
                                    {
                                        int count = (int)pulseTimer.Tag;
                                        if (count < 3)
                                        {
                                            cell.Opacity = count % 2 == 0 ? 0.7 : 1.0;
                                            cell.Refresh();
                                            pulseTimer.Tag = count + 1;
                                        }
                                        else
                                        {
                                            cell.Opacity = 1.0;
                                            cell.Refresh();
                                            pulseTimer.Stop();
                                            pulseTimer.Dispose();
                                        }
                                    };
                                    pulseTimer.Start();
                                });
                            }
                            catch (Exception ex)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    cell.Status = "Error";
                                    cell.ResponseTime = ex.Message;
                                    cell.BackColor = _darkMode ? Color.FromArgb(80, 40, 0) : Color.FromArgb(255, 220, 150);
                                    cell.StatusImage = Properties.Resources.error_icon;
                                    cell.LastSeen = DateTime.Now.ToString("HH:mm:ss");
                                    cell.Refresh();
                                    failCount++;
                                });
                            }
                        });
                }, _cts.Token);

                var duration = (DateTime.Now - startTime).TotalSeconds;
                AnimateStatusChange($"Ping completed: {successCount} success, {failCount} failed in {duration:0.0}s",
                    failCount == 0 ? Color.Green : (successCount == 0 ? Color.Red : Color.Orange));
            }
            catch (OperationCanceledException)
            {
                AnimateStatusChange("Ping operation cancelled", Color.Orange);
            }
            catch (Exception ex)
            {
                AnimateStatusChange($"Error: {ex.Message}", Color.Red);
            }
            finally
            {
                ToggleControls(true);
                statusProgress.Style = ProgressBarStyle.Blocks;
                statusProgress.Visible = false;
            }
        }

        private void ToggleControls(bool enable)
        {
            btnLoadExcel.Enabled = enable;
            btnTrack.Enabled = enable;
            btnSelectAll.Enabled = enable;
            btnDeselectAll.Enabled = enable;
            btnAddIP.Enabled = enable;
            btnRemoveIP.Enabled = enable;
            btnClear.Enabled = enable;
            btnExport.Enabled = enable;
            btnSettings.Enabled = enable;
            btnTheme.Enabled = enable;
            chkAutoCheck.Enabled = enable;
            numColumns.Enabled = enable;
        }

        private void AnimateButton(Button button)
        {
            var originalColor = button.BackColor;
            var highlightColor = Color.FromArgb(
                Math.Min(originalColor.R + 40, 255),
                Math.Min(originalColor.G + 40, 255),
                Math.Min(originalColor.B + 40, 255));

            button.BackColor = highlightColor;

            var timer = new Timer { Interval = 100 };
            timer.Tick += (s, e) =>
            {
                button.BackColor = originalColor;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void AnimateStatusChange(string message, Color color)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = color;

            var timer = new Timer { Interval = 3000 };
            timer.Tick += (s, e) =>
            {
                statusLabel.ForeColor = _darkMode ? Color.White : SystemColors.ControlText;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void numColumns_ValueChanged(object sender, EventArgs e)
        {
            ArrangeIPCells();
        }

        private void ArrangeIPCells()
        {
            if (panelIPContainer == null) return;

            panelIPContainer.SuspendLayout();

            try
            {
                int columns = Math.Max(1, (int)numColumns.Value);
                int availableWidth = panelIPContainer.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;
                cellWidth = (availableWidth / columns) - 10;

                foreach (var cell in ipCells)
                {
                    cell.Width = cellWidth;
                }
            }
            finally
            {
                panelIPContainer.ResumeLayout(true);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (!_isFirstLoad && ipCells.Count > 0)
            {
                ArrangeIPCells();
            }
        }
    }

    public class IPCell : Panel
    {
        public string IPAddress { get; set; }
        public string Status { get; set; } = "Pending";
        public string ResponseTime { get; set; } = "";
        public string LastSeen { get; set; } = DateTime.Now.ToString("HH:mm:ss");
        public bool Selected { get; set; }
        public Color BorderColor { get; set; } = Color.Transparent;
        public Image StatusImage { get; set; }
        public double Opacity { get; set; } = 1.0;

        public IPCell(string ipAddress)
        {
            IPAddress = ipAddress;
            this.DoubleBuffered = true;
            this.Paint += IPCell_Paint;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.White;
            StatusImage = Properties.Resources.pending_icon;
        }

        private void IPCell_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = this.ClientRectangle;

            if (Opacity < 1.0)
            {
                var bmp = new Bitmap(rect.Width, rect.Height);
                using (var gBmp = Graphics.FromImage(bmp))
                {
                    DrawCellContent(gBmp, new Rectangle(0, 0, rect.Width, rect.Height));
                }

                var matrix = new System.Drawing.Imaging.ColorMatrix();
                matrix.Matrix33 = (float)Opacity;
                var attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(matrix);

                g.DrawImage(bmp, rect,
                    0, 0, rect.Width, rect.Height,
                    GraphicsUnit.Pixel, attributes);
                return;
            }

            DrawCellContent(g, rect);
        }

        private void DrawCellContent(Graphics g, Rectangle rect)
        {
            using (var brush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(brush, rect);
            }

            if (BorderColor != Color.Transparent)
            {
                using (var pen = new Pen(BorderColor, 2))
                {
                    g.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3);
                }
            }

            if (StatusImage != null)
            {
                g.DrawImage(StatusImage, rect.Right - 40, rect.Top + 10, 30, 30);
            }

            using (var font = new Font("Segoe UI", 9))
            using (var boldFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var statusFont = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                g.DrawString(IPAddress, boldFont, Brushes.Black, new PointF(10, 10));

                var statusColor = Status switch
                {
                    "Online" => Color.Green,
                    "Offline" => Color.Red,
                    "Error" => Color.Orange,
                    _ => Color.Gray
                };
                using (var statusBrush = new SolidBrush(statusColor))
                {
                    g.DrawString($"Status: {Status}", statusFont, statusBrush, new PointF(10, 35));
                }

                g.DrawString($"Response: {ResponseTime}ms", font, Brushes.Black, new PointF(10, 55));
                g.DrawString($"Last: {LastSeen}", font, Brushes.Black, new PointF(10, 75));
            }

            using (var pen = new Pen(Color.FromArgb(220, 220, 220)))
            {
                g.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
        }

        public void ApplyTheme(bool darkMode)
        {
            if (darkMode)
            {
                this.ForeColor = Color.White;
                if (this.BackColor == Color.White)
                    this.BackColor = Color.FromArgb(40, 40, 40);
                else if (this.BackColor == Color.FromArgb(200, 255, 200))
                    this.BackColor = Color.FromArgb(0, 80, 0);
                else if (this.BackColor == Color.FromArgb(255, 200, 200))
                    this.BackColor = Color.FromArgb(80, 0, 0);
                else if (this.BackColor == Color.FromArgb(255, 220, 150))
                    this.BackColor = Color.FromArgb(80, 40, 0);
            }
            else
            {
                this.ForeColor = SystemColors.ControlText;
                if (this.BackColor == Color.FromArgb(40, 40, 40))
                    this.BackColor = Color.White;
                else if (this.BackColor == Color.FromArgb(0, 80, 0))
                    this.BackColor = Color.FromArgb(200, 255, 200);
                else if (this.BackColor == Color.FromArgb(80, 0, 0))
                    this.BackColor = Color.FromArgb(255, 200, 200);
                else if (this.BackColor == Color.FromArgb(80, 40, 0))
                    this.BackColor = Color.FromArgb(255, 220, 150);
            }
            this.Refresh();
        }
    }

    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
    }

    //public static class Prompt
    //{
    //    public static string ShowDialog(string text, string caption)
    //    {
    //        Form prompt = new Form()
    //        {
    //            Width = 300,
    //            Height = 150,
    //            FormBorderStyle = FormBorderStyle.FixedDialog,
    //            Text = caption,
    //            StartPosition = FormStartPosition.CenterScreen
    //        };

    //        Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 260 };
    //        TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 260 };
    //        Button confirmation = new Button() { Text = "OK", Left = 110, Width = 80, Top = 80, DialogResult = DialogResult.OK };

    //        confirmation.Click += (sender, e) => { prompt.Close(); };
    //        prompt.Controls.Add(textBox);
    //        prompt.Controls.Add(confirmation);
    //        prompt.Controls.Add(textLabel);
    //        prompt.AcceptButton = confirmation;

    //        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
    //    }
    //}

    //public class SettingsForm : Form
    //{
    //    public int PingTimeout { get; private set; }
    //    public int MaxConcurrentPings { get; private set; }

    //    public SettingsForm(int currentTimeout, int currentConcurrentPings)
    //    {
    //        this.Text = "Ping Settings";
    //        this.ClientSize = new Size(300, 150);
    //        this.FormBorderStyle = FormBorderStyle.FixedDialog;
    //        this.StartPosition = FormStartPosition.CenterParent;

    //        var lblTimeout = new Label { Text = "Ping Timeout (ms):", Left = 20, Top = 20 };
    //        var numTimeout = new NumericUpDown { Left = 150, Top = 20, Width = 120, Minimum = 100, Maximum = 10000, Value = currentTimeout };

    //        var lblConcurrent = new Label { Text = "Max Concurrent Pings:", Left = 20, Top = 50 };
    //        var numConcurrent = new NumericUpDown { Left = 150, Top = 50, Width = 120, Minimum = 1, Maximum = 100, Value = currentConcurrentPings };

    //        var btnOK = new Button { Text = "OK", Left = 110, Top = 80, Width = 80, DialogResult = DialogResult.OK };
    //        btnOK.Click += (sender, e) =>
    //        {
    //            PingTimeout = (int)numTimeout.Value;
    //            MaxConcurrentPings = (int)numConcurrent.Value;
    //            this.DialogResult = DialogResult.OK;
    //            this.Close();
    //        };

    //        this.Controls.Add(lblTimeout);
    //        this.Controls.Add(numTimeout);
    //        this.Controls.Add(lblConcurrent);
    //        this.Controls.Add(numConcurrent);
    //        this.Controls.Add(btnOK);
    //        this.AcceptButton = btnOK;
    //    }
    //}
}