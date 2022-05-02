using System.Net;

namespace NetworkMonitor.Models
{
    public class Packet
    {
        protected readonly int length = 65535;
        public IPAddress LocalAddress { get; set; }
        public IPAddress RemoteAddress { get; set; }
        public uint LocalPort { get; set; }
        public ushort RemotePort { get; set; }
        public int PID { get; set; }
        public string Protocol { get; set; }
        public int ID => GetHashCode();
        public uint State { get; set; }
        public string StateText { get; set; }
    }
}
