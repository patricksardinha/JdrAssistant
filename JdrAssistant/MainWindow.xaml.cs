using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System.IO;
using System.Windows;

namespace JdrAssistant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
        }
    }
}
