using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace JdrAssistant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Init();
            InitAsync();
        }

        private async void InitAsync()
        {
            // For production mode we will copy the react build in /dist inside a wwwroot folder and point on index.html
            //var webViewPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");

            // For development mode we just point on the local server
            var webViewPath = "http://localhost:5173";

            await webView.EnsureCoreWebView2Async();
            webView.Source = new Uri(webViewPath);

            webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
        }

        private void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var message = JsonSerializer.Deserialize<Dictionary<string, string>>(e.WebMessageAsJson);

            if (message == null) return;

            switch (message["action"])
            {
                case "addCampaign":
                    string name = message["name"];
                    string id = Guid.NewGuid().ToString();
                    Database.AddCampaign(id, name);

                    // Retourner le nouveau
                    var campaignObj = new { type = "campaignAdded", id, name };
                    webView.CoreWebView2.PostWebMessageAsJson(JsonSerializer.Serialize(campaignObj));
                    break;

                case "getCampaigns":
                    var campaigns = Database.GetCampaigns();
                    var response = new { type = "campaignList", campaigns };
                    webView.CoreWebView2.PostWebMessageAsJson(JsonSerializer.Serialize(response));
                    break;
            }
        }
    }
}
