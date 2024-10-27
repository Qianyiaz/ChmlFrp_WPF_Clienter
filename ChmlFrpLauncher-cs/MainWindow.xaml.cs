using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Text.Json;


namespace ChmlFrpLauncher_cs
{
    public partial class MainWindow : Window
    {
        string directoryPath = Directory.GetCurrentDirectory();
        int NumFiles = 0;

        public MainWindow()
        {
            InitializeComponent();
            string CFL = Path.Combine(directoryPath, "CFL");
            string frp = Path.Combine(CFL, "frp"); 
            string frpc = Path.Combine(frp, "frpc.exe");
            if (!File.Exists(frp) && !File.Exists(frp))
            {
                Directory.CreateDirectory(CFL); Directory.CreateDirectory(frp);
            }
            if (!File.Exists(frpc))
            {
                MessageBox.Show("要想启动frp需安装frpc", "注意frpc尚未安装", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            string frp_ini = Path.Combine(frp, "frpc.ini");
            if (File.Exists(frp_ini))
            {
                string ini = File.ReadAllText(frp_ini);
                if (ini != null)
                {
                    TextBox1.Text = ini;
                }
            }
        }

        private void Launch(object sender, RoutedEventArgs e)
        {
            directoryPath = Directory.GetCurrentDirectory();
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

            NumFiles = NumFiles + 1;

            string command = frp + " -c " + frp_ini + " >%cd%/CFL/" + NumFiles + ".logs 2>&1";

            if (NumFiles == 5)
            {
                NumFiles = 0;
            }

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
        }

        private void TextBox_ini(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string folderPath = Path.Combine(directoryPath, "CFL");
            folderPath=Path.Combine(folderPath, "frp");
            string frp_ini = Path.Combine(folderPath, "frpc.ini");
            string Text = TextBox1.Text;
            TextBox1.Text = Text;

            using (StreamWriter writer = new StreamWriter(frp_ini, false)) 
            {
                writer.Write(Text);
            }
        }
    }
}