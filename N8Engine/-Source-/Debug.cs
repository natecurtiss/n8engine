using System.IO;

namespace N8Engine
{
    public static class Debug
    {
        private static string _pathToLogsFile;
        
        internal static void Initialize()
        {
            _pathToLogsFile = Directory.GetFiles(PathExtensions.PathToRootFolder, "*.logs", SearchOption.AllDirectories)[0];
            if (_pathToLogsFile == string.Empty)
                throw new FileNotFoundException("No .logs file was found, please add one :)");
        }

        public static void Log(object message)
        {
            File.AppendAllLinesAsync(_pathToLogsFile, new[] { message.ToString() });
        }
    }
}