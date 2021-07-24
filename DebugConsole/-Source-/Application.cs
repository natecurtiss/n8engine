using System;
using DebugConsole;
using Shared;

Application.Initialize();

namespace DebugConsole
{
    internal static class Application
    {
        private static string _path;
        
        public static void Initialize()
        {
            _path = PathExtensions.PathToRootFolder + "/.logs";
        }
    }
}