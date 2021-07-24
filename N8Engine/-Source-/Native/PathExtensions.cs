using System;
using System.IO;

namespace N8Engine.Native
{
    internal static class PathExtensions
    {
        public static string PathToRootFolder => PathToRootFolderDirectoryInfo.FullName;
        private static DirectoryInfo PathToRootFolderDirectoryInfo => Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent;
    }
}