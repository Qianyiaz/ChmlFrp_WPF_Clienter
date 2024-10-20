using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Xml.Linq;


namespace ChmlFrpLauncher_cs
{
    public partial class MainWindow : Window
    {
        string directoryPath = Directory.GetCurrentDirectory();

        public MainWindow()
        {
            string folderPath = Path.Combine(directoryPath, "CFL");

            Directory.CreateDirectory(folderPath); folderPath = Path.Combine(folderPath, "frp"); Directory.CreateDirectory(folderPath);

            folderPath = Path.Combine(folderPath, "frpc.exe");

            if (!File.Exists(folderPath))
            {
                MessageBox.Show("要想启动frp需安装frpc", "注意frpc尚未安装", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Launch(object sender, RoutedEventArgs e)
        {
            string folderPath = Path.Combine(directoryPath, "CFL");
            folderPath = Path.Combine(folderPath, "frp");
            string frp_ini = Path.Combine(folderPath, "frpc.ini");
            string frp = Path.Combine(folderPath, "frpc.exe");
            if (!File.Exists(frp_ini))
            {
                MessageBox.Show("要想启动frp配置frpc.ini", "注意 未配置ini", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!File.Exists(frp))
            {
                MessageBox.Show("要想启动frp需安装frpc", "注意frpc尚未安装", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string command = frp + " -c " + frp_ini + " >%cd%/CFL/.logs 2>&1";

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
        }

        private void Killfrp(object sender, RoutedEventArgs e)
        {
            string name = "frpc.exe";
            Process[] process = Process.GetProcesses();
            foreach (Process p1 in process)
            {

                string processName = p1.ProcessName.ToLower().Trim();
                if (processName == name)
                {
                    p1.Kill();
                }
            }
        }
    }
}