using System;
using System.IO;
using DebugConsole;
using Shared;

Application.Initialize();

namespace DebugConsole
{
    internal static class Application
    {
        private static string _path;
        private static int _numberOfWrittenLinesInConsole;
        
        public static void Initialize()
        {
            Console.Title = "N8Engine Debug Console";
            _path = PathExtensions.PathToRootFolder + "/.logs";
            while (true)
            {
                var fileLines = File.ReadAllLines(_path);
                if (fileLines.Length != _numberOfWrittenLinesInConsole)
                    for (var line = _numberOfWrittenLinesInConsole - 1; line < fileLines.Length; line++)
                    {
                        Console.WriteLine(line);
                        _numberOfWrittenLinesInConsole++;
                    }
            }
        }
    }
}