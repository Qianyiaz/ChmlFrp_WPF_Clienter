using System.IO;
using System.Text.Json;
using System.Windows;


namespace ChmlFrpLauncher_cs
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string directoryPath = Directory.GetCurrentDirectory();
            string folderPath = System.IO.Path.Combine(directoryPath, "CFL");

            Directory.CreateDirectory(folderPath); folderPath = System.IO.Path.Combine(folderPath, "frp"); Directory.CreateDirectory(folderPath);

        }
    }
}