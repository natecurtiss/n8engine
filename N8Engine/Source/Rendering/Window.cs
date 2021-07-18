using System;
using System.Collections.Generic;
using N8Engine.Native;
using N8Engine.Mathematics;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using Console = System.Console;

namespace N8Engine.Rendering
{
    /// <summary>
    /// The console window used to display the game.
    /// </summary>
    internal static class Window
    {
        private static readonly float _width = Console.WindowWidth;
        private static readonly float _height = Console.WindowHeight;
        private static readonly Vector _span = new(_width, _height);

        /// <summary>
        /// Initializes the window.
        /// </summary>
        public static void Initialize()
        {
            ConsoleText.SetCurrentFont("Arial", 5);
            ConsoleQuickEditMode.Enabled = false;
            ConsoleResizing.Maximize();
            Console.CursorVisible = false;
        }

        public static Vector GetWindowPositionAsWorldPosition(in Vector position) => position + _span / 2;
        
        public static bool IsWithinWindow(this in Vector position) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= _width - 1 &&
            position.Y <= _height - 1;
    }
}