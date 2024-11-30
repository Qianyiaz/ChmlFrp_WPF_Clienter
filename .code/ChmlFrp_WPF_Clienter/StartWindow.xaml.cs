using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using IniParser;
using IniParser.Model;

namespace ChmlFrp_WPF_Clienter
{
    public partial class StartWindow : Window
    {
        private Timer timer;

        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }

        public StartWindow()
        {
            InitializeComponent();
            if (IsProcessRunning("ChmlFrpLauncher", 2)) Close(); //检测到有两个ChmlFrpLauncher就退出
            //进入 2 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //创建路径函数
                string directoryPath = Directory.GetCurrentDirectory(); string CWC = Path.Combine(directoryPath, "CWC"); string page_image = Path.Combine(CWC, "pictures"); string frp = Path.Combine(CWC, "frp"); string temp = Path.Combine(CWC, "temp"); string frpc = Path.Combine(frp, "frpc.exe"); string ini = Path.Combine(CWC, "Setup.ini");
                //创建ini实例
                var parser = new FileIniDataParser();
                IniData data;
                //检测是否有相关配置文件
                if (!File.Exists(CWC) && !File.Exists(frp) && !File.Exists(ini) && !File.Exists(page_image) && !File.Exists(temp))
                {
                    Directory.CreateDirectory(CWC); Directory.CreateDirectory(frp); Directory.CreateDirectory(page_image); Directory.CreateDirectory(temp); //创建文件夹
                    data = new IniData();
                    data["ChmlFrp_WPF_Clienter Setup"]["Versions"] = "0.0.0.0.3";
                    parser.WriteFile(ini, data);
                }
                for (int i = 1; i < 6; i++)
                {
                    if (!File.Exists(Path.Combine(CWC, i + ".logs")))
                    {
                        File.Create(Path.Combine(CWC, i + ".logs")); //创建logs日志文件
                    }
                }
                //界面退出，弹出MainWindow。
                MainWindow MainWindow = new MainWindow();
                Window window = Window.GetWindow(this);
                window.Close();
                MainWindow.Show();
            });
        }
    }
}

