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
        //private string directoryPath;
        private string frpPath;
        //private string frpIniPath;
        //private string frpExePath;
        private string setupIniPath;
        private string temp_path;
        //private string temp_api_path;
        private string cflPath;
        private string pictures_path;
        private Timer timer;

        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }

        public StartWindow()
        {
            InitializeComponent();
            ClienterClass ClienterClass = new ClienterClass();
            //directoryPath = ClienterClass.DirectoryPath();
            cflPath = ClienterClass.CFLPath();
            frpPath = ClienterClass.FrpPath();
            //frpIniPath = ClienterClass.FrpIniPath();
            //frpExePath = ClienterClass.FrpExePath();
            setupIniPath = ClienterClass.SetupIniPath();
            temp_path = ClienterClass.Temp_path();
            //temp_api_path = ClienterClass.Temp_api_path();
            pictures_path = ClienterClass.Pictures_path();
            if (IsProcessRunning("ChmlFrpLauncher", 2)) Close(); //检测到有两个ChmlFrpLauncher就退出
            //进入 2 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //创建ini实例
                var parser = new FileIniDataParser();
                IniData data;
                //检测是否有相关配置文件
                if (!File.Exists(cflPath) && !File.Exists(frpPath) && !File.Exists(setupIniPath) && !File.Exists(pictures_path) && !File.Exists(temp_path))
                {
                    Directory.CreateDirectory(cflPath); Directory.CreateDirectory(frpPath); Directory.CreateDirectory(pictures_path); Directory.CreateDirectory(temp_path); //创建文件夹
                    data = new IniData();
                    data["ChmlFrp_WPF_Clienter Setup"]["Versions"] = "0.0.0.0.3";
                    parser.WriteFile(setupIniPath, data);
                }
                for (int i = 1; i < 6; i++)
                {
                    if (!File.Exists(Path.Combine(cflPath, i + ".logs")))
                    {
                        File.Create(Path.Combine(cflPath, i + ".logs")); //创建logs日志文件
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

