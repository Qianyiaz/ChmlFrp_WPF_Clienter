using System.IO;
using System.Windows;

namespace ChmlFrpLauncher_cs
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string directoryPath = Directory.GetCurrentDirectory();
            string folderPath = System.IO.Path.Combine(directoryPath, "CFL");

            if  (Directory.Exists(folderPath))
            {
                
            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}