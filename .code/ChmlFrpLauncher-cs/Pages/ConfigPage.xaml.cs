using System.Windows;
using System.Windows.Controls;
using System;
using System.IO;
using IniParser;
using IniParser.Model;
using Newtonsoft;
using System.Net;
using Newtonsoft.Json;

namespace ChmlFrpLauncher_cs.Pages
{
    /// <summary>
    /// Lógica de interacción para NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        public class Person
        {
            public string tag_name { get; set; }
        }

        public NotesPage()
        {
            InitializeComponent();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            start:
            string directoryPath = Directory.GetCurrentDirectory();
            string CFL = Path.Combine(directoryPath, "CFL");
            string ini = Path.Combine(CFL, "Setup.ini");
            string temp = Path.Combine(CFL, "temp");
            string Github_json = Path.Combine(temp, "Github.json");
            var parser = new FileIniDataParser(); IniData data;
            if (!File.Exists(ini))
            {
                data = new IniData();
                data["ChmlFrpLauncher_cs Setup"]["Versions"] = "0.0.0.3";
                parser.WriteFile(ini, data);
            }
            try
            {
                data = parser.ReadFile(ini);
                string V = data["ChmlFrpLauncher_cs Setup"]["Versions"];
            }
            catch
            {
                File.Delete(ini);
                goto start;
            }
            data = parser.ReadFile(ini);
            string Versions = data["ChmlFrpLauncher_cs Setup"]["Versions"];
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.DownloadFile("https://raw.githubusercontent.com/Qianyiaz/ChmlFrpLauncher_cs/refs/heads/api/DownloadLink.json", Github_json);
            }
            catch
            {
                //File.Delete(Github_json);
                return;
            }
            string json = File.ReadAllText(Github_json);
            var jsonObject = JsonConvert.DeserializeObject<Person>(json);
            Console.WriteLine(jsonObject.tag_name);
        }
    }
}
