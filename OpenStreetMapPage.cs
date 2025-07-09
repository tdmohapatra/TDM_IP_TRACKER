using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
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
            // Ensure WebView2 is properly initialized
            await webView2.EnsureCoreWebView2Async();

            // After initialization, disable DevTools (optional)
            webView2.CoreWebView2.Settings.AreDevToolsEnabled = false;

            // Now that WebView2 is initialized, proceed with loading the map
            webView2.CoreWebView2.WebMessageReceived += WebView2_WebMessageReceived;

            // Load the OpenStreetMap content
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

            // Load the map content
            webView2.NavigateToString(html);
        }

        private void WebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                // The raw message is received as a string
                string message = e.WebMessageAsJson;
                Console.WriteLine("Received message: " + message);

                // Ensure that we are receiving a valid JSON string
                if (!string.IsNullOrEmpty(message))
                {
                    // Deserialize the JSON string into a dynamic object
                    dynamic msg = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(message);

                    // Ensure the 'type' field is valid and proceed accordingly
                    if (msg != null && msg.type != null && msg.type == "coordinates")
                    {
                        double lat = msg.lat;
                        double lng = msg.lng;
                        Console.WriteLine($"Coordinates from JS → Lat: {lat}, Lon: {lng}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid or missing 'type' field in message.");
                    }
                }
                else
                {
                    Console.WriteLine("Received empty or null message.");
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Error parsing message: " + ex.Message);
            }
        }


        /// <summary>
        /// Adds/updates a marker from C#
        /// </summary>
        public async void AddMapMarker(double latitude, double longitude, string label)
        {
            // Ensure WebView2 is fully initialized
            if (webView2.CoreWebView2 != null)
            {
                string js = $"setMarker({latitude},{longitude},'{label.Replace("'", "\\'")}');";
                await webView2.ExecuteScriptAsync(js); // Execute JavaScript code to add a marker
            }
            else
            {
                MessageBox.Show("WebView2 is not initialized. Please try again later.");
            }
        }
    }

}
