using System;
using System.Collections.Generic;
using System.IO;

namespace ErrorConsole
{
    // Publish this as a single self-contained executable and place the .exe file in the root directory.
    internal static class Program
    {
        private static int _numberOfWrittenLinesInConsole;

        private static void Main(string[] arguments)
        {
            Console.Title = "N8Engine Error Console";
            var path = arguments[0];
            while (true)
            {
                var fileLines = new List<string>();
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var streamReader = new StreamReader(fileStream))
                {
                    var line = "";
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