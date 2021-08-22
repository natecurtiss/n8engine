using System.IO;

namespace N8Engine
{
    public static class Debug
    {
        private static string _pathToLogsFile;

        internal static void Initialize(string pathToLogsFile)
        {
#if DEBUG
            _pathToLogsFile = Path.GetFullPath(pathToLogsFile);
            File.WriteAllText(_pathToLogsFile, string.Empty);
#endif
        }

        public static void Log(object message)
        {
#if DEBUG
            File.AppendAllLinesAsync(_pathToLogsFile, new[] { message.ToString() });
#endif
        }
    }
}
