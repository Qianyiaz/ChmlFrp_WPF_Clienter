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

namespace ChmlFrpLauncher_cs.Pages
{
    /// <summary>
    /// ChmFrp.xaml 的交互逻辑
    /// </summary>
    public partial class ChmFrp : Page
    {
        public ChmFrp()
        {
            InitializeComponent();
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_path = Path.Combine(CFL, "temp"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            if (File.Exists(temp_Username))
            {
                IniData data;
                data = new IniData();
                var parser = new FileIniDataParser();
                data = parser.ReadFile(temp_Username);
                Username_TextBox.Text = data["ChmlFrpLauncher_cs Setup"]["Username"];
                Userpassword_TextBox.Text = data["ChmlFrpLauncher_cs Setup"]["Password"];
                text_msg.Text = null;
                directoryPath = Directory.GetCurrentDirectory(); string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
                string url = "https://cf-v2.uapis.cn/login?username=" + Username_TextBox.Text + "&password=" + Userpassword_TextBox.Text;
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
                text_msg.Text = msg;
                if (msg == "登录成功")
                {

                }
            }
        }
        string directoryPath = Directory.GetCurrentDirectory();
        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            //创建路径函数
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(temp_Username);
            data["ChmlFrpLauncher_cs Setup"]["Username"] = Username_TextBox.Text;
            try
            {
                data["ChmlFrpLauncher_cs Setup"]["Password"] = Userpassword_TextBox.Text;
            }
            catch { }
            parser.WriteFile(temp_Username, data);
        }

        private void TextBox_password_ini(object sender, TextChangedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_Username = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(temp_Username);
            data["ChmlFrpLauncher_cs Setup"]["Password"] = Userpassword_TextBox.Text;
            try
            {
                data["ChmlFrpLauncher_cs Setup"]["Username"] = Username_TextBox.Text;
            }
            catch { }
            parser.WriteFile(temp_Username, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string temp_path = Path.Combine(CFL, "temp"); string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
            string url = "https://cf-v2.uapis.cn/login?username=" + Username_TextBox.Text + "&password=" + Userpassword_TextBox.Text;
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
            text_msg.Text = msg;
            if (msg == "登录成功")
            {

            }
            else
            {
                logonButton.Click += logon;
            }
        }
    }
}
