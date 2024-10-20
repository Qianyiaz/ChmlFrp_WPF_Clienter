using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ChmlFrpLauncher_cs.pages
{
    /// <summary>
    /// Launcher.xaml 的交互逻辑
    /// </summary>
    public partial class Launcher : Page
    {
        private void Launch(object sender, RoutedEventArgs e)
        {
            string command = "frpc >%cd%/CFL/.logs 2>&1";

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
