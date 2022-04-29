using System.Drawing;

namespace NetworkMonitor.Models
{
    public class GridItem
    {
        public int ID { get; set; }
        public string Protocol { get; set; }
        public int LocalPort { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }
        public int RemotePort { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public Image ProcessIcon { get; set; }
        public int PID { get; set; }
    }
}
