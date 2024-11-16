using System.Windows;
using System.Windows.Controls;
using System;
using System.IO;
using IniParser;
using IniParser.Model;
using Newtonsoft;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using File = System.IO.File;
using System.Security.Cryptography;

namespace ChmlFrpLauncher_cs.Pages
{
    /// <summary>
    /// Lógica de interacción para NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        public class Person
        {
            public string latestVersion { get; set; }
            public string DownloadLink_zh { get; set; }
            public string DownloadLink_en { get; set; }
        }

        public NotesPage()
        {
            InitializeComponent();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            //start:
            //string directoryPath = Directory.GetCurrentDirectory();
            //string CFL = Path.Combine(directoryPath, "CFL");
            //string ini = Path.Combine(CFL, "Setup.ini");
            //string temp = Path.Combine(CFL, "temp");
            //string Github_json = Path.Combine(temp, "Github.json");
            //var parser = new FileIniDataParser(); IniData data;
            //if (!File.Exists(ini))
            //{
            //    data = new IniData();
            //    data["ChmlFrpLauncher_cs Setup"]["Versions"] = "0.0.0.3";
            //    parser.WriteFile(ini, data);
            //}
            //try
            //{
            //    data = parser.ReadFile(ini);
            //    string V = data["ChmlFrpLauncher_cs Setup"]["Versions"];
            //}
            //catch
            //{
            //    File.Delete(ini);
            //    goto start;
            //}
            //try
            //{
            //    WebClient client = new WebClient();
            //    client.Credentials = CredentialCache.DefaultCredentials;
            //    client.DownloadFile("https://raw.githubusercontent.com/Qianyiaz/ChmlFrpLauncher_cs/refs/heads/api/DownloadLink.json", Github_json);
            //}
            //catch
            //{
            //    //File.Delete(Github_json);
            //    return;
            //}
            //data = parser.ReadFile(ini);
            //string nowV = data["ChmlFrpLauncher_cs Setup"]["Versions"];
            //string json = File.ReadAllText(Github_json);
            //var jsonObject = JsonConvert.DeserializeObject<Person>(json);
            //string exePath = Assembly.GetExecutingAssembly().Location;
            //string exeName = Path.GetFileName(exePath);
            //string exe = Path.Combine(temp, exeName);
            //if (jsonObject.latestVersion == nowV)
            //{
            //    try
            //    {
            //        WebClient client = new WebClient();
            //        client.Credentials = CredentialCache.DefaultCredentials;
            //        client.DownloadFile(jsonObject.DownloadLink_zh, exe);
            //        if (File.Exists(Path.Combine(temp, exeName)))
            //        {
            //            string nowexe = Path.Combine(directoryPath, exeName);
            //            string command = "del / f / q " + nowexe + "&& move " + exe + " %cd%";
            //            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            //            {
            //                RedirectStandardOutput = true,
            //                UseShellExecute = false,
            //                CreateNoWindow = true
            //            };
            //            using (Process process = new Process())
            //            {
            //                process.StartInfo = processInfo;
            //                process.Start();
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        File.Delete(exe);
            //        return;
            //    }
            //}
        }
    }
}
