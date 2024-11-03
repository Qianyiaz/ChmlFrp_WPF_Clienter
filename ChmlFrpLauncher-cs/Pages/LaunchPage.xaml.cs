using System.Diagnostics;
using System.IO;
using Path = System.IO.Path;
using System.Windows;
using System.Windows.Controls;
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
            string folderPath = System.IO.Path.Combine(directoryPath, "CFL");
            folderPath = Path.Combine(folderPath, "frp");
            string frp = Path.Combine(folderPath, "frpc.exe");

            LaunchButton.Content = " 启动 frpc";
            LaunchButton.FontSize = 15;


        }
        string directoryPath = Directory.GetCurrentDirectory();

        private void Launch(object sender, RoutedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory();
            string folderPath = Path.Combine(directoryPath, "CFL");
            folderPath = Path.Combine(folderPath, "frp");
            string frp_ini = Path.Combine(folderPath, "frpc.ini");
            string frp = Path.Combine(folderPath, "frpc.exe");
            LaunchButton.Content = "正在检测是否存在frpc.ini";
            if (!File.Exists(frp_ini))
            {
                LaunchButton.Content = "要想启动frp配置frpc.ini\n  注意 未配置ini";
                return;
            }
            LaunchButton.Content = "正在检测是否存在frpc.exe";
            if (!File.Exists(frp))
            {
                LaunchButton.Content = "要想启动frp需安装frpc\n  注意frpc尚未安装";
                return;
            }
            LaunchButton.Content = "检测成功";


            string command = frp + " -c " + frp_ini + " >%cd%/CFL/" + ".logs 2>&1";


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

            string processName = "frpc";

            if (IsProcessRunning(processName))
            {
                MessageBox.Show("frpc已启动", "", MessageBoxButton.OK);
            }
            //kongzitaNavigation.Navigate(new System.Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

    }
}
