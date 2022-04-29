using NetworkMonitor.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace NetworkMonitor.Services
{
    public class IPGeo
    {
        private static readonly string ipUrl = "http://ip-api.com/json/";

        public static IPAddress GetIp()
        {
            var uri = new Uri(ipUrl);
            return Dns.GetHostAddresses(uri.Host)[0];
        }

        public static Geo GetGeoInfo(string ip)
        {
            var request = WebRequest.Create($"{ipUrl}{ip}");
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var res = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Geo>(res);
        }
    }
}
