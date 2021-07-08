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
        public static Vector2 Center => Vector2.Zero;
        public static Vector2 CenterLeft => Center + Vector2.Left * Console.WindowWidth;
        public static Vector2 CenterRight => Center + Vector2.Right * Console.WindowWidth;
        public static Vector2 CenterTop => Center + Vector2.Up * Console.WindowHeight;
        public static Vector2 CenterBottom => Center + Vector2.Down * Console.WindowHeight;
        public static Vector2 BottomLeft => Center - HalfSpan;
        public static Vector2 TopLeft => Center + new Vector2(-HalfSpan.X, HalfSpan.Y);
        public static Vector2 BottomRight => Center + new Vector2(HalfSpan.X, -HalfSpan.Y);
        public static Vector2 TopRight => Center + HalfSpan;

        private static Vector2 Span => new(Console.WindowWidth, Console.WindowHeight);
        private static Vector2 HalfSpan => Span / 2f;
        
        /// <summary>
        /// Initializes the window.
        /// </summary>
        internal static void Initialize()
        {
            ConsoleQuickEditMode.Enabled = false;
            ConsoleText.SetCurrentFont("Arial", 1);
            ConsoleResizing.Maximize();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▒▒▒▒▒▒▒▒▒\n▒▒▒▒▒▒▒▒▒\n▒▒▒▒▒▒▒▒▒\n▒▒▒▒▒▒▒▒▒\n▒▒▒▒▒▒▒▒▒\n▒▒▒▒▒▒▒▒▒");
            Console.CursorVisible = false;
        }
    }
}