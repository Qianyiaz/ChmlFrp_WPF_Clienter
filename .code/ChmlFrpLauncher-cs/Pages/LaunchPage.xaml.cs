using IniParser;
using IniParser.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

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
            //创建路径函数
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string frp_path = Path.Combine(CFL, "frp"); string frp_ini = Path.Combine(frp_path, "frpc.ini"); string frp = Path.Combine(frp_path, "frpc.exe"); string ini = Path.Combine(CFL, "Setup.ini");
            if (IsProcessRunning("frpc", 1))
            {
                LaunchButton.Content = "点击关闭 frpc";
                LaunchButton.Click -= Launch;
                LaunchButton.Click += Killfrp;
                return;
            }
            if (File.Exists(frp_ini) && File.Exists(frp))
            {
                LaunchButton.Content = "启动 frpc";
            }
        }
        string directoryPath = Directory.GetCurrentDirectory();
        int i = 0;

        private void Launch(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Launch;
            //创建路径函数
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string frp_path = Path.Combine(CFL, "frp"); string frp_ini = Path.Combine(frp_path, "frpc.ini"); string frp = Path.Combine(frp_path, "frpc.exe"); string ini = Path.Combine(CFL, "Setup.ini");
            if (!File.Exists(frp_ini) && !File.Exists(frp))
            {
                LaunchButton.Content = "未找到配置文件";
                return;
            }
            //创建ini实例
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(ini);
            data = parser.ReadFile(ini);
            LaunchButton.Content = "正在启动中...";
            if (i == 5) { i = 0; }
            i++; string logs = Path.Combine(CFL, i + ".logs");
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + frp + " -c " + frp_ini + " >" + logs + " 2>&1")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
                LaunchButton.Content = "点击关闭 frpc";
                LaunchButton.Click += Killfrp;
            }
            //kongzitaNavigation.Navigate(new System.Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }
        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }
        private void Killfrp(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Killfrp;
            if (!IsProcessRunning("frpc", 0))
            {
                LaunchButton.Content = "启动 frpc";
                LaunchButton.Click += Launch;
                return;
            }
            string name = "frpc";
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
            LaunchButton.Content = "启动 frpc";
            LaunchButton.Click += Launch;
        }
    }
}
