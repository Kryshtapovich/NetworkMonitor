using NetworkMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace NetworkMonitor.Services
{
    public class NetworkService
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, int reserved);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern uint GetExtendedUdpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, UDP_TABLE_CLASS tblClass, int reserved);

        public List<Packet> GetTcpPackets(bool excludeCurrentProcess = false)
        {
            var AF_INET = 2;
            var buffSize = 0;
            var currentPid = Environment.ProcessId;

            var val = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

            if (val != 0 && val != 122)
            {
                throw new Exception("invalid size " + val);
            }

            var buffTable = Marshal.AllocHGlobal(buffSize);
            var lstRecords = new List<Packet>();
            try
            {
                val = GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

                if (val != 0)
                {
                    throw new Exception("ivalid data " + val);
                }

                var tcpTable = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_TCPTABLE_OWNER_PID));
                var rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tcpTable.dwNumEntries));

                for (var i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    var tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    if (excludeCurrentProcess && currentPid == tcpRow.owningPid)
                    {
                        rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                        continue;
                    }

                    lstRecords.Add(new Packet
                    {
                        Protocol = "TCP",
                        LocalAddress = new IPAddress(tcpRow.localAddr),
                        RemoteAddress = new IPAddress(tcpRow.remoteAddr),
                        LocalPort = BitConverter.ToUInt16(new byte[2] { tcpRow.localPort2, tcpRow.localPort1 }, 0),
                        RemotePort = BitConverter.ToUInt16(new byte[2] { tcpRow.remotePort2, tcpRow.remotePort1 }, 0),
                        PID = tcpRow.owningPid
                    });

                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }
            return lstRecords.Distinct().ToList();
        }

        public List<Packet> GetUdpPackets()
        {
            var AF_INET = 2;
            var buffSize = 0;

            var val = GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, AF_INET, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);

            if (val != 0 && val != 122)
            {
                throw new Exception("invalid size " + val);
            }

            var buffTable = Marshal.AllocHGlobal(buffSize);
            var lstRecords = new List<Packet>();

            try
            {
                val = GetExtendedUdpTable(buffTable, ref buffSize, true, AF_INET, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);

                if (val != 0)
                {
                    throw new Exception("ivalid data " + val);
                }

                var udpTable = (MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_UDPTABLE_OWNER_PID));
                var rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(udpTable.dwNumEntries));

                for (var i = 0; i < udpTable.dwNumEntries; i++)
                {
                    var udpRow = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    lstRecords.Add(new Packet
                    {
                        Protocol = "UDP",
                        LocalAddress = new IPAddress(udpRow.localAddr),
                        RemoteAddress = new IPAddress(udpRow.remoteAddr),
                        LocalPort = BitConverter.ToUInt16(new byte[2] { udpRow.localPort2, udpRow.localPort1 }, 0),
                        RemotePort = BitConverter.ToUInt16(new byte[2] { udpRow.remotePort2, udpRow.remotePort1 }, 0)
                    });

                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(udpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }
            return lstRecords.Distinct().ToList();
        }
    }
}
