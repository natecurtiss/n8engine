using System;
using System.IO;

namespace N8Engine
{
    public static class Debug
    {
        private static string _pathToLogsFile;
        
        internal static void Initialize()
        {
            try
            {
                _pathToLogsFile = Directory.GetFiles(PathExtensions.PathToRootFolder, "*.logs", SearchOption.AllDirectories)[0];
            }
            catch (IndexOutOfRangeException)
            {
                throw new FileNotFoundException("No .logs file was found, please add one :)");
            }

            File.WriteAllText(_pathToLogsFile, string.Empty);
        }

        public static void Log(object message) => File.AppendAllLinesAsync(_pathToLogsFile, new[] { message.ToString() });
    }
}