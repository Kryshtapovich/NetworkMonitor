namespace NetworkMonitor.Models
{
    public class GridItem
    {
        public int ID { get; set; }
        public string Protocol { get; set; }
        public uint LocalPort { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }
        public uint RemotePort { get; set; }
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
    }
}
