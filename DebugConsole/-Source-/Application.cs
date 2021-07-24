using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DebugConsole;
using Shared;

Application.Initialize();

namespace DebugConsole
{
    internal static class Application
    {
        private static int _numberOfWrittenLinesInConsole;
        
        public static void Initialize()
        {
            Console.Title = "N8Engine Debug Console";
            var path = PathExtensions.PathToLogsFolder;
            while (true)
            {
                var fileLines = new List<string>();
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var streamReader = new StreamReader(fileStream, Encoding.Default))
                    while (streamReader.ReadLine() != null)
                        fileLines.Add(streamReader.ReadLine());
                if (fileLines.Count != _numberOfWrittenLinesInConsole)
                    for (var line = _numberOfWrittenLinesInConsole - 1; line < fileLines.Count; line++)
                    {
                        Console.WriteLine(line);
                        _numberOfWrittenLinesInConsole++;
                    }
            }
        }
    }
}