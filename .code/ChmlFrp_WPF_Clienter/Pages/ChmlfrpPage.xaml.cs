using System.IO;
using System.Windows.Controls;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Windows.Media.Imaging;
using System.Net;


namespace ChmlFrp_WPF_Clienter.Pages
{
    /// <summary>
    /// ChmlfrpPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlfrpPage : Page
    {
        private string directoryPath;
        private string frpPath;
        private string frpIniPath;
        private string frpExePath;
        private string setupIniPath;
        private string temp_path;
        private string temp_api_path;
        private string CFLPath;
        private void InitializePaths()
        {
            directoryPath = Directory.GetCurrentDirectory();
            CFLPath = Path.Combine(directoryPath, "CFL");
            frpPath = Path.Combine(CFLPath, "frp");
            frpIniPath = Path.Combine(frpPath, "frpc.ini");
            frpExePath = Path.Combine(frpPath, "frpc.exe");
            setupIniPath = Path.Combine(CFLPath, "Setup.ini");
            temp_path = Path.Combine(CFLPath, "temp");
            temp_api_path = Path.Combine(temp_path, "Chmlfrp_api.json");
        }

        public ChmlfrpPage()
        {
            InitializeComponent();
            InitializePaths();
            Uri uri = new Uri("/Pages/ChmlFrpLoginPages/HomePage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }

        private void rdLaunchPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("/Pages/ChmlFrpLoginPages/HomePage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }

        private void rdTMA_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("/Pages/BlankPage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }
    }
}
