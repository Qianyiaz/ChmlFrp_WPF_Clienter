using System;
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

        public StartWindow()
        {
            InitializeComponent();
            //进入 2 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(2), Timeout.InfiniteTimeSpan);
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                string directoryPath = Directory.GetCurrentDirectory();
                string CFL = Path.Combine(directoryPath, "CFL");
                string frp = Path.Combine(CFL, "frp");
                string frpc = Path.Combine(frp, "frpc.exe");
                string ini = Path.Combine(CFL, "Setup.ini");

                if (!File.Exists(CFL) && !File.Exists(frp) && !File.Exists(ini))
                {
                    Directory.CreateDirectory(CFL); Directory.CreateDirectory(frp);

                    var parser = new FileIniDataParser();
                    IniData data;
                    data = new IniData();
                    data["ChmlFrpLauncher_cs Setup"]["Versions"] = "0.0.0.3";
                    parser.WriteFile(ini, data);
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

