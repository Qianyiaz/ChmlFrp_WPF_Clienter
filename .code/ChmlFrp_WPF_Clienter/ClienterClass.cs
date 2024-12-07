using System.IO;

namespace ChmlFrp_WPF_Clienter
{
    internal class ClienterClass
    {
        private string directoryPath;
        private string frpPath;
        private string frpIniPath;
        private string frpExePath;
        private string setupIniPath;
        private string temp_path;
        private string temp_api_path;
        private string cflPath;
        private string pictures_path;
        private void InitializePaths()
        {
            directoryPath = Directory.GetCurrentDirectory();
            cflPath = Path.Combine(directoryPath, "CFL");
            frpPath = Path.Combine(cflPath, "frp");
            frpIniPath = Path.Combine(frpPath, "frpc.ini");
            frpExePath = Path.Combine(frpPath, "frpc.exe");
            setupIniPath = Path.Combine(cflPath, "Setup.ini");
            temp_path = Path.Combine(cflPath, "temp");
            temp_api_path = Path.Combine(temp_path, "Chmlfrp_api.json");
            pictures_path = Path.Combine(cflPath, "pictures");
        }
        public ClienterClass()
        {
            InitializePaths();
        }
        public string FrpPath()
        {
            return frpPath;
        }
        public string FrpIniPath()
        {
            return frpIniPath;
        }
        public string FrpExePath()
        {
            return frpExePath;
        }
        public string DirectoryPath()
        {
            return directoryPath;
        }
        public string CFLPath()
        {
            return cflPath;
        }
        public string SetupIniPath()
        {
            return setupIniPath;
        }
        public string Temp_api_path()
        {
            return temp_api_path;
        }
        public string Temp_path()
        {
            return temp_path;
        }
        public string Pictures_path()
        {
            return pictures_path;
        }
    }
}
