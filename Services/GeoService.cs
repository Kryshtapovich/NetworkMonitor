using NetworkMonitor.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetworkMonitor.Services
{
    public class GeoService
    {
        private readonly string ipUrl = "http://ip-api.com/json/";

        public IPAddress GetIp()
        {
            var uri = new Uri(ipUrl);
            return Dns.GetHostAddresses(uri.Host)[0];
        }

        public async Task<Geoposition> GetGeopositionAsync(string ip)
        {
            var data = await new HttpClient().GetStringAsync($"{ipUrl}{ip}");
            var geoposition = JsonSerializer.Deserialize<Geoposition>(data);
            if (geoposition.Status != "success")
            {
                throw new Exception("Unable to get geoposition");
            }
            return geoposition;
        }
    }
}
