using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
using System.Net;

namespace TDM_IP_Tracker
{
    public partial class MassIPForm : Form
    {
        // Constants
        private const int DEFAULT_AUTO_CHECK_INTERVAL = 10000; // 10 seconds
        private const int STATS_REFRESH_INTERVAL = 5000; // 5 seconds
        private const int IP_CONTROL_MARGIN = 5;

        // Fields
        private readonly List<IPControl> ipControls = new List<IPControl>();
        private readonly Dictionary<string, List<IPControl>> sectionIPs = new Dictionary<string, List<IPControl>>();
        private readonly Dictionary<string, int> sectionStats = new Dictionary<string, int>();

        private bool darkTheme = false;
        private int autoCheckInterval = DEFAULT_AUTO_CHECK_INTERVAL;
        private System.Timers.Timer statsRefreshTimer;
        private Color currentThemeColor = Color.FromArgb(0, 120, 215);

        private readonly object sectionStatsLock = new object();
        private readonly object sectionIPsLock = new object();
        private readonly object _sectionLock = new object();

        #region Initialization

        public MassIPForm()
        {
            InitializeComponent();
            InitializeApplication();
            InitializeSectionMonitoringTab();
        }

        private void InitializeApplication()
        {
            InitializeDataGridView();
            //InitializeChart();
            UpdateChartBySection();
            InitializeSectionMonitoring();
            SetupTimers();
            ConfigureInitialUI();
        }

        private void InitializeDataGridView()
        {
            dataGridViewDetails.Columns.Add("IP", "IP Address");
            dataGridViewDetails.Columns.Add("Status", "Status");
            dataGridViewDetails.Columns.Add("ResponseTime", "Response Time (ms)");
            dataGridViewDetails.Columns.Add("LastChecked", "Last Checked");
            dataGridViewDetails.Columns.Add("HostName", "Host Name");
        }
        private void InitializeSectionMonitoringTab()
        {
            TabPage tabPageSections = new TabPage("Section Monitoring");
            tabControl.TabPages.Add(tabPageSections);

            // Ensure the tab page has a minimum size
            tabPageSections.MinimumSize = new Size(100, 100);

            // Use a SplitContainer for better layout management
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.SplitterDistance = 100; // 70% for chart, 30% for grid

            // Initialize and configure the chart
            sectionChart = new Chart();
            sectionChart.Dock = DockStyle.Fill;
            sectionChart.MinimumSize = new Size(100, 100);

            ChartArea chartArea = new ChartArea();
            sectionChart.ChartAreas.Add(chartArea);

            Series series = new Series("Section Status");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            sectionChart.Series.Add(series);

            sectionChart.Legends.Add(new Legend());


            // In InitializeComponent()
            sectionGridView = new DataGridView();
            sectionGridView.Name = "sectionGridView";
            sectionGridView.Dock = DockStyle.Fill;
            sectionGridView.AutoGenerateColumns = false;

            // Add columns
            sectionGridView.Columns.Add("Section", "Section");
            sectionGridView.Columns.Add("Count", "Count");
            sectionGridView.Columns.Add("Status", "Status");
            // Add controls to the split container
            splitContainer.Panel1.Controls.Add(sectionChart);
            splitContainer.Panel2.Controls.Add(sectionGridView);

            // Add the split container to the tab page
            tabPageSections.Controls.Add(splitContainer);

            // Force a layout update
            tabPageSections.PerformLayout();
        }
        private void UpdateSectionChart()
        {
            if (sectionChart == null) return;

            var series = sectionChart.Series["Section Status"];
            series.Points.Clear();

            List<string> sections;
            Dictionary<string, int> statsSnapshot;
            Dictionary<string, List<IPControl>> ipsSnapshot;

            lock (sectionStatsLock)
            {
                statsSnapshot = new Dictionary<string, int>(sectionStats);
                sections = statsSnapshot.Keys.OrderBy(s => s).ToList();
            }

            lock (sectionIPsLock)
            {
                ipsSnapshot = new Dictionary<string, List<IPControl>>(sectionIPs);
            }

            foreach (var section in sections)
            {
                if (!ipsSnapshot.TryGetValue(section, out var ipList))
                    continue;

                int total = ipList.Count;
                int active = statsSnapshot[section];

                var point = new DataPoint
                {
                    AxisLabel = section,
                    YValues = new double[] { active },
                    Label = $"{active}/{total}",
                    Color = Color.FromArgb(76, 175, 80),
                    LabelForeColor = Color.Black
                };

                series.Points.Add(point);
            }

            if (sectionChart.ChartAreas.Count > 0)
            {
                var area = sectionChart.ChartAreas[0];

                area.AxisX.Interval = 1;
                area.AxisX.LabelStyle.Angle = -45;

                area.Position.Auto = false;
                area.Position.X = 0;
                area.Position.Y = 0;
                area.Position.Width = 100;
                area.Position.Height = 100;

                area.InnerPlotPosition.Auto = false;
                area.InnerPlotPosition.X = 5;
                area.InnerPlotPosition.Y = 5;
                area.InnerPlotPosition.Width = 90;
                area.InnerPlotPosition.Height = 90;
            }
        }


        private void UpdateChartBySection()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateChartBySection));
                return;
            }

            if (chartStatus == null) return;
            if (ipControls == null || ipControls.Count == 0) return;

            var grouped = ipControls
                .GroupBy(ip => ip.Section)
                .Select(g => new
                {
                    Section = g.Key,
                    Total = g.Count(),
                    Active = g.Count(ip => ip.LastStatus == IPStatus.Success)
                }).ToList();

            var series = chartStatus.Series["Section Availability"];
            series.Points.Clear();

            foreach (var group in grouped)
            {
                double availabilityPercent = group.Total > 0 ? (group.Active * 100.0 / group.Total) : 0;

                // Add data point with availabilityPercent as Y value
                var point = series.Points.Add(availabilityPercent);

                // Label with section name and percentage
                point.Label = $"{group.Section} ({availabilityPercent:F1}%)";

                // Tooltip (optional)
                point.ToolTip = $"{group.Section}: {availabilityPercent:F1}% availability";

                // Legend text for the slice
                point.LegendText = group.Section;
            }
        }

        private void InitializeChart()
        {
            if (chartStatus == null) return;

            chartStatus.Series.Add("IP Status");
            chartStatus.Series["IP Status"].ChartType = SeriesChartType.Pie;
            chartStatus.Series["IP Status"]["PieLabelStyle"] = "Disabled";

            if (chartStatus.ChartAreas.Count > 0)
            {
                chartStatus.ChartAreas[0].Area3DStyle.Enable3D = true;
            }
        }
        private void InitializeSectionMonitoring()
        {
            // Initialize section chart
            if (sectionChart != null)
            {
                const string seriesName = "Section Status";

                if (!sectionChart.Series.IsUniqueName(seriesName))
                {
                    sectionChart.Series[seriesName].Points.Clear(); // reuse and reset
                }
                else
                {
                    var series = sectionChart.Series.Add(seriesName);
                    series.ChartType = SeriesChartType.Column;
                    series.Color = Color.SteelBlue; // Optional: choose a consistent theme color
                }
            }

            // Initialize section grid view
            var sectionGrid = GetSectionGridView();
            if (sectionGrid != null && sectionGrid.Columns.Count == 0)
            {
                sectionGrid.Columns.Add("Section", "Section");
                sectionGrid.Columns.Add("Total", "Total IPs");
                sectionGrid.Columns.Add("Active", "Active IPs");
                sectionGrid.Columns.Add("Inactive", "Inactive IPs");
                sectionGrid.Columns.Add("Availability", "Availability %");

                // Optional: formatting
                sectionGrid.Columns["Availability"].DefaultCellStyle.Format = "0.00'%'";
                sectionGrid.Columns["Availability"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }



        private void SetupTimers()
        {
            autoTimer.Interval = autoCheckInterval;

            statsRefreshTimer = new System.Timers.Timer(STATS_REFRESH_INTERVAL)
            {
                AutoReset = true,
                Enabled = true
            };
            statsRefreshTimer.Elapsed += (s, e) => RefreshStats();
        }

        private void ConfigureInitialUI()
        {
            numColumns.Value = 4;
            UpdateStats();
            ApplyTheme();
        }

        #endregion

        #region IP Management

        private void LoadIPsFromExcel()
        {
            using (var openFileDialog = new OpenFileDialog { Filter = "Excel Files|*.xls;*.xlsx;*.xlsm" })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    ProcessExcelFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error loading Excel file: {ex.Message}");
                    UpdateStatus("Error loading Excel file");
                }
            }
        }

        private void ProcessExcelFile(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                try
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    panelIPContainer.SuspendLayout();

                    ClearExistingIPs();
                    LoadIPsFromWorksheet(worksheet);

                    UpdateDataGridView();
                    UpdateStats();
                    UpdateSectionStats();
                    UpdateStatus($"Loaded IPs from {Path.GetFileName(filePath)}");
                }


                finally
                {
                    panelIPContainer.ResumeLayout();
                }
            }
        }

        private void ClearExistingIPs()
        {
            panelIPContainer.Controls.Clear();

            lock (sectionIPsLock)
            {
                sectionIPs.Clear();
            }
            lock (sectionStatsLock)
            {
                sectionStats.Clear();
            }

            ipControls.Clear();
        }


        private void LoadIPsFromWorksheet(ExcelWorksheet worksheet)
        {
            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                var ipAddress = worksheet.Cells[row, 1].Text;
                var section = string.IsNullOrEmpty(worksheet.Cells[row, 2].Text)
                    ? "General"
                    : worksheet.Cells[row, 2].Text;

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    AddIPControl(ipAddress, section);
                }
            }
        }

        private void AddIPControl(string ipAddress, string section = "General")
        {
            var ipControl = new IPControl(ipAddress)
            {
                Width = CalculateIPControlWidth(),
                Margin = new Padding(IP_CONTROL_MARGIN),
                Section = section
            };

            ipControl.StatusChanged += (s, e) =>
            {
                UpdateStats();
                UpdateDataGridView();
                UpdateSectionStats();
            };

            ipControls.Add(ipControl);
            panelIPContainer.Controls.Add(ipControl);
            AddToSection(ipControl, section);
        }

        private int CalculateIPControlWidth()
        {
            int spacingBetweenControls = 15;
            int columns = (int)numColumns.Value;

            int totalSpacing = (columns - 1) * spacingBetweenControls;
            int availableWidth = panelIPContainer.Width - totalSpacing;

            return availableWidth / columns;
        }


        private void AddToSection(IPControl ipControl, string section)
        {
            if (!sectionIPs.ContainsKey(section))
            {
                sectionIPs[section] = new List<IPControl>();
            }
            sectionIPs[section].Add(ipControl);
        }

        //private void TrackAllIPs()
        //{
        //    if (!ValidateIPsToTrack()) return;

        //    PrepareForTracking();

        //    Task.Run(() =>
        //    {
        //        //TrackIPsInParallel();

        //        TrackIPsAsync();
        //        //CompleteTracking();

        //    });
        //  //  CompleteTracking();
        //    UpdateChartBySection();
        //}

        private async void TrackAllIPs()
        {
            if (!ValidateIPsToTrack()) return;

            PrepareForTracking();

            await Task.Run(async () =>
            {
                await TrackIPsAsync(); // properly awaited
            });

            CompleteTracking();         // only called after tracking finishes
            UpdateChartBySection();     // safe to update chart now
        }


        private bool ValidateIPsToTrack()
        {
            if (ipControls.Count == 0)
            {
                ShowInformationMessage("No IPs to track. Please add IPs first.");
                return false;
            }
            return true;
        }

        private void PrepareForTracking()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(PrepareForTracking));
                return;
            }

            statusProgress.Style = ProgressBarStyle.Marquee;
            UpdateStatus("Tracking IPs...");
            btnTrack.Enabled = false;
        }

        private async Task TrackIPsAsync()
        {
            var ipsToTrack = ipControls.Where(ip => ip.Checked || !chkAutoCheck.Checked).ToList();
            int total = ipsToTrack.Count;
            int processed = 0;

            var progress = new Progress<int>(percent =>
            {
                UpdateProgressBar(percent);  // safely updates UI
            });

            var tasks = ipsToTrack.Select(async ipControl =>
            {
                await ipControl.PingAsync();  // assuming PingAsync returns a Task
                int processedCount = Interlocked.Increment(ref processed);
                int progressValue = (int)((double)processedCount / total * 100);
                ((IProgress<int>)progress).Report(progressValue);
            });

            await Task.WhenAll(tasks);
        }
        //private async Task TrackIPsInParallelAsync()
        //{
        //    var ipsToTrack = ipControls.Where(ip => ip.Checked || !chkAutoCheck.Checked).ToList();
        //    int total = ipsToTrack.Count;
        //    int processed = 0;

        //    var tasks = ipsToTrack.Select(async ipControl =>
        //    {
        //        await ipControl.PingAsync();  // Await the asynchronous ping

        //        int processedCount = Interlocked.Increment(ref processed);
        //        int progressValue = (int)((double)processedCount / total * 100);

        //        UpdateProgressBar(progressValue);
        //    });

        //    await Task.WhenAll(tasks);  // Wait for all pings to complete
        //}



        private void RemoveSelectedIPs()
        {
            var selectedIPs = ipControls.Where(ip => ip.Checked).ToList();

            if (selectedIPs.Count == 0)
            {
                ShowInformationMessage("No IPs selected for removal.");
                return;
            }

            if (ShowConfirmationMessage($"Remove {selectedIPs.Count} selected IP(s)?", "Confirm Removal"))
            {
                RemoveIPControls(selectedIPs);
                UpdateStats();
                UpdateStatus($"Removed {selectedIPs.Count} IP(s)");
            }
        }

        private void ClearAllIPs()
        {
            if (ipControls.Count == 0)
            {
                ShowInformationMessage("No IPs to clear.");
                return;
            }

            if (ShowConfirmationMessage("Clear all IPs?", "Confirm Clear"))
            {
                panelIPContainer.Controls.Clear();
                ipControls.Clear();
                sectionIPs.Clear();
                UpdateStats();
                UpdateStatus("Cleared all IPs");
            }
        }

        private void AddNewIP()
        {
            var ip = Microsoft.VisualBasic.Interaction.InputBox("Enter IP Address or Hostname:", "Add New IP");
            if (string.IsNullOrWhiteSpace(ip)) return;

            AddIPControl(ip);
            UpdateStats();
            UpdateStatus($"Added IP: {ip}");
        }

        #endregion

        #region UI Updates

        //private void UpdateStats()
        //{
        //    int total = ipControls.Count;
        //    int active = ipControls.Count(ip => ip.LastStatus == IPStatus.Success);
        //    int failed = total - active;

        //    UpdateStatsLabels(total, active, failed);
        //    UpdateChart(active, failed);
        //}

        private void UpdateStats()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateStats));
                return;
            }

            int total = ipControls.Count;
            int active = ipControls.Count(ip => ip.LastStatus == IPStatus.Success);
            int failed = total - active;

            UpdateStatsLabels(total, active, failed);
            UpdateChart(active, failed);
        }


        private void UpdateSectionStats()
        {
            lock (sectionStatsLock)
            {
                sectionStats.Clear();

                lock (sectionIPsLock)
                {
                    foreach (var section in sectionIPs.Keys)
                    {
                        int active = sectionIPs[section].Count(ip => ip.LastStatus == IPStatus.Success);
                        sectionStats[section] = active;
                    }
                }
            }

            UpdateSectionGridView();
            UpdateSectionChart();
        }


        //private void UpdateDataGridView()
        //{
        //    if (dataGridViewDetails.InvokeRequired)
        //    {
        //        dataGridViewDetails.Invoke(new Action(UpdateDataGridView));
        //        return;
        //    }

        //    dataGridViewDetails.SuspendLayout();
        //    dataGridViewDetails.Rows.Clear();

        //    foreach (var ipControl in ipControls)
        //    {
        //        AddIPControlToGridView(ipControl);
        //    }

        //    dataGridViewDetails.ResumeLayout();
        //}

        private void UpdateDataGridView()
        {
            if (dataGridViewDetails.InvokeRequired)
            {
                dataGridViewDetails.Invoke(new Action(UpdateDataGridView));
                return;
            }

            dataGridViewDetails.SuspendLayout();
            dataGridViewDetails.Rows.Clear();

            foreach (var ipControl in ipControls)
            {
                AddIPControlToGridView(ipControl);
            }

            dataGridViewDetails.ResumeLayout(true);
        }


        private void AddIPControlToGridView(IPControl ipControl)
        {
            var rowIndex = dataGridViewDetails.Rows.Add(
                ipControl.IPAddress,
                ipControl.LastStatus == IPStatus.Success ? "Online" : "Offline",
                ipControl.LastResponseTime > 0 ? ipControl.LastResponseTime.ToString() : "N/A",
                ipControl.LastChecked.ToString("g"),
                ipControl.HostName ?? "N/A"
            );

            ColorRowBasedOnStatus(rowIndex, ipControl.LastStatus);
        }

        //private void UpdateSectionGridView()
        //{
        //    var grid = GetSectionGridView();
        //    if (grid == null) return;

        //    if (grid.InvokeRequired)
        //    {
        //        grid.Invoke(new Action(UpdateSectionGridView));
        //        return;
        //    }

        //    grid.SuspendLayout();
        //    grid.Rows.Clear();

        //    foreach (var section in sectionStats.Keys.OrderBy(s => s))
        //    {
        //        AddSectionToGridView(grid, section);
        //    }

        //    grid.ResumeLayout();
        //}

        private void UpdateSectionGridView()
        {
            var grid = GetSectionGridView();
            if (grid == null) return;

            if (grid.InvokeRequired)
            {
                grid.Invoke(new Action(UpdateSectionGridView));
                return;
            }

            grid.SuspendLayout();
            grid.Rows.Clear();

            List<string> sections;
            lock (sectionStatsLock)
            {
                sections = sectionStats.Keys.OrderBy(s => s).ToList();
            }

            foreach (var section in sections)
            {
                AddSectionToGridView(grid, section);
            }

            grid.ResumeLayout();
        }


        private DataGridView GetSectionGridView()
        {
            if (tabControl == null)
                return null;

            // Look for tab page by name
            var tabPage = tabControl.TabPages
                .Cast<TabPage>()
                .FirstOrDefault(tp => tp.Name == "tabPageSectionMonitoring" || tp.Text == "Section Monitoring");

            if (tabPage == null)
                return null;

            // Search for the DataGridView by name
            return tabPage.Controls.Find("sectionGridView", true).FirstOrDefault() as DataGridView;
        }


        //private void AddSectionToGridView(DataGridView grid, string section)
        //{
        //    int total = sectionIPs[section].Count;
        //    int active = sectionStats[section];
        //    int inactive = total - active;

        //    int rowIndex = grid.Rows.Add(
        //        section,
        //        total,
        //        active,
        //        inactive,
        //        $"{Math.Round((double)active / total * 100, 2)}%"
        //    );

        //    FormatSectionGridRow(grid, rowIndex, inactive, total);
        //}


        private void AddSectionToGridView(DataGridView grid, string section)
        {
            int total, active;

            lock (_sectionLock)
            {
                if (!sectionIPs.TryGetValue(section, out var ipList) || !sectionStats.TryGetValue(section, out active))
                    return;

                total = ipList.Count;
            }

            int inactive = total - active;

            int rowIndex = grid.Rows.Add(
                section,
                total,
                active,
                inactive,
                total > 0 ? $"{Math.Round((double)active / total * 100, 2)}%" : "0%"
            );

            FormatSectionGridRow(grid, rowIndex, inactive, total);
        }

        private static void FormatSectionGridRow(DataGridView grid, int rowIndex, int inactive, int total)
        {
            if (inactive > 0)
            {
                grid.Rows[rowIndex].DefaultCellStyle.BackColor =
                    inactive == total ? Color.LightPink : Color.LightYellow;
            }
        }

        private void ApplyTheme()
        {
            Theme theme = darkTheme ?
                (Theme)new DarkTheme(currentThemeColor) :
                new LightTheme(currentThemeColor);
            theme.Apply(this);
        }

        private void AdjustIPControlWidths()
        {
            int newWidth = CalculateIPControlWidth();
            foreach (var ipControl in ipControls)
            {
                ipControl.Width = newWidth;
            }
        }

        private void ColorRowBasedOnStatus(int rowIndex, IPStatus status)
        {
            dataGridViewDetails.Rows[rowIndex].DefaultCellStyle.BackColor = status == IPStatus.Success
                ? Color.FromArgb(230, 245, 230)
                : Color.FromArgb(255, 230, 230);
        }

        private void ConfigureSettings()
        {
            using (var settingsForm = new SettingsForm(autoCheckInterval, currentThemeColor))
            {
                if (settingsForm.ShowDialog() != DialogResult.OK) return;

                autoCheckInterval = settingsForm.AutoCheckInterval;
                autoTimer.Interval = autoCheckInterval;
                currentThemeColor = settingsForm.ThemeColor;
                ApplyTheme();
                UpdateStatus("Settings updated");
            }
        }

        private void ExportData()
        {
            if (ipControls.Count == 0)
            {
                ShowInformationMessage("No data to export.");
                return;
            }

            using (var saveFileDialog = new SaveFileDialog { Filter = "CSV Files|*.csv|Excel Files|*.xlsx" })
            {
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (saveFileDialog.FileName.EndsWith(".csv"))
                    {
                        ExportToCSV(saveFileDialog.FileName);
                    }
                    else
                    {
                        ExportToExcel(saveFileDialog.FileName);
                    }

                    UpdateStatus($"Results exported to {Path.GetFileName(saveFileDialog.FileName)}");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Export failed: {ex.Message}");
                    UpdateStatus("Export failed");
                }
            }
        }

        private void ExportToCSV(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("IP Address,Host Name,Status,Response Time (ms),Last Checked");

                foreach (var ipControl in ipControls)
                {
                    writer.WriteLine(
                        $"\"{ipControl.IPAddress}\"," +
                        $"\"{ipControl.HostName ?? "N/A"}\"," +
                        $"\"{(ipControl.LastStatus == IPStatus.Success ? "Online" : "Offline")}\"," +
                        $"\"{(ipControl.LastResponseTime > 0 ? ipControl.LastResponseTime.ToString() : "N/A")}\"," +
                        $"\"{ipControl.LastChecked.ToString("g")}\""
                    );
                }
            }
        }

        private void ExportToExcel(string filePath)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("IP Tracking");

                    // Headers
                    worksheet.Cells[1, 1].Value = "IP Address";
                    worksheet.Cells[1, 2].Value = "Host Name";
                    worksheet.Cells[1, 3].Value = "Status";
                    worksheet.Cells[1, 4].Value = "Response Time (ms)";
                    worksheet.Cells[1, 5].Value = "Last Checked";

                    // Format headers
                    var headerRange = worksheet.Cells["A1:E1"];
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    // Data
                    for (int i = 0; i < ipControls.Count; i++)
                    {
                        var ipControl = ipControls[i];
                        var row = i + 2;

                        worksheet.Cells[row, 1].Value = ipControl.IPAddress;
                        worksheet.Cells[row, 2].Value = ipControl.HostName ?? "N/A";
                        worksheet.Cells[row, 3].Value = ipControl.LastStatus == IPStatus.Success ? "Online" : "Offline";
                        worksheet.Cells[row, 4].Value = ipControl.LastResponseTime > 0 ? ipControl.LastResponseTime : 0;
                        worksheet.Cells[row, 5].Value = ipControl.LastChecked.ToString("g");

                        // Format row
                        var rowRange = worksheet.Cells[row, 1, row, 5];
                        rowRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowRange.Style.Fill.BackgroundColor.SetColor(
                            ipControl.LastStatus == IPStatus.Success
                                ? Color.LightGreen
                                : Color.LightPink
                        );
                    }

                    worksheet.Cells.AutoFitColumns();
                    package.SaveAs(new FileInfo(filePath));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Excel export failed: " + ex.Message);
            }
        }

        private void ExportToPDF()
        {
            if (ipControls.Count == 0)
            {
                ShowInformationMessage("No data to export.");
                return;
            }

            using (var saveFileDialog = new SaveFileDialog { Filter = "PDF Files|*.pdf" })
            {
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    CreateSimplePDF(saveFileDialog.FileName);
                    UpdateStatus($"Exported to PDF: {Path.GetFileName(saveFileDialog.FileName)}");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"PDF export failed: {ex.Message}");
                    UpdateStatus("PDF export failed");
                }
            }
        }

        private void CreateSimplePDF(string filePath)
        {
            // This is a simplified version - in a real app you would use iTextSharp
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("%PDF-1.4");
                // ... PDF content would go here
                writer.WriteLine("%%EOF");
            }
        }

        private void RefreshStats()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateStats));
            }
            else
            {
                UpdateStats();
            }
        }

        private void RemoveIPControls(List<IPControl> ipControlsToRemove)
        {
            foreach (var ipControl in ipControlsToRemove)
            {
                panelIPContainer.Controls.Remove(ipControl);
                ipControls.Remove(ipControl);

                // Remove from sections
                foreach (var section in sectionIPs.Keys.ToList())
                {
                    if (sectionIPs[section].Contains(ipControl))
                    {
                        sectionIPs[section].Remove(ipControl);
                        if (sectionIPs[section].Count == 0)
                        {
                            sectionIPs.Remove(section);
                        }
                    }
                }
            }
        }

        private void SetAllIPControlsCheckedState(bool isChecked)
        {
            foreach (var ipControl in ipControls)
            {
                ipControl.Checked = isChecked;
            }
        }

        private void ToggleTheme()
        {
            darkTheme = !darkTheme;
            ApplyTheme();
            UpdateStatus(darkTheme ? "Dark theme applied" : "Light theme applied");
        }

        private void UpdateChart(int active, int failed)
        {
            if (chartStatus == null) return;

            var series = chartStatus.Series["IP Status"] ?? chartStatus.Series.Add("IP Status");
            series.Points.Clear();

            if (active + failed <= 0) return;

            series.Points.AddXY("Active", active);
            series.Points.AddXY("Failed", failed);

            series.Points[0].Color = Color.FromArgb(76, 175, 80);
            series.Points[1].Color = Color.FromArgb(244, 67, 54);
        }

        private void UpdateStatsLabels(int total, int active, int failed)
        {
            lblTotalIPs.Text = total.ToString();
            lblActiveIPs.Text = active.ToString();
            lblFailedIPs.Text = failed.ToString();
        }

        #endregion

        #region Utility Methods

        private void CompleteTracking()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CompleteTracking));
                return;
            }

            statusProgress.Style = ProgressBarStyle.Continuous;
            statusProgress.Value = statusProgress.Maximum; // Show progress is 100%
            UpdateStatus("Tracking completed");
            btnTrack.Enabled = true;
            UpdateDataGridView();
        }


        private void UpdateProgressBar(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgressBar), value);
                return;
            }

            // Only update if not in Marquee mode
            if (statusProgress.Style != ProgressBarStyle.Marquee)
            {
                value = Math.Min(statusProgress.Maximum, Math.Max(statusProgress.Minimum, value));

                // Show the progress bar if hidden
                if (!statusProgress.Visible)
                    statusProgress.Visible = true;

                statusProgress.Value = value;

                // Optional: Update a status label too
                statusLabel.Text = $"Progress: {value}%";

                // Optional: auto-hide and reset after reaching 100%
                if (value >= statusProgress.Maximum)
                {
                    Task.Delay(1000).ContinueWith(_ =>
                    {
                        if (!IsDisposed)
                        {
                            Invoke(new Action(() =>
                            {
                                statusProgress.Visible = false;
                                statusProgress.Value = 0;
                                statusLabel.Text = "Completed";
                            }));
                        }
                    });
                }
            }
        }



        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }

            statusLabel.Text = message;
        }

        private void ShowInformationMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ShowConfirmationMessage(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        #endregion

        #region Event Handlers

        private void MassIPForm_Load(object sender, EventArgs e) { }

        private void btnLoadExcel_Click(object sender, EventArgs e) => LoadIPsFromExcel();

        private void btnTrack_Click(object sender, EventArgs e) => TrackAllIPs();

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetAllIPControlsCheckedState(true);
            UpdateStatus("Selected all IPs");
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            SetAllIPControlsCheckedState(false);
            UpdateStatus("Deselected all IPs");
        }

        private void chkAutoCheck_CheckedChanged(object sender, EventArgs e)
        {
            autoTimer.Enabled = chkAutoCheck.Checked;
            UpdateStatus(chkAutoCheck.Checked ? "Auto check enabled" : "Auto check disabled");
        }

        private void btnAddIP_Click(object sender, EventArgs e) => AddNewIP();

        private void btnRemoveIP_Click(object sender, EventArgs e) => RemoveSelectedIPs();

        private void btnClear_Click(object sender, EventArgs e) => ClearAllIPs();

        private void btnExport_Click(object sender, EventArgs e) => ExportData();

        private void btnSettings_Click(object sender, EventArgs e) => ConfigureSettings();

        private void btnTheme_Click(object sender, EventArgs e) => ToggleTheme();

        private void btnExportPDF_Click(object sender, EventArgs e) => ExportToPDF();

        private void btnRefreshStats_Click(object sender, EventArgs e)
        {
            RefreshStats();
            UpdateStatus("Stats refreshed");
        }

        private void autoTimer_Tick(object sender, EventArgs e) => TrackAllIPs();

        private void numColumns_ValueChanged(object sender, EventArgs e) => AdjustIPControlWidths();

        private void panelIPContainer_Resize(object sender, EventArgs e) => AdjustIPControlWidths();

        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            // Implement fade effect if needed
        }

        #endregion

        private void statusProgress_Click(object sender, EventArgs e)
        {

        }

        private void panelDashboard_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    // Theme classes
    public abstract class Theme
    {
        protected Color ThemeColor { get; }

        protected Theme(Color themeColor)
        {
            ThemeColor = themeColor;
        }

        public abstract void Apply(Control control);
    }




}