using NetworkMonitor.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetworkMonitor.Services
{
    public class VisualizationHelper
    {
        private static readonly List<TcpRecord> mapItems = new List<TcpRecord>();
        public static IList<GridItem> GridSource = new SortableBindingList<GridItem>();

        public static bool CheckData(ref Settings settings)
        {
            var results = new List<GridItem>();
            var tcpList = NetworkHelper.GetTcpConnections(settings.ExcludeAppFromTCPConnections);
            var udpList = NetworkHelper.GetUdpConnections();
            var wasChange = false;
            if (GridSource.Count > 0)
            {
                results = GridSource.ToList();
                var nonExisting = results.Where(fn => fn.Protocol == "TCP").ToList().Where(fn => !tcpList.Select(t => t.ID).ToList().Contains(fn.ID)).ToList();
                nonExisting.ForEach(it =>
                {
                    results.Remove(it);
                    mapItems.Remove(mapItems.Where(fn => fn.ID == it.ID).FirstOrDefault());
                });
                nonExisting = results.Where(fn => fn.Protocol == "UDP").ToList().Where(fn => !udpList.Select(t => t.ID).ToList().Contains(fn.ID)).ToList();
                nonExisting.ForEach(it => results.Remove(it));
            }
            else
            {
                wasChange = true;
            }
            tcpList.ForEach(t =>
            {
                if (results.Where(fn => fn.Protocol == t.Protocol && fn.ID == t.ID).Count() == 0)
                {
                    t.UpdateDetails();
                    if (t.GeoData.IsOK())
                    {
                        mapItems.Add(t);
                    }
                    var item = new GridItem
                    {
                        ID = t.ID,
                        Protocol = t.Protocol,
                        LocalPort = t.LocalPort,
                        RemotePort = t.RemotePort,
                        LocalAddress = t.LocalAddress.ToString(),
                        RemoteAddress = t.RemoteAddress.ToString(),
                        Country = t.Country,
                        City = t.City,
                        ProcessName = t.ProcessName,
                        Status = t.StateText,
                        PID = t.PID,
                        ProcessIcon = t.ProcessIcon,
                        ProcessPath = t.ProcessPath,
                    };
                    results.Add(item);
                    wasChange = true;
                }
            });
            udpList.ForEach(t =>
            {
                if (results.Where(fn => fn.Protocol == t.Protocol && fn.ID == t.ID).Count() == 0)
                {
                    var item = new GridItem
                    {
                        ID = t.ID,
                        Protocol = t.Protocol,
                        LocalPort = (int)t.LocalPort,
                        RemotePort = -1,
                        LocalAddress = t.LocalAddress.ToString(),
                        RemoteAddress = string.Empty,
                        Country = string.Empty,
                        City = string.Empty,
                        ProcessName = t.ProcessName,
                        Status = string.Empty,
                        PID = t.PID,
                        ProcessIcon = t.ProcessIcon,
                        ProcessPath = t.ProcessPath,
                    };
                    results.Add(item);
                    wasChange = true;
                }
            });

            if (wasChange)
            {
                GridSource = new SortableBindingList<GridItem>(results);
            }
            return wasChange;
        }
    }
}
