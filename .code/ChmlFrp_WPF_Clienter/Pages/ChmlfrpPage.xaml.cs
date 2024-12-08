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
        //private string directoryPath;
        //private string frpPath;
        //private string frpIniPath;
        //private string frpExePath;
        //private string setupIniPath;
        //private string temp_path;
        //private string temp_api_path;
        //private string cflPath;
        //private string pictures_path;

        public ChmlfrpPage()
        {
            InitializeComponent();
            //ClienterClass ClienterClass = new ClienterClass();
            //directoryPath = ClienterClass.DirectoryPath();
            //cflPath = ClienterClass.CFLPath();
            //frpPath = ClienterClass.FrpPath();
            //frpIniPath = ClienterClass.FrpIniPath();
            //frpExePath = ClienterClass.FrpExePath();
            //setupIniPath = ClienterClass.SetupIniPath();
            //temp_path = ClienterClass.Temp_path();
            //temp_api_path = ClienterClass.Temp_api_path();
            //pictures_path = ClienterClass.Pictures_path();
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
            Uri uri = new Uri("/Pages/ChmlFrpLoginPages/TMAPage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }
    }
}
