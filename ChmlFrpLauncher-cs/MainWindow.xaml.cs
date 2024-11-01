using System;
using System.Collections.Generic;
using System.IO;
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
using UIKitTutorials.Pages;
using Path = System.IO.Path;

namespace UIKitTutorials
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PagesNavigation.Navigate(new Uri("Pages/LaunchPage.xaml", UriKind.RelativeOrAbsolute));
            string directoryPath = Directory.GetCurrentDirectory();
            string CFL = Path.Combine(directoryPath, "CFL");
            string frp = Path.Combine(CFL, "frp");
            string frpc = Path.Combine(frp, "frpc.exe");
            if (!File.Exists(CFL) && !File.Exists(frp))
            {
                Directory.CreateDirectory(CFL); Directory.CreateDirectory(frp);
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
            PagesNavigation.Navigate(new Uri("Pages/LaunchPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void rdNotes_Click(object sender, RoutedEventArgs e)
        {
            //PagesNavigation.Navigate(new System.Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
