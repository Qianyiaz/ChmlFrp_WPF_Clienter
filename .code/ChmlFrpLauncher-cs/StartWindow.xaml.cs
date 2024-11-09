using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using static ChmlFrpLauncher_cs.StartWindow;

namespace ChmlFrpLauncher_cs
{
    public partial class StartWindow : Window
    {
        public class Person
        {
            public string Versions { get; set; }
        }

        private Timer timer;

        public StartWindow()
        {
            InitializeComponent();

            string directoryPath = Directory.GetCurrentDirectory();
            string CFL = Path.Combine(directoryPath, "CFL");
            string frp = Path.Combine(CFL, "frp");
            string frpc = Path.Combine(frp, "frpc.exe");
            string json = Path.Combine(CFL, "CFL.config");
            Person person = new Person
            {
                Versions = "0.0.0.3",
            };
            if (!File.Exists(CFL) && !File.Exists(frp) && !File.Exists(json))
            {
                Directory.CreateDirectory(CFL); Directory.CreateDirectory(frp);
                File.WriteAllText(json, JsonConvert.SerializeObject(person, Formatting.Indented));
            }
            //进入 2 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(2), Timeout.InfiniteTimeSpan);
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //程序退出，弹出MainWindow。
                MainWindow mainWindow = new MainWindow();
                Window window = Window.GetWindow(this);
                window.Close();
                mainWindow.Show();
            });
        }
    }
}

