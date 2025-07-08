using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class OpenStreetMapPage : Form
    {
        public OpenStreetMapPage()
        {
            InitializeComponent();
        }

        private async void OpenStreetMapPage_Load(object sender, EventArgs e)
        {
            await webView2.EnsureCoreWebView2Async();
            webView2.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webView2.CoreWebView2.WebMessageReceived += WebView2_WebMessageReceived;
            LoadOpenStreetMap();
        }

        private void LoadOpenStreetMap()
        {
            string html = @"
<!DOCTYPE html>
<html>
<head><meta charset='utf-8'/><title>Map</title>
<link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.3/dist/leaflet.css'/>
<script src='https://unpkg.com/leaflet@1.9.3/dist/leaflet.js'></script>
<style>html,body,#map{height:100%;margin:0;padding:0;}#search-container{position:absolute;top:10px;left:10px;z-index:1000;background:white;padding:10px;border-radius:5px;box-shadow:0 0 10px rgba(0,0,0,0.2);}#search-input{width:200px;padding:5px;}</style>
</head>
<body>
    <div id='search-container'>
        <input id='search-input' type='text' placeholder='Search location...'>
        <button id='search-button'>Search</button>
        <button id='locate-button'>My Location</button>
    </div>
    <div id='map'></div>
    <script>
    var map, markers=[];
    window.onload = function(){
        map = L.map('map').setView([20.5937,78.9629],4);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',{attribution:'© OpenStreetMap contributors',maxZoom:19}).addTo(map);

        document.getElementById('search-button').onclick = function(){
            var q = document.getElementById('search-input').value;
            if(!q) return;
            fetch('https://nominatim.openstreetmap.org/search?format=json&q='+encodeURIComponent(q))
                .then(r=>r.json()).then(d=>{
                    if(d && d.length>0){
                        var lat=parseFloat(d[0].lat), lon=parseFloat(d[0].lon);
                        setMarker(lat,lon,q);
                        window.chrome.webview.postMessage(JSON.stringify({type:'coordinates',lat:lat,lng:lon}));
                    }
                });
        };
        document.getElementById('locate-button').onclick = function(){
            navigator.geolocation.getCurrentPosition(pos=>{
                setMarker(pos.coords.latitude, pos.coords.longitude,'My Location');
                window.chrome.webview.postMessage(JSON.stringify({type:'coordinates',lat:pos.coords.latitude,lng:pos.coords.longitude}));
            },err=>alert('Error: '+err.message));
        };
    };
    function setMarker(lat, lon, label){
        markers.forEach(m=>map.removeLayer(m));
        markers = [];
        var mk=L.marker([lat,lon]).addTo(map).bindPopup(label).openPopup();
        markers.push(mk);
        map.setView([lat,lon],15);
    }
    </script>
</body>
</html>";

            webView2.NavigateToString(html);
        }

        private void WebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(e.WebMessageAsJson);
                if (msg?.type == "coordinates")
                {
                    double lat = msg.lat;
                    double lng = msg.lng;
                    Console.WriteLine($"Coords from JS → Lat: {lat}, Lon: {lng}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing message: " + ex.Message);
            }
        }

        /// <summary>
        /// Adds/updates a marker from C#
        /// </summary>
        public async void AddMarker(double latitude, double longitude, string label)
        {
            string js = $"setMarker({latitude},{longitude},'{label.Replace("'", "\\'")}');";
            await webView2.ExecuteScriptAsync(js);
        }
    }
}
