using NetworkMonitor.Models;
using System;
using System.Collections.Generic;

namespace NetworkMonitor.Services
{
    public class VisualizationService
    {
        public List<GridItem> GetData()
        {
            var tcpPackets = new NetworkService().GetTcpPackets();
            var udpPackets = new NetworkService().GetUdpPackets();

            var results = new List<GridItem>();
            var geoService = new GeoService();
            var processService = new ProcessService();

            Action<Packet> addPacket = (packet) =>
            {
                var item = new GridItem
                {
                    ID = packet.ID,
                    Protocol = packet.Protocol,
                    LocalPort = packet.LocalPort,
                    RemotePort = packet.RemotePort,
                    LocalAddress = packet.LocalAddress.ToString(),
                    RemoteAddress = packet.RemoteAddress.ToString(),
                    ProcessName = processService.GetProcessNameByPID(packet.PID),
                    ProcessPath = processService.GetProcessPathByPID(packet.PID),
                };
                results.Add(item);
            };
            tcpPackets.ForEach(addPacket);
            udpPackets.ForEach(addPacket);

            return results;
        }
    }
}
