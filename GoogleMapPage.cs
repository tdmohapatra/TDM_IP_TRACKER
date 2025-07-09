using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace TDM_IP_Tracker
{
    public partial class GoogleMapPage : Form
    {
        private const string GoogleMapsApiKey = "YOUR_GOOGLE_MAPS_API_KEY"; // Replace this!

        public GoogleMapPage()
        {
            InitializeComponent();
            this.Load += GoogleMapPage_Load;
        }

        private async void InitializeAsync()
        {
            // Ensure WebView2 is initialized
            await webView2.EnsureCoreWebView2Async();
            webView2.CoreWebView2.Settings.AreDevToolsEnabled = false;

            // Handle messages from the JavaScript (coordinates)
            webView2.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

            // Load the Google Map in the WebView2 control
            LoadGoogleMap();
        }

        private void LoadGoogleMap()
        {
            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>Google Maps</title>
    <style>
        html, body, #map {{
            height: 100%;
            margin: 0;
            padding: 0;
        }}
        #floating-panel {{
            position: absolute;
            top: 10px;
            left: 25%;
            z-index: 5;
            background-color: #fff;
            padding: 5px;
            border: 1px solid #999;
            width: 50%;
            text-align: center;
            font-family: 'Roboto', 'sans-serif';
            line-height: 30px;
        }}
        #directions-panel {{
            height: 150px;
            overflow: auto;
            margin-top: 10px;
        }}
    </style>
</head>
<body>
    <div id='floating-panel'>
        <input id='address' type='textbox' placeholder='Search place or address' />
        <input id='submit' type='button' value='Search' />
        <button id='current-loc'>My Location</button>
        <button id='get-directions'>Get Directions</button>
    </div>
    <div id='map'></div>
    <div id='directions-panel'></div>

    <script>
        let map;
        let marker;
        let geocoder;
        let directionsService;
        let directionsRenderer;

        function initMap() {{
            geocoder = new google.maps.Geocoder();
            map = new google.maps.Map(document.getElementById('map'), {{
                center: {{lat: 0, lng: 0}},
                zoom: 2,
                mapTypeControl: true,
                streetViewControl: true,
                fullscreenControl: true
            }});

            marker = new google.maps.Marker({{
                position: {{lat: 0, lng: 0}},
                map: map,
                draggable: true,
                title: 'Your location'
            }});

            directionsService = new google.maps.DirectionsService();
            directionsRenderer = new google.maps.DirectionsRenderer();
            directionsRenderer.setMap(map);
            directionsRenderer.setPanel(document.getElementById('directions-panel'));

            document.getElementById('submit').addEventListener('click', geocodeAddress);

            document.getElementById('current-loc').addEventListener('click', () => {{
                if (navigator.geolocation) {{
                    navigator.geolocation.getCurrentPosition(
                        (position) => {{
                            const pos = {{
                                lat: position.coords.latitude,
                                lng: position.coords.longitude
                            }};
                            map.setCenter(pos);
                            marker.setPosition(pos);
                            map.setZoom(15);

                            // Send coordinates to WinForms app via WebView2
                            window.chrome.webview.postMessage(JSON.stringify({{
                                type: 'coordinates',
                                lat: pos.lat,
                                lng: pos.lng
                            }}));
                        }},
                        () => alert('Error: The Geolocation service failed.')
                    );
                }} else {{
                    alert('Error: Your browser doesn\'t support geolocation.');
                }}
            }});

            document.getElementById('get-directions').addEventListener('click', () => {{
                const start = marker.getPosition();
                const end = prompt('Enter destination address:');
                if (!end) return;

                directionsService.route({{
                    origin: start,
                    destination: end,
                    travelMode: 'DRIVING'
                }}, (response, status) => {{
                    if (status === 'OK') {{
                        directionsRenderer.setDirections(response);
                    }} else {{
                        alert('Directions request failed due to ' + status);
                    }}
                }});
            }});

            marker.addListener('dragend', () => {{
                const pos = marker.getPosition();
                window.chrome.webview.postMessage(JSON.stringify({{
                    type: 'coordinates',
                    lat: pos.lat(),
                    lng: pos.lng()
                }}));
            }});
        }}

        function geocodeAddress() {{
            const address = document.getElementById('address').value;
            if (!address) return;
            geocoder.geocode({{ 'address': address }}, (results, status) => {{
                if (status === 'OK') {{
                    map.setCenter(results[0].geometry.location);
                    marker.setPosition(results[0].geometry.location);
                    map.setZoom(15);
                    window.chrome.webview.postMessage(JSON.stringify({{
                        type: 'coordinates',
                        lat: results[0].geometry.location.lat(),
                        lng: results[0].geometry.location.lng()
                    }}));
                }} else {{
                    alert('Geocode was not successful: ' + status);
                }}
            }});
        }}

        window.onload = initMap;
    </script>

    <script async defer
        src='https://maps.googleapis.com/maps/api/js?key={GoogleMapsApiKey}&callback=initMap&libraries=places'>
    </script>
</body>
</html>
";

            webView2.NavigateToString(html);
        }

        // Handle the message received from JavaScript (coordinates)
        private void CoreWebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<dynamic>(e.WebMessageAsJson);
                if (message.type == "coordinates")
                {
                    double lat = message.lat;
                    double lng = message.lng;

                    Console.WriteLine($"Coordinates received from JS: {lat}, {lng}");
                    // TODO: Update UI or perform other actions with coordinates here
                    // Example: Set the location in your form's controls or use it for other logic
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing message from JavaScript: " + ex.Message);
            }
        }

        private void GoogleMapPage_Load(object sender, EventArgs e)
        {
            InitializeAsync();
        }
    }
}
