using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MemoryAnalizer
{
    public class WmicGet : SystemInfoGetter
    {
        static WmicGet() 
        {
            if (!isAvailibleOS("windows")) throw new Exception("Is not availible OS to Wmic usage");     
        }

        private static string GetWmicOutput(string query, bool redirectStandardOutput = true)
        {
            var info = new ProcessStartInfo("wmic");
            info.Arguments = query;
            info.RedirectStandardOutput = redirectStandardOutput;
            var output = "";
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd().Split("=")[1];
            }
            return output.Trim();
        }

        public static int FreeSpace => int.Parse(GetWmicOutput("OS get FreePhysicalMemory /VALUE"))/1024;
        public static int TotalSpace => int.Parse(GetWmicOutput("OS get TotalVisibleMemorySize /VALUE"));
        public static string CPUName => GetWmicOutput("CPU get Name /VALUE");
        public static int CPULoadPercentage => int.Parse(GetWmicOutput("CPU get LoadPercentage /VALUE"));
        
        public static bool CanRunProgram(int space)
        {
            return FreeSpace > space;
        }
        public static bool CanRunThisProgram()
        {
            return FreeSpace > AppWorkingMemory;
        }

        public static int CPUAvarangeLoadPercentage(int seconds)
        {
            int avg = 0;
            for (int i = 0; i < seconds; i++)
            {
                avg += CPULoadPercentage;
                Thread.Sleep(1000);
            }
            return avg / seconds;
        }
        
    }
}
