using System;
using System.IO;

namespace Shared
{
    public static class PathExtensions
    {
        public static string PathToRootFolder => Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        public static string PathToLogsFolder => $"{PathToRootFolder}\\.logs";
    }
}