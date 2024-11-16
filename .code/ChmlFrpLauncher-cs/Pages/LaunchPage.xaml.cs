using System.Diagnostics;
using System.IO;
using Path = System.IO.Path;
using System.Windows;
using System.Windows.Controls;
using IniParser;
using IniParser.Model;
using System.Windows.Markup;
using System;

namespace ChmlFrpLauncher_cs.Pages
{
    /// <summary>
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            directoryPath = Directory.GetCurrentDirectory();
            string folderPath = Path.Combine(directoryPath, "CFL");
            folderPath = Path.Combine(folderPath, "frp");
            string frp = Path.Combine(folderPath, "frpc.exe");
            LaunchButton.Content = " 启动 frpc";
            LaunchButton.FontSize = 15;
        }
        string directoryPath = Directory.GetCurrentDirectory();
        int i = 0;

        private void Launch(object sender, RoutedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory();
            string CFL = Path.Combine(directoryPath, "CFL");
            string frp_path = Path.Combine(CFL, "frp");
            string frp_ini = Path.Combine(frp_path, "frpc.ini");
            string frp = Path.Combine(frp_path , "frpc.exe");
            string ini = Path.Combine(CFL, "Setup.ini");
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(ini);
            data = parser.ReadFile(ini);
            string i_parser = data["ChmlFrpLauncher_cs Setup"]["number"];
            if (i_parser == "6")
            {
                LaunchButton.Click -= Launch;
                LaunchButton.Content = "启动已达上线";
                return;
            }
            if (i_parser == "6")
            {
                LaunchButton.Click -= Launch;
                LaunchButton.Content = "启动已达上线";
                return;
            }
            LaunchButton.Content = "正在检测是否存在frpc.ini";
            if (!File.Exists(frp_ini))
            {
                LaunchButton.Content = "要想启动frp配置frpc.ini\n  注意 未配置ini";
                return;
            }
            if (!File.Exists(frp))
            {
                LaunchButton.Content = "要想启动frp需安装frpc\n  注意frpc尚未安装";
                return;
            }
            LaunchButton.Content = "正在启动中...";
            i++; string logs = Path.Combine(CFL, i+".logs"); string command = frp + " -c " + frp_ini + " >" + logs + " 2>&1";
            string i_string = i.ToString();
            data["ChmlFrpLauncher_cs Setup"]["number"] = i_string;
            parser.WriteFile(ini, data);
            data = parser.ReadFile(ini);
            string i_parser_1 = data["ChmlFrpLauncher_cs Setup"]["number"];
            if (i_parser_1 == "6")
            {
                LaunchButton.Click -= Launch;
                LaunchButton.Content = "启动已达上线";
                return;
            }
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
            }
            LaunchButton.Content = "启动成功";
            //if (IsProcessRunning("frpc"))
            //{
            //    //MessageBox.Show("frpc已启动", "", MessageBoxButton.OK);
            //}

            //kongzitaNavigation.Navigate(new System.Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }
        static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }
    }
}
