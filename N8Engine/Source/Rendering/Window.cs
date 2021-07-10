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
        public static readonly float Width = Console.WindowWidth;
        public static readonly float Height = Console.WindowHeight;

        private static readonly Vector2 _span = new(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// Initializes the window.
        /// </summary>
        public static void Initialize()
        {
            ConsoleText.SetCurrentFont("Arial", 9);
            ConsoleQuickEditMode.Enabled = false;
            ConsoleResizing.Maximize();
            Console.CursorVisible = false;
        }

        public static Vector2 GetWindowPositionAsWorldPosition(in Vector2 position) => position + _span / 2;

        public static bool IsWithinWindow(in Vector2 position) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= Window.Width - 1 &&
            position.Y <= Window.Height - 1;
    }
}