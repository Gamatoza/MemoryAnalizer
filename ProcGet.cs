using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MemoryAnalizer
{
    public class ProcGet : SystemInfoGetter, IMemInfo
    {
        static ProcGet()
        {
            if (isAvailibleOS("windows")) throw new Exception("Is not availible OS to Wmic usage");
        }

        public static string GetInfo(string query, bool redirectStandardOutput = true)
        {
            var info = new ProcessStartInfo("cat /proc/meminfo");
            info.Arguments = query;
            info.RedirectStandardOutput = redirectStandardOutput;
            var output = "";
            using (var process = Process.Start(info))
            {
                var res = process.StandardOutput.ReadToEnd().Split("\n");
                res.Where(i => i.ToLower().Contains(query.ToLower()));
                if (res.Length > 1) throw new Exception("Incorrect query input");
                output = res[0].Split(":")[1].Replace("kB",String.Empty);
            }
            return output.Trim();
        }

        public static int FreeSpace => int.Parse(GetInfo("MemFree"));
        public static int TotalSpace => int.Parse(GetInfo("MemFree"));
        public static string CPUName => null;
        public static int CPULoadPercentage => 100;
        public static bool CanRunProgram(int space)
        {
            return FreeSpace > space;
        }
        public static bool CanRunThisProgram()
        {
            return FreeSpace > AppWorkingMemory;
        }

        public static int CPUAvarangeLoadPercentage(int seconds) { return -1; }
    }
}
