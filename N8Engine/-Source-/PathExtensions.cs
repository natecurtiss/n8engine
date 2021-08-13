using System;
using System.IO;

namespace N8Engine
{
    public static class PathExtensions
    {
        public static string PathToRootFolder { get; private set; }
        public static string PathToLogsFolder => $"{PathToRootFolder}\\.error_logs";

        internal static void Initialize()
        {
            const int maximum_parent_directories_to_search = 20;
            var startingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var currentDirectory = startingDirectory;

            for (var i = 0; i < maximum_parent_directories_to_search; i++)
            {
                var numberOfSolutionFilesInDirectory = Directory.GetFiles(currentDirectory, "*.sln", SearchOption.TopDirectoryOnly).Length;
                if (numberOfSolutionFilesInDirectory == 1)
                {
                    PathToRootFolder = currentDirectory;
                    return;
                }
                currentDirectory = Directory.GetParent(currentDirectory).FullName;
            }
            throw new FileNotFoundException(".sln file was not found when looking for root directory!");
        }
    }
}