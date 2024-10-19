using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChmlFrpLauncher_cs.pages
{
    /// <summary>
    /// Launcher.xaml 的交互逻辑
    /// </summary>
    public partial class Launcher : Page
    {
        public Launcher()
        {

            InitializeComponent();
        }

        private void Launch(object sender, RoutedEventArgs e)
        {
            string command = "frpc >.logs 2>&1";

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            using (Process process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
            }
        }
    }
}
