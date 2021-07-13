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
        public static readonly float Width = Console.WindowWidth;
        public static readonly float Height = Console.WindowHeight;

        private static readonly Vector2 _span = new(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// Initializes the window.
        /// </summary>
        public static void Initialize()
        {
            ConsoleText.SetCurrentFont("Arial", 7);
            ConsoleQuickEditMode.Enabled = false;
            ConsoleResizing.Maximize();
            Console.CursorVisible = false;
        }

        public static Vector2 GetWindowPositionAsWorldPosition(in Vector2 position) => position + _span / 2;

        public static bool IsWithinWindow(this in Vector2 position) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= Width - 1 &&
            position.Y <= Height - 1;
    }
}