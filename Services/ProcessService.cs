using NetworkMonitor.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace NetworkMonitor.Services
{
    public class ProcessService
    {
        public List<Process> GetListOfProcesses()
        {
            return Process.GetProcesses().ToList();
        }

        public Process GetProcessByID(int pid)
        {
            return Process.GetProcesses().Where(p => p.Id == pid).SingleOrDefault();
        }

        public string GetProcessNameByPID(int pid)
        {
            if (pid == 0)
            {
                return "System";
            }

            var process = GetProcessByID(pid);
            return process == null ? "Unknown" : process.ProcessName;
        }

        public string GetProcessPathByPID(int pid)
        {
            var process = GetProcessByID(pid);
            return process == null ? "Not Available" : process.GetMainModuleFileName();
        }
    }
}
