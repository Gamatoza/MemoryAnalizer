using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
namespace MemoryAnalizer
{
    public class SystemInfoGetter
    {
        protected static string AppName => AppDomain.CurrentDomain.FriendlyName;
        protected static long AppWorkingMemory => Process.GetProcessesByName(AppName)[0].WorkingSet64/1024/1024;
        protected static string OSName => Environment.OSVersion.VersionString;
        public static bool isAvailibleOS(string osname) => OSName.ToLower().Contains(osname);
    }
}
