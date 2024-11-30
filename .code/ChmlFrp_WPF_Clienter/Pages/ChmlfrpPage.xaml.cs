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
        string directoryPath = Directory.GetCurrentDirectory();
        public ChmlfrpPage()
        {
            InitializeComponent();
            directoryPath = Directory.GetCurrentDirectory();
            string CWC = Path.Combine(directoryPath, "CWC");
            string temp_path = Path.Combine(CWC, "temp");
            string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
            Uri uri = new Uri("/Pages/Chmlfrp/HomePage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }

        private void rdLaunchPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("/Pages/Chmlfrp/HomePage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }
    }
}
