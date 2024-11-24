using System.IO;
using System.Windows.Controls;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Windows.Media.Imaging;
using System.Net;

namespace ChmlFrpLauncher_cs.Pages.ChmlFrp
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
        }
    }
}
