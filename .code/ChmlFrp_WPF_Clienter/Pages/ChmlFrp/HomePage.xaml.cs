﻿using System.IO;
using System.Windows.Controls;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Windows.Media.Imaging;
using System.Net;
using System.Windows;

namespace ChmlFrp_WPF_Clienter.Pages.ChmlFrp
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        string directoryPath = Directory.GetCurrentDirectory();
        public HomePage()
        {
            InitializeComponent();
            directoryPath = Directory.GetCurrentDirectory();

            string CFL = Path.Combine(directoryPath, "CFL");
            string temp_path = Path.Combine(CFL, "temp");
            string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
            string jsonContent = File.ReadAllText(temp_api);
            var parsedJson = JToken.Parse(jsonContent);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            Console.WriteLine(formattedJson);
            var jsonObject = JObject.Parse(jsonContent);
            string url = jsonObject["data"]["userimg"]?.ToString();
            string temp_UserImage = Path.Combine(temp_path, "temp_UserImage.png");
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), temp_UserImage);
                }
            }
            catch
            {
            }
            UserImage.ImageSource = new BitmapImage(new Uri(temp_UserImage));
            UserTextBlock.Text=UserTextBlock.Text + jsonObject["data"]["username"]?.ToString();
            Usermailbox.Text = jsonObject["data"]["email"]?.ToString();
            UserRegistration_time.Text = jsonObject["data"]["regtime"]?.ToString();
            UserQQ.Text = jsonObject["data"]["qq"]?.ToString();
            Userright.Text = jsonObject["data"]["usergroup"]?.ToString();
            UserExpiration_time.Text = jsonObject["data"]["term"]?.ToString();
            UserReal_name_status.Text = jsonObject["data"]["realname"]?.ToString();
            UserPoints_remaining.Text = jsonObject["data"]["integral"]?.ToString();
            UserTunnel_restrictions.Text = jsonObject["data"]["tunnelCount"]?.ToString() + " / " + jsonObject["data"]["tunnel"]?.ToString();
            UserBandwidth_throttling.Text = "国内" + jsonObject["data"]["bandwidth"]?.ToString() + "m";
        }

        private void TokenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Token.Click -= TokenClick;
            string CFL = Path.Combine(directoryPath, "CFL");
            string temp_path = Path.Combine(CFL, "temp");
            string temp_api = Path.Combine(temp_path, "Chmlfrp_api.json");
            string jsonContent = File.ReadAllText(temp_api);
            var jsonObject = JObject.Parse(jsonContent);
            if (Token.Content.ToString() == jsonObject["data"]["usertoken"]?.ToString())
            {
                Clipboard.SetDataObject(Token.Content.ToString());
                Token.Content = "token已复制的剪切板点击重新显示";
                Token.Click += TokenClick;
                return;
            }
            Token.Content = jsonObject["data"]["usertoken"]?.ToString();
            Token.Click += TokenClick;
        }
    }
}
