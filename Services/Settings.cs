using NetworkMonitor.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace NetworkMonitor.Services
{
    public class SettingsHelper
    {
        public static Settings GetSettings()
        {
            var r = new Settings { ExcludeAppFromTCPConnections = true };
            var path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "settings.json");
            if (File.Exists(path))
            {
                r = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
            }
            return r;
        }

        public static void SaveSettings(Settings s)
        {
            var path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "settings.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(s));
        }
    }
}
