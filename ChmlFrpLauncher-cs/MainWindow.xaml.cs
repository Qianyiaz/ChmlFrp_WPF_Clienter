using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using ChmlFrpLauncher_cs.Pages;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace ChmlFrpLauncher_cs
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Person
        {
            public string Versions { get; set; }
            public int Counter { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            PagesNavigation.Navigate(new Uri("Pages/LaunchPage.xaml", UriKind.RelativeOrAbsolute));
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
                    Versions = "0.0.0.2",
                    Counter = 0,
                };

                string json_1 = JsonConvert.SerializeObject(person, Formatting.Indented);
                File.WriteAllText(json, json_1);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
            rdNotes.IsChecked = false;
            PagesNavigation.Navigate(new Uri("Pages/LaunchPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void rdNotes_Click(object sender, RoutedEventArgs e)
        {
            rdHome.IsChecked = false;
            PagesNavigation.Navigate(new System.Uri("Pages/ConfigPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
