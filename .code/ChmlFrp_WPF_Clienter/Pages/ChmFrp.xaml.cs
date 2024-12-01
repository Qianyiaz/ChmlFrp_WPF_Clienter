using System;
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
using System.Threading;
using ChmlFrp_WPF_Clienter.Classes;

namespace ChmlFrp_WPF_Clienter.Pages
{
    /// <summary>
    /// ChmFrp.xaml 的交互逻辑
    /// </summary>
    public partial class ChmFrp : Page
    {
        string directoryPath = Directory.GetCurrentDirectory();
        private Timer timer;
        public ChmFrp()
        {
            InitializeComponent();
            this.DataContext = new ModelClass();

            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_path = Path.Combine(CFL, "temp"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            if (File.Exists(temp_Username))
            {
                IniData data;
                var parser = new FileIniDataParser();
                data = parser.ReadFile(temp_Username);
                directoryPath = Directory.GetCurrentDirectory(); string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
                string url = "https://cf-v2.uapis.cn/login?username=" + data["ChmlFrp_WPF_Clienter Setup"]["Username"] + "&password=" + data["ChmlFrp_WPF_Clienter Setup"]["Password"];
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        WebClient webClient = new WebClient();
                        webClient.Encoding = Encoding.UTF8;
                        File.WriteAllText(temp_api, webClient.DownloadString(url));
                    }
                    catch
                    {
                        return;
                    }
                }
                string jsonContent = File.ReadAllText(temp_api);
                var parsedJson = JToken.Parse(jsonContent);
                string formattedJson = parsedJson.ToString(Formatting.Indented);
                var jsonObject = JObject.Parse(jsonContent);
                string msg = jsonObject["msg"]?.ToString();
                if (msg == "登录成功")
                {
                    ThreadStart childref = new ThreadStart(Navigation);
                    Thread childThread = new Thread(childref);
                    childThread.Start();
                    return;
                }
                ModelClass.Username = data["ChmlFrp_WPF_Clienter Setup"]["Username"];
                ModelClass.Userpassword = data["ChmlFrp_WPF_Clienter Setup"]["Password"];
            }
        }
        private void Navigation()
        {
                var ChmlFrpPage = new ChmlfrpPage();
                NavigationService.Navigate(ChmlFrpPage);
        }

        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(temp_Username);
            string name = ModelClass.Username;
            name = ModelClass.Username;
            data["ChmlFrp_WPF_Clienter Setup"]["Username"] = name;
            parser.WriteFile(temp_Username, data);
        }

        private void TextBox_password_ini(object sender, TextChangedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(temp_Username);
            string password = ModelClass.Userpassword;
            password = ModelClass.Userpassword;
            data["ChmlFrp_WPF_Clienter Setup"]["Password"] = password;
            parser.WriteFile(temp_Username, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_path = Path.Combine(CFL, "temp"); string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
            string url = "https://cf-v2.uapis.cn/login?username=" + ModelClass.Username + "&password=" + ModelClass.Userpassword;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    File.WriteAllText(temp_api, webClient.DownloadString(url));
                }
                catch
                {
                    return;
                }
            }

            string jsonContent = File.ReadAllText(temp_api);
            var parsedJson = JToken.Parse(jsonContent);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            var jsonObject = JObject.Parse(jsonContent);
            string msg = jsonObject["msg"]?.ToString();
            //text_msg.Text = msg;
            if (msg == "登录成功")
            {
                var ChmlFrpPage = new ChmlfrpPage();
                NavigationService.Navigate(ChmlFrpPage);
            }
            else
            {
                ModelClass.Username = null;
                ModelClass.Userpassword = null;
                logonButton.Click += logon;
            }
        }
    }
}
