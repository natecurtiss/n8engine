using System;
using N8Engine.Internal;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    /// <summary>
    /// The console window used to display the game.
    /// </summary>
    internal static class Window
    {
        private static readonly Vector2 _span = new(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// Initializes the window.
        /// </summary>
        public static void Initialize()
        {
            ConsoleQuickEditMode.Enabled = false;
            ConsoleText.SetCurrentFont("Arial", 20);
            ConsoleResizing.Maximize();
            Console.CursorVisible = false;
        }

        public static Vector2 GetWindowPositionAsWorldPosition(in Vector2 position) => position + _span / 2;
    }
}