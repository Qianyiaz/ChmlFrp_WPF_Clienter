using System.IO;

namespace ChmlFrp_WPF_Clienter
{
    internal class ClienterClass
    {
        public string directoryPath;
        public string frpPath;
        public string frpIniPath;
        public string frpExePath;
        public string setupIniPath;
        public string temp_path;
        public string temp_api_path;
        public string cflPath;
        public string pictures_path;
        public ClienterClass()
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
    }
}
