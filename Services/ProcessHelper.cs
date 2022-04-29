using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;


namespace NetworkMonitor.Services
{
    public class ProcessHelper
    {
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        static extern uint GetClassLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        static extern IntPtr GetClassLong64(IntPtr hWnd, int nIndex);

        static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            return IntPtr.Size == 4 ? new IntPtr(GetClassLong32(hWnd, nIndex)) : GetClassLong64(hWnd, nIndex);
        }

        static readonly uint WM_GETICON = 0x007f;
        static readonly IntPtr ICON_SMALL2 = new IntPtr(2);
        static readonly int GCL_HICON = -14;

        public static Image GetProcessIcon(int pid)
        {
            if (GetProcessByID(pid) == null)
            {
                return null;
            }

            var hWnd = GetProcessByID(pid).MainWindowHandle;
            var hIcon = SendMessage(hWnd, WM_GETICON, ICON_SMALL2, IntPtr.Zero);

            if (hIcon == IntPtr.Zero)
            {
                hIcon = GetClassLongPtr(hWnd, GCL_HICON);
            }

            if (hIcon == IntPtr.Zero)
            {
                hIcon = LoadIcon(IntPtr.Zero, (IntPtr)0x7F00);
            }

            return hIcon != IntPtr.Zero ? new Bitmap(Icon.FromHandle(hIcon).ToBitmap(), 16, 16) : null;
        }

        public static List<Process> GetListOfProcesses()
        {
            return Process.GetProcesses().ToList<Process>();
        }

        public static Process GetProcessByID(int pid)
        {
            return Process.GetProcesses().Where(p => p.Id == pid).SingleOrDefault();
        }

        public static string GetProcessNameByTcpConnection(IPAddress sourceAddress, IPAddress destinationAddress, ushort sourcePort, ushort destinationPort, IPAddress localIP)
        {
            List<TcpRecord> tcpRecords = null;
            ushort port;
            IPAddress address;

            if (localIP == sourceAddress)
            {
                port = sourcePort;
                address = sourceAddress;
            }
            else
            {
                port = destinationPort;
                address = destinationAddress;
            }

            if ((tcpRecords = NetworkHelper.GetTcpConnections()) != null && tcpRecords.Count > 0)
            {
                var record = tcpRecords.Where(r => r.LocalPort == port).SingleOrDefault();
                if (record != null)
                {
                    return record.PID.ToString();
                }
            }

            return string.Empty;
        }

        public static string GetProcessNameByUDpConnection(IPAddress sourceAddress, IPAddress destinationAddress, ushort sourcePort, ushort destinationPort, IPAddress localIP)
        {
            List<UdpRecord> udpRecords = null;
            ushort port;
            IPAddress address;

            if (localIP == sourceAddress)
            {
                port = sourcePort;
                address = sourceAddress;
            }
            else
            {
                port = destinationPort;
                address = destinationAddress;
            }

            if ((udpRecords = NetworkHelper.GetUdpConnections()) != null)
            {
                var record = udpRecords.Where(r => r.LocalPort == port).SingleOrDefault();
                if (record != null)
                {
                    return record.PID.ToString();
                }
            }

            return string.Empty;
        }
    }

    internal static class Extensions
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            try
            {
                var fileNameBuilder = new StringBuilder(buffer);
                var bufferLength = (uint)fileNameBuilder.Capacity + 1;
                return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                    fileNameBuilder.ToString() :
                    null;
            }
            catch
            {
                return "Not Available";
            }
        }
    }
}
