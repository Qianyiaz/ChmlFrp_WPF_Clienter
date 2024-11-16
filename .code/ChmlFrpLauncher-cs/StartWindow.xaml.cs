using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using IniParser;
using IniParser.Model;

namespace ChmlFrpLauncher_cs
{
    public partial class StartWindow : Window
    {
        private Timer timer;

        static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        public StartWindow()
        {
            InitializeComponent();
            if (IsProcessRunning("ChmlFrpLauncher"))
            {
                Close();
            }
            //进入 2 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(2), Timeout.InfiniteTimeSpan);
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            { 
                start:
                string directoryPath = Directory.GetCurrentDirectory();
                string CFL = Path.Combine(directoryPath, "CFL");
                string frp = Path.Combine(CFL, "frp");
                string temp = Path.Combine(CFL, "temp");
                string frpc = Path.Combine(frp, "frpc.exe");
                string ini = Path.Combine(CFL, "Setup.ini");
                var parser = new FileIniDataParser();
                IniData data;

                for(int i = 1; i < 6; i++)
                {
                    File.Create(Path.Combine(CFL,  i+".logs"));
                }

                if (!File.Exists(CFL) && !File.Exists(frp) && !File.Exists(ini) && !File.Exists(temp))
                {
                    Directory.CreateDirectory(CFL); Directory.CreateDirectory(frp); Directory.CreateDirectory(temp);

                    data = new IniData();
                    data["ChmlFrpLauncher_cs Setup"]["Versions"] = "0.0.0.0.3";
                    data["ChmlFrpLauncher_cs Setup"]["number"] = "0";
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
                try
                {
                    data = parser.ReadFile(ini);
                    string N = data["ChmlFrpLauncher_cs Setup"]["number"];
                }
                catch
                {
                    File.Delete(ini);
                    goto start;
                }

                //程序退出，弹出MainWindow。
                MainWindow mainWindow = new MainWindow();
                Window window = Window.GetWindow(this);
                window.Close();
                mainWindow.Show();
            });
        }
    }
}

