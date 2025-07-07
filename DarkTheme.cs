using System;
using System.Drawing;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public class DarkTheme : Theme
    {
        public DarkTheme(Color themeColor) : base(themeColor) { }

        public override void Apply(Control control)
        {
            if (control is not MassIPForm form) return;

            Color background = Color.FromArgb(45, 45, 48);
            Color panelBackground = Color.FromArgb(60, 60, 60);
            Color textColor = Color.White;

            form.SuspendLayout();
            form.BackColor = background;

            // Top panel
            if (form.panelTitle != null)
                form.panelTitle.BackColor = ThemeColor;

            // Status strip
            if (form.statusStrip != null)
            {
                form.statusStrip.BackColor = panelBackground;
                form.statusStrip.ForeColor = textColor;
            }

            // IP Container and Tabs
            if (form.panelIPContainer != null)
                form.panelIPContainer.BackColor = panelBackground;

            if (form.tabControl != null)
                form.tabControl.BackColor = panelBackground;

            if (form.tabPageDashboard != null)
                form.tabPageDashboard.BackColor = panelBackground;

            if (form.tabPageDetails != null)
                form.tabPageDetails.BackColor = panelBackground;

            // DataGridView Styling
            if (form.dataGridViewDetails != null)
            {
                var grid = form.dataGridViewDetails;
                grid.BackgroundColor = panelBackground;
                grid.DefaultCellStyle.BackColor = panelBackground;
                grid.DefaultCellStyle.ForeColor = textColor;
                grid.ColumnHeadersDefaultCellStyle.BackColor = background;
                grid.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
                grid.GridColor = Color.Gray;
            }

            // Chart Styling
            if (form.chartStatus != null)
            {
                form.chartStatus.BackColor = panelBackground;
                if (form.chartStatus.ChartAreas.Count > 0)
                {
                    var area = form.chartStatus.ChartAreas[0];
                    area.BackColor = panelBackground;
                    area.AxisX.LabelStyle.ForeColor = textColor;
                    area.AxisY.LabelStyle.ForeColor = textColor;
                }

                if (form.chartStatus.Legends.Count > 0)
                {
                    var legend = form.chartStatus.Legends[0];
                    legend.BackColor = panelBackground;
                    legend.ForeColor = textColor;
                }
            }

            // Stats Panel
            if (form.panelStats != null)
            {
                form.panelStats.BackColor = panelBackground;
                form.panelStats.ForeColor = textColor;
            }

            // Labels
            void SetLabelColor(Label lbl)
            {
                if (lbl != null)
                    lbl.ForeColor = textColor;
            }

            SetLabelColor(form.lblTotalIPs);
            SetLabelColor(form.lblActiveIPs);
            SetLabelColor(form.lblFailedIPs);
            SetLabelColor(form.label2);
            SetLabelColor(form.label3);
            SetLabelColor(form.label4);

            form.ResumeLayout();
        }
    }
}
