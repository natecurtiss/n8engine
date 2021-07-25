using System;
using System.IO;

namespace N8Engine
{
    public static class PathExtensions
    {
        public static string PathToRootFolder => Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        public static string PathToLogsFolder => $"{PathToRootFolder}\\internal.error_logs";
    }
}