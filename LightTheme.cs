using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDM_IP_Tracker
{
    public class LightTheme : Theme
    {
        public LightTheme(Color themeColor) : base(themeColor) { }

        public override void Apply(Control control)
        {
            if (control is MassIPForm form)
            {
                // Apply light theme to form
                form.BackColor = Color.White;

                // Apply to other controls
                if (form.panelTitle != null)
                    form.panelTitle.BackColor = ThemeColor;

                if (form.statusStrip != null)
                {
                    form.statusStrip.BackColor = Color.FromArgb(240, 240, 240);
                    form.statusStrip.ForeColor = Color.Black;
                }

                if (form.panelIPContainer != null)
                    form.panelIPContainer.BackColor = Color.FromArgb(240, 240, 240);

                if (form.tabControl != null)
                    form.tabControl.BackColor = Color.FromArgb(240, 240, 240);

                if (form.tabPageDashboard != null)
                    form.tabPageDashboard.BackColor = Color.FromArgb(240, 240, 240);

                if (form.tabPageDetails != null)
                    form.tabPageDetails.BackColor = Color.FromArgb(240, 240, 240);

                if (form.dataGridViewDetails != null)
                {
                    form.dataGridViewDetails.BackgroundColor = Color.White;
                    form.dataGridViewDetails.DefaultCellStyle.BackColor = Color.White;
                    form.dataGridViewDetails.DefaultCellStyle.ForeColor = Color.Black;
                    form.dataGridViewDetails.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                    form.dataGridViewDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    form.dataGridViewDetails.GridColor = SystemColors.Control;
                }

                if (form.chartStatus != null)
                {
                    form.chartStatus.BackColor = Color.White;
                    if (form.chartStatus.ChartAreas.Count > 0)
                    {
                        form.chartStatus.ChartAreas[0].BackColor = Color.White;
                        form.chartStatus.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                        form.chartStatus.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                    }
                    if (form.chartStatus.Legends.Count > 0)
                    {
                        form.chartStatus.Legends[0].BackColor = Color.White;
                        form.chartStatus.Legends[0].ForeColor = Color.Black;
                    }
                }

                if (form.panelStats != null)
                {
                    form.panelStats.BackColor = Color.White;
                    form.panelStats.ForeColor = Color.Black;
                }

                // Update label colors
                if (form.lblTotalIPs != null) form.lblTotalIPs.ForeColor = Color.Black;
                if (form.lblActiveIPs != null) form.lblActiveIPs.ForeColor = Color.Black;
                if (form.lblFailedIPs != null) form.lblFailedIPs.ForeColor = Color.Black;
                if (form.label2 != null) form.label2.ForeColor = Color.Black;
                if (form.label3 != null) form.label3.ForeColor = Color.Black;
                if (form.label4 != null) form.label4.ForeColor = Color.Black;
            }


        }
    }
}
