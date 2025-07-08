namespace TDM_IP_Tracker
{
    partial class GoogleMapPage
    {
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;

        private void InitializeComponent()
        {
            this.webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.SuspendLayout();

            // webView2
            this.webView2.CreationProperties = null;
            this.webView2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2.Location = new System.Drawing.Point(0, 0);
            this.webView2.Name = "webView2";
            this.webView2.Size = new System.Drawing.Size(800, 450);
            this.webView2.TabIndex = 0;
            this.webView2.ZoomFactor = 1D;

            // GoogleMapPage
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webView2);
            this.Name = "GoogleMapPage";
            this.Text = "Google Map Viewer";
            this.Load += new System.EventHandler(this.GoogleMapPage_Load);
            this.ResumeLayout(false);
        }
    }
}
