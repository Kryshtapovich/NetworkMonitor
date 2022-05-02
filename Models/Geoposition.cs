using System;
using System.Text.Json.Serialization;

namespace NetworkMonitor.Models
{
    [Serializable]
    public class Geoposition
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("regionName")]
        public string RegionName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("org")]
        public string Organization { get; set; }
    }
}
