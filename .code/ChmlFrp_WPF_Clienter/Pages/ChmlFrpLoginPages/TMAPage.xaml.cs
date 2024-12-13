using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IniParser.Model;
using IniParser;
using System.IO;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace ChmlFrp_WPF_Clienter.Pages.ChmlFrpLoginPages
{
    /// <summary>
    /// TMAPage.xaml 的交互逻辑
    /// </summary>
    public partial class TMAPage : Page
    {
        private int i;
        private string directoryPath;
        private string frpPath;
        private string frpIniPath;
        private string frpExePath;
        private string setupIniPath;
        private string temp_path;
        private string temp_api_path;
        private string cflPath;
        private string pictures_path;
        private string temp_api_tunnel_path;

        public TMAPage()
        {
            InitializeComponent();
            ClienterClass ClienterClass = new ClienterClass();
            directoryPath = ClienterClass.DirectoryPath();
            cflPath = ClienterClass.CFLPath();
            frpPath = ClienterClass.FrpPath();
            frpIniPath = ClienterClass.FrpIniPath();
            frpExePath = ClienterClass.FrpExePath();
            setupIniPath = ClienterClass.SetupIniPath();
            temp_path = ClienterClass.Temp_path();
            temp_api_path = ClienterClass.Temp_api_path();
            pictures_path = ClienterClass.Pictures_path();
            InitializesetPaths();
            NodeTextBlock_1_3_2.IsReadOnly = true;
        }

        public void InitializesetPaths()
        {
            string jsonContent = System.IO.File.ReadAllText(temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);
            string url = "http://cf-v2.uapis.cn/tunnel?token=" + jsonObject["data"]["usertoken"]?.ToString();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    temp_api_tunnel_path = System.IO.Path.Combine(temp_path, "Chmlfrp_tunnel_api.json");
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    File.WriteAllText(temp_api_tunnel_path, webClient.DownloadString(url));
                }
                catch
                {
                    return;
                }
            }
            jsonContent = System.IO.File.ReadAllText(temp_api_tunnel_path);
            jsonObject = JObject.Parse(jsonContent);
            if(jsonObject["msg"]?.ToString() == "获取隧道数据成功")
            {
                foreach (var tunnel in jsonObject["data"])
                {
                    i++;
                    if (i == 1)
                    {
                        NodeTextBlock_1_1.Text = tunnel["name"]?.ToString();
                        NodeTextBlock_1_2.Text = tunnel["id"]?.ToString();
                        string state = tunnel["state"]?.ToString();
                        if (state == "true") NodeTextBlock_1_3_2.Text = "在线";
                        if (state == "false") NodeTextBlock_1_3_2.Text = "离线"; NodeTextBlock_1_3_1.Background = new SolidColorBrush(Colors.LightYellow); NodeTextBlock_1_3_2.Foreground = new SolidColorBrush(Colors.Yellow);
                    }

                    i = 0;
                }
            } else
            {
                return;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            InitializesetPaths();
        }
    }
}
