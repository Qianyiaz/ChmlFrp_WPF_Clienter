using IniParser;
using IniParser.Model;
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
            if (IsProcessRunning("frpc", 5))
            {
                LaunchButton.Content = "启动已达上线";
                return;
            }
            if (File.Exists(frp_ini) && File.Exists(frp))
            {
                LaunchButton.Content = "启动 frpc";
                return;
            }
        }
        string directoryPath = Directory.GetCurrentDirectory();
        int i = 0;

        private void Launch(object sender, RoutedEventArgs e)
        {
            //创建路径函数
            directoryPath = Directory.GetCurrentDirectory(); string CFL = Path.Combine(directoryPath, "CFL"); string frp_path = Path.Combine(CFL, "frp"); string frp_ini = Path.Combine(frp_path, "frpc.ini"); string frp = Path.Combine(frp_path, "frpc.exe"); string ini = Path.Combine(CFL, "Setup.ini");
            //创建ini实例
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(ini);
            data = parser.ReadFile(ini);
            LaunchButton.Content = "正在启动中...";
            i++; string logs = Path.Combine(CFL, i + ".logs");
            if (IsProcessRunning("frpc", 5))
            {
                LaunchButton.Content = "启动已达上线";
                return;
            }
            if (i == 6)
            {
                LaunchButton.Content = "启动已达上线";
                return;
            }
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
                LaunchButton.Content = "启动成功";
            }
            //kongzitaNavigation.Navigate(new System.Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }
        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }
    }
}
