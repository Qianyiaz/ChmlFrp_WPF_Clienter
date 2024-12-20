using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;
using IniParser.Model;
using IniParser;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ChmlFrp_WPF_Clienter.Pages
{
    /// <summary>
    /// ChmlFrpLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrpLoginPage : Page
    {
        public ChmlFrpLoginPage()
        {
            InitializeComponent();
            ClienterClass ClienterClass = new ClienterClass();
            //directoryPath = ClienterClass.DirectoryPath();
            //cflPath = ClienterClass.CFLPath();
            //frpPath = ClienterClass.FrpPath();
            //frpIniPath = ClienterClass.FrpIniPath();
            //frpExePath = ClienterClass.FrpExePath();
            setupIniPath = ClienterClass.SetupIniPath();
            //temp_path = ClienterClass.Temp_path();
            temp_api_path = ClienterClass.Temp_api_path();
            //pictures_path = ClienterClass.Pictures_path();
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(setupIniPath);
            TextBox_Username.Text = data["ChmlFrp_WPF_Clienter Setup"]["Username"];
            TextBox_password.Text = data["ChmlFrp_WPF_Clienter Setup"]["Password"];
        }

        //private string directoryPath;
        //private string frpPath;
        //private string frpIniPath;
        //private string frpExePath;
        private string setupIniPath;
        //private string temp_path;
        private string temp_api_path;
        //private string cflPath;
        //private string pictures_path;


        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(setupIniPath);
            data["ChmlFrp_WPF_Clienter Setup"]["Username"] = TextBox_Username.Text;
            parser.WriteFile(setupIniPath, data);
        }

        private void TextBox_password_ini(object sender, TextChangedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(setupIniPath);
            data["ChmlFrp_WPF_Clienter Setup"]["Password"] = TextBox_password.Text;
            parser.WriteFile(setupIniPath, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            string url = "https://cf-v2.uapis.cn/login?username=" + TextBox_Username.Text + "&password=" + TextBox_password.Text;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    File.WriteAllText(temp_api_path, webClient.DownloadString(url));
                }
                catch
                {
                    return;
                }
            }

            string jsonContent = File.ReadAllText(temp_api_path);
            var parsedJson = JToken.Parse(jsonContent);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            var jsonObject = JObject.Parse(jsonContent);
            string msg = jsonObject["msg"]?.ToString();
            if (msg == "登录成功")
            {
                NavigationService.Navigate(new ChmlFrphomePage());
            }
            else
            {
                TextBox_Username.Text = null;
                TextBox_password.Text = null;
                logonButton.Click += logon;
            }
        }
    }
}
