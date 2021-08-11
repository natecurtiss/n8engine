using System;
using N8Engine.Native;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal static class Window
    {
        public static bool UseExternalErrorConsole { get; set; } = true;
        
        private static readonly float _width = Console.WindowWidth;
        private static readonly float _height = Console.WindowHeight;
        private static readonly Vector _span = new(_width, _height);
        
        public static void Initialize()
        {
            ConsoleFontHelper.SetCurrentFont("Arial", 5);
            ConsoleQuickEditModeHelper.IsEnabled = false;
            ConsoleResizingHelper.Maximize();
            Console.CursorVisible = false;
            ConsoleErrorHelper.CreateErrorConsole();
        }

        public static Vector FromWindowPositionToWorldPosition(this Vector position) => position + _span / 2;
        
        public static bool IsWithinWindow(this Vector position) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= _width - 1 &&
            position.Y <= _height - 1;
    }
}