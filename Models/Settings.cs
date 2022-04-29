using Newtonsoft.Json;
using System;

namespace NetworkMonitor.Models
{
    [Serializable]
    public class Settings
    {
        [JsonProperty("ExcludeAppFromTCPConnections")]
        public bool ExcludeAppFromTCPConnections { get; set; }

        [JsonProperty("LastDeviceUsed")]
        public string LastDeviceUsed { get; set; }
    }
}
