using System;
using System.Collections.Generic;
using System.IO;
using N8Engine;

Application.Initialize();

namespace N8Engine
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
                using (var streamReader = new StreamReader(fileStream))
                {
                    var line = string.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                        fileLines.Add(line);
                }
                
                if (fileLines.Count != _numberOfWrittenLinesInConsole)
                {
                    for (var line = _numberOfWrittenLinesInConsole; line < fileLines.Count; line++)
                        Console.WriteLine(fileLines[line]);
                    _numberOfWrittenLinesInConsole += fileLines.Count;
                }
            }
        }
    }
}