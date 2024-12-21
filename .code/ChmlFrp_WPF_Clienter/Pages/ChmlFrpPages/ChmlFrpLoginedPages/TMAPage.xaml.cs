﻿using System.Net.Http;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_WPF_Clienter.Pages.ChmlFrpLoginPages
{
    /// <summary>
    /// TMAPage.xaml 的交互逻辑
    /// </summary>
    public partial class TMAPage : Page
    {
        private int i;
        private int x;
        public string temp_api_tunnel_path;
        private ClienterClass clienterClass;

        public TMAPage()
        {
            InitializeComponent();
            clienterClass = new ClienterClass();
            InitializesetPaths();
            NodeTextBlock_1_3_2.IsReadOnly = true;
            Leftbutton.Click -= btnLeft_Click;
            Leftbutton.BorderBrush = Brushes.Gray;
            Rightbutton.Click -= btnRight_Click;
            Rightbutton.BorderBrush = Brushes.Gray;
        }

        public void InitializesetPaths()
        {
            string jsonContent = System.IO.File.ReadAllText(clienterClass.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);
            string url = "http://cf-v2.uapis.cn/tunnel?token=" + jsonObject["data"]["usertoken"]?.ToString();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    temp_api_tunnel_path = System.IO.Path.Combine(clienterClass.temp_path, "Chmlfrp_tunnel_api.json");
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
            if (jsonObject["msg"]?.ToString() == "获取隧道数据成功")
            {
                foreach (var tunnel in jsonObject["data"])
                {
                    i++;
                    if (i == 1)
                    {
                        NodeTextBlock_1_1.Text = tunnel["name"]?.ToString();
                        NodeTextBlock_1_2.Text = tunnel["id"]?.ToString();
                        string state = tunnel["state"]?.ToString();
                        if (state == "false")
                        {
                            NodeTextBlock_1_3_2.Text = "离线";
                        }
                        if (state == "true")
                        {
                            NodeTextBlock_1_3_2.Text = "在线";
                        }
                        NodeTextBlock_1_4_2.Text = tunnel["node"]?.ToString();
                        NodeTextBlock_1_5_2.Text = tunnel["localip"]?.ToString() + " - " + tunnel["type"]?.ToString();
                    }
                    if (i == 2)
                    {
                        NodeTextBlock_2_1.Text = tunnel["name"]?.ToString();
                        NodeTextBlock_2_2.Text = tunnel["id"]?.ToString();
                        string state = tunnel["state"]?.ToString();
                        if (state == "false")
                        {
                            NodeTextBlock_2_3_2.Text = "离线";
                        }
                        NodeTextBlock_2_4_2.Text = tunnel["node"]?.ToString();
                        if (state == "true")
                        {
                            NodeTextBlock_2_3_2.Text = "在线";
                        }
                        NodeTextBlock_2_5_2.Text = tunnel["localip"]?.ToString() + " - " + tunnel["type"]?.ToString();
                    }
                    if (i == 3)
                    {
                        NodeTextBlock_3_1.Text = tunnel["name"]?.ToString();
                        NodeTextBlock_3_2.Text = tunnel["id"]?.ToString();
                        string state = tunnel["state"]?.ToString();
                        if (state == "false")
                        {
                            NodeTextBlock_3_3_2.Text = "离线";
                        }
                        if (state == "true")
                        {
                            NodeTextBlock_3_3_2.Text = "在线";
                        }
                        NodeTextBlock_3_4_2.Text = tunnel["node"]?.ToString();
                        NodeTextBlock_3_5_2.Text = tunnel["localip"]?.ToString() + " - " + tunnel["type"]?.ToString();
                    }
                    if (i == 4)
                    {
                        NodeTextBlock_4_1.Text = tunnel["name"]?.ToString();
                        NodeTextBlock_4_2.Text = tunnel["id"]?.ToString();
                        string state = tunnel["state"]?.ToString();
                        if (state == "false")
                        {
                            NodeTextBlock_2_3_2.Text = "离线";
                        }
                        if (state == "true")
                        {
                            NodeTextBlock_2_3_2.Text = "在线";
                        }
                        NodeTextBlock_4_4_2.Text = tunnel["node"]?.ToString();
                        NodeTextBlock_4_5_2.Text = tunnel["localip"]?.ToString() + " - " + tunnel["type"]?.ToString();
                    }
                    if (i >= 5)
                    {
                        Rightbutton.Click += btnRight_Click;
                        Rightbutton.Background = Brushes.Blue;
                    }
                }
                if (i < +x + 1)
                {
                    RemoveBorderName("Border_name1");
                    RemoveBorderName("Border_name2");
                    RemoveBorderName("Border_name3");
                    RemoveBorderName("Border_name4");
                }
                if (i < +x + 2)
                {
                    RemoveBorderName("Border_name2");
                    RemoveBorderName("Border_name3");
                    RemoveBorderName("Border_name4");
                }
                if (i < +x + 3)
                {
                    RemoveBorderName("Border_name3");
                    RemoveBorderName("Border_name4");
                }
                if (i < +x + 4)
                {
                    RemoveBorderName("Border_name4");
                }
            }
            else
            {
                return;
            }
            i = 0;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            InitializesetPaths();
        }

        private void RemoveBorderName(string name)
        {
            if (this.FindName(name) is Border border)
            {
                if (border.Parent is Grid parentGrid)
                {
                    parentGrid.Children.Remove(border);
                }
            }
        }

        private void RestoreBorderName(string name, int row, int column)
        {
            if (this.FindName(name) is Border border)
            {
                if (border.Parent == null)
                {
                    if (name == "Border_name3" || name == "Border_name4")
                    {
                        Grid.SetRow(border, row);
                        Grid.SetColumn(border, column);
                        ParentGrid2.Children.Add(border);
                        return;
                    }
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, column);
                    ParentGrid1.Children.Add(border);
                }
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            x -= 4;
            Leftbutton.Click -= btnLeft_Click;
            Rightbutton.Click += btnRight_Click;
            RestoreBorderName("Border_name1", 0, 0);
            RestoreBorderName("Border_name2", 0, 1);
            RestoreBorderName("Border_name3", 1, 0);
            RestoreBorderName("Border_name4", 1, 1);
            InitializesetPaths();
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            x += 4;
            Leftbutton.Click += btnLeft_Click;
            Rightbutton.Click -= btnRight_Click;
            RestoreBorderName("Border_name1", 0, 0);
            RestoreBorderName("Border_name2", 0, 1);
            RestoreBorderName("Border_name3", 1, 0);
            RestoreBorderName("Border_name4", 1, 1);
            InitializesetPaths();
        }
    }
}
