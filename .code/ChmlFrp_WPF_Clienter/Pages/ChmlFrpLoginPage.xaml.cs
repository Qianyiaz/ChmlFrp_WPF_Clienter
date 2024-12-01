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
using System;


namespace ChmlFrp_WPF_Clienter.Pages
{
    /// <summary>
    /// ChmlFrpLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrpLoginPage : Page
    {
        string directoryPath = Directory.GetCurrentDirectory();
        private Timer timer;

        public ChmlFrpLoginPage()
        {
            InitializeComponent();
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
                    timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(0), Timeout.InfiniteTimeSpan);
                }
                TextBox_Username.Text = data["ChmlFrp_WPF_Clienter Setup"]["Username"];
                TextBox_password.Text = data["ChmlFrp_WPF_Clienter Setup"]["Password"];
            }
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var ChmlFrpPage = new ChmlfrpPage();
                NavigationService.Navigate(ChmlFrpPage);
            });
        }

        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(temp_Username);
            data["ChmlFrp_WPF_Clienter Setup"]["Username"] = TextBox_Username.Text;
            parser.WriteFile(temp_Username, data);
        }

        private void TextBox_password_ini(object sender, TextChangedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(temp_Username);
            data["ChmlFrp_WPF_Clienter Setup"]["Password"] = TextBox_password.Text;
            parser.WriteFile(temp_Username, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_path = Path.Combine(CFL, "temp"); string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
            string url = "https://cf-v2.uapis.cn/login?username=" + TextBox_Username.Text + "&password=" + TextBox_password.Text;
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
                var ChmlFrpPage = new ChmlfrpPage();
                NavigationService.Navigate(ChmlFrpPage);
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
