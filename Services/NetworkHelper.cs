using NetworkMonitor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace NetworkMonitor.Services
{
    public enum TCP_TABLE_CLASS
    {
        TCP_TABLE_BASIC_LISTENER,
        TCP_TABLE_BASIC_CONNECTIONS,
        TCP_TABLE_BASIC_ALL,
        TCP_TABLE_OWNER_PID_LISTENER,
        TCP_TABLE_OWNER_PID_CONNECTIONS,
        TCP_TABLE_OWNER_PID_ALL,
        TCP_TABLE_OWNER_MODULE_LISTENER,
        TCP_TABLE_OWNER_MODULE_CONNECTIONS,
        TCP_TABLE_OWNER_MODULE_ALL
    }

    public enum UDP_TABLE_CLASS
    {
        UDP_TABLE_BASIC,
        UDP_TABLE_OWNER_PID,
        UDP_TABLE_OWNER_MODULE
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        MIB_TCPROW_OWNER_PID table;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPROW_OWNER_PID
    {
        public uint state;
        public uint localAddr;
        public byte localPort1;
        public byte localPort2;
        public byte localPort3;
        public byte localPort4;
        public uint remoteAddr;
        public byte remotePort1;
        public byte remotePort2;
        public byte remotePort3;
        public byte remotePort4;
        public int owningPid;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        public MIB_UDPROW_OWNER_PID udpTable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPROW_OWNER_PID
    {
        public uint localAddr;
        public byte localPort1;
        public byte localPort2;
        public byte localPort3;
        public byte localPort4;
        public int owningPid;
    }

    public class NetworkHelper
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, int reserved);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedUdpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, UDP_TABLE_CLASS tblClass, int reserved);

        public const int NO_ERROR = 0;
        public const int MIB_TCP_STATE_CLOSED = 1;
        public const int MIB_TCP_STATE_LISTEN = 2;
        public const int MIB_TCP_STATE_SYN_SENT = 3;
        public const int MIB_TCP_STATE_SYN_RCVD = 4;
        public const int MIB_TCP_STATE_ESTAB = 5;
        public const int MIB_TCP_STATE_FIN_WAIT1 = 6;
        public const int MIB_TCP_STATE_FIN_WAIT2 = 7;
        public const int MIB_TCP_STATE_CLOSE_WAIT = 8;
        public const int MIB_TCP_STATE_CLOSING = 9;
        public const int MIB_TCP_STATE_LAST_ACK = 10;
        public const int MIB_TCP_STATE_TIME_WAIT = 11;
        public const int MIB_TCP_STATE_DELETE_TCB = 12;

        public static string StateToStr(int state)
        {
            var strg_state = "";
            switch (state)
            {
                case MIB_TCP_STATE_CLOSED: strg_state = "CLOSED"; break;
                case MIB_TCP_STATE_LISTEN: strg_state = "LISTEN"; break;
                case MIB_TCP_STATE_SYN_SENT: strg_state = "SYN_SENT"; break;
                case MIB_TCP_STATE_SYN_RCVD: strg_state = "SYN_RCVD"; break;
                case MIB_TCP_STATE_ESTAB: strg_state = "ESTAB"; break;
                case MIB_TCP_STATE_FIN_WAIT1: strg_state = "FIN_WAIT1"; break;
                case MIB_TCP_STATE_FIN_WAIT2: strg_state = "FIN_WAIT2"; break;
                case MIB_TCP_STATE_CLOSE_WAIT: strg_state = "CLOSE_WAIT"; break;
                case MIB_TCP_STATE_CLOSING: strg_state = "CLOSING"; break;
                case MIB_TCP_STATE_LAST_ACK: strg_state = "LAST_ACK"; break;
                case MIB_TCP_STATE_TIME_WAIT: strg_state = "TIME_WAIT"; break;
                case MIB_TCP_STATE_DELETE_TCB: strg_state = "DELETE_TCB"; break;
            }
            return strg_state;
        }

        public static List<TcpRecord> GetTcpConnections(bool excludeCurrentProcess = false)
        {
            var AF_INET = 2;
            var buffSize = 0;
            var currentPid = Process.GetCurrentProcess().Id;

            var val = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

            if (val != 0 && val != 122)
            {
                throw new Exception("invalid size " + val);
            }

            var buffTable = Marshal.AllocHGlobal(buffSize);
            var lstRecords = new List<TcpRecord>();
            try
            {
                val = GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

                if (val != 0)
                {
                    throw new Exception("ivalid data " + val);
                }

                var tcpTable = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_TCPTABLE_OWNER_PID));
                var rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tcpTable.dwNumEntries));

                for (int i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    var tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    if (excludeCurrentProcess && currentPid == tcpRow.owningPid)
                    {
                        rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                        continue;
                    }
                    lstRecords.Add(new TcpRecord(
                        new IPAddress(tcpRow.localAddr),
                        new IPAddress(tcpRow.remoteAddr),
                        BitConverter.ToUInt16(new byte[2] { tcpRow.localPort2, tcpRow.localPort1 }, 0),
                        BitConverter.ToUInt16(new byte[2] { tcpRow.remotePort2, tcpRow.remotePort1 }, 0),
                        tcpRow.owningPid,
                        tcpRow.state));

                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }
            return lstRecords.Distinct().ToList<TcpRecord>();
        }

        public static List<UdpRecord> GetUdpConnections()
        {
            int AF_INET = 2;
            int buffSize = 0;

            var val = GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, AF_INET, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);

            if (val != 0 && val != 122)
            {
                throw new Exception("invalid size " + val);
            }

            var buffTable = Marshal.AllocHGlobal(buffSize);
            var lstRecords = new List<UdpRecord>();
            try
            {
                val = GetExtendedUdpTable(buffTable, ref buffSize, true, AF_INET, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);

                if (val != 0)
                    throw new Exception("ivalid data " + val);

                var udpTable = (MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_UDPTABLE_OWNER_PID));
                var rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(udpTable.dwNumEntries));

                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    var udpRow = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    lstRecords.Add(new UdpRecord(
                        new IPAddress(udpRow.localAddr),
                        BitConverter.ToUInt16(new byte[2] { udpRow.localPort2, udpRow.localPort1 }, 0),
                        udpRow.owningPid));

                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(udpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }
            return lstRecords.Distinct().ToList();
        }

        public static int GetHash(string protocol, int port, int val, long address, int remotePort, int pid, int state)
        {
            if (string.IsNullOrEmpty(protocol))
            {
                return -1;
            }

            var ret = 0;
            var a = 127;

            var ch = protocol.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                ret = ((a * ret + port + remotePort + pid + state + ch[i]) % val);
            }

            return (ret + (int)((address + remotePort + pid + state) & val));
        }
    }

    public class TcpRecord
    {
        private int PLength { get; set; }
        public IPAddress LocalAddress { get; set; }
        public IPAddress RemoteAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public ushort LocalPort { get; set; }
        public ushort RemotePort { get; set; }
        public int PID { get; set; }
        public uint State { get; set; }
        public string StateText { get; set; }
        public string Protocol { get; set; }
        public int ID { get { return this.GetHashCode(); } }
        public Geo GeoData { get; set; }
        public Image ProcessIcon { get; set; }

        public string ProcessName
        {
            get
            {
                if (PID == 0)
                {
                    return "System";
                }
                Process p;
                return (p = ProcessHelper.GetProcessByID(PID)) != null ? p.ProcessName : "Unknown";
            }
        }

        public string ProcessPath
        {
            get
            {
                if (PID == 0)
                {
                    return string.Empty;
                }
                Process p;
                return (p = ProcessHelper.GetProcessByID(PID)) != null ? p.GetMainModuleFileName() : string.Empty;
            }
        }

        public TcpRecord(IPAddress localIP, IPAddress remoteIP, ushort localPort, ushort remotePort, int pid, uint state)
        {
            LocalAddress = localIP;
            RemoteAddress = remoteIP;
            LocalPort = localPort;
            RemotePort = remotePort;
            PID = pid;
            State = state;
            StateText = NetworkHelper.StateToStr((int)state);
            Protocol = "TCP";
            PLength = 65535;
            ProcessIcon = ProcessHelper.GetProcessIcon(this.PID);
        }

        public void UpdateDetails()
        {

            GeoData = new Geo();
            if (!RemoteAddress.ToString().Equals("0.0.0.0") && !RemoteAddress.ToString().Equals("127.0.0.1"))
            {
                GeoData = IPGeo.GetGeoInfo(RemoteAddress.ToString());
            }
            if (GeoData.IsOK())
            {
                Country = GeoData.Country;
                City = GeoData.City;
            }
        }

        public override int GetHashCode()
        {
            return NetworkHelper.GetHash(Protocol, LocalPort, PLength, LocalAddress.Address, RemotePort, PID, (int)State);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is TcpRecord record && record.GetHashCode() == GetHashCode();
        }
    }

    public class UdpRecord
    {
        private int PLength { get; set; }
        public IPAddress LocalAddress { get; set; }
        public uint LocalPort { get; set; }
        public int PID { get; set; }
        public string Protocol { get; set; }
        public int ID { get { return this.GetHashCode(); } }
        public Image ProcessIcon { get; set; }

        public string ProcessName
        {
            get
            {
                if (PID == 0)
                {
                    return "System";
                }
                Process p;
                return (p = ProcessHelper.GetProcessByID(PID)) != null ? p.ProcessName : "Unknown";
            }
        }

        public string ProcessPath
        {
            get
            {
                if (PID == 0)
                {
                    return string.Empty;
                }
                Process p;
                return (p = ProcessHelper.GetProcessByID(PID)) != null ? p.GetMainModuleFileName() : string.Empty;
            }
        }

        public UdpRecord(IPAddress localAddress, uint localPort, int pid)
        {
            LocalAddress = localAddress;
            LocalPort = localPort;
            PID = pid;
            Protocol = "UDP";
            PLength = 65535;
            ProcessIcon = ProcessHelper.GetProcessIcon(PID);
        }

        public override int GetHashCode()
        {
            return NetworkHelper.GetHash(Protocol, (int)LocalPort, PLength, LocalAddress.Address, 0, PID, 0);
        }

        public override bool Equals(object obj)
        {
            return (obj != null) && obj is UdpRecord record && record.GetHashCode() == GetHashCode();
        }
    }
}
