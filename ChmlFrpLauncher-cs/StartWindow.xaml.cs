using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace ChmlFrpLauncher_cs
{
    public partial class StartWindow : Window
    {
        public class Person
        {
            public string Versions { get; set; }
            public int Counter { get; set; }
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
            if (!File.Exists(CFL) && !File.Exists(frp) && !File.Exists(frp))
            {
                Directory.CreateDirectory(CFL); Directory.CreateDirectory(frp);

                Person person = new Person
                {
                    Versions = "0.0.0.3",
                    Counter = 0,
                };

                string json_1 = JsonConvert.SerializeObject(person, Formatting.Indented);
                File.WriteAllText(json, json_1);
            }
            //进入 3 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);
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

