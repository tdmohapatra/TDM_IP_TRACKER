using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class MyMap : Form
    {
        public MyMap()
        {
            InitializeComponent();
        }

        private async void MyMap_Load(object sender, EventArgs e)
        {
            await webView2.EnsureCoreWebView2Async();

            // Open DevTools to debug (remove for production)
            webView2.CoreWebView2.OpenDevToolsWindow();

            string html = @"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'/>
    <title>Map</title>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.3/dist/leaflet.css'/>
    <script src='https://unpkg.com/leaflet@1.9.3/dist/leaflet.js'></script>
<style>
  html, body, #map {
    height: 100%;
    margin: 0;
    padding: 0;
  }
</style>

</head>
<body style='margin:0'>
    <div id='map' style='width:100%; height:100%'></div>
    <script>
        var map = L.map('map').setView([20.5937, 78.9629], 4);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors'
        }).addTo(map);

        window.addMarker = function(lat, lon, label) {
            L.marker([lat, lon]).addTo(map).bindPopup(label).openPopup();
            map.setView([lat, lon], 10);
        };
    </script>
</body>
</html>";

            webView2.NavigateToString(html);
        }

        public async void AddMapMarker(double latitude, double longitude, string label)
        {
            if (webView2.CoreWebView2 != null)
            {
                string escapedLabel = label.Replace("'", "\\'");
                string script = $"window.addMarker({latitude}, {longitude}, '{escapedLabel}');";
                await webView2.ExecuteScriptAsync(script);
            }
        }
    }
}
