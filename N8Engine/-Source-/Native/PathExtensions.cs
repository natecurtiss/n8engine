using System;
using System.IO;

namespace N8Engine.Native
{
    internal static class PathExtensions
    {
        public static string PathToProjectAnchor
        {
            get
            {
                var anchorFiles = Directory.GetFiles(PathToRootFolderDirectoryInfo.FullName, "*.anchor", SearchOption.AllDirectories);
                var anchorFileDirectory = Directory.GetParent(anchorFiles[0])?.FullName;
                return anchorFileDirectory;
            }
        }
        public static string PathToRootFolder => PathToRootFolderDirectoryInfo.FullName;
        
        private static DirectoryInfo PathToRootFolderDirectoryInfo => Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent;
    }
}