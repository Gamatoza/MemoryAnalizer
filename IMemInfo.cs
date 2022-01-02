using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryAnalizer
{
    public interface IMemInfo
    {
        public static int FreeSpace { get; }
        public static int TotalSpace { get; }
        public static string CPUName { get; }
        public static int CPULoadPercentage { get; }

        public static int CPUAvarangeLoadPercentage(int seconds) { return -1; }
    }
}
