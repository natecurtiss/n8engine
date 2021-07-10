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
        public static int Width => Console.WindowWidth;
        public static int Height => Console.WindowHeight;
        
        public static Vector2 Center => Vector2.Zero;
        public static Vector2 LeftCenter => Center + Vector2.Left * Console.WindowWidth;
        public static Vector2 RightCenter => Center + Vector2.Right * Console.WindowWidth;
        public static Vector2 TopCenter => Center + Vector2.Up * Console.WindowHeight;
        public static Vector2 BottomCenter => Center + Vector2.Down * Console.WindowHeight;
        public static Vector2 BottomLeft => Center - HalfSpan;
        public static Vector2 TopLeft => Center + new Vector2(-HalfSpan.X, HalfSpan.Y);
        public static Vector2 BottomRight => Center + new Vector2(HalfSpan.X, -HalfSpan.Y);
        public static Vector2 TopRight => Center + HalfSpan;

        private static Vector2 Span => new(Width, Height);
        private static Vector2 HalfSpan => Span / 2f;
        
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

        public static Vector2 GetWindowPositionAsWorldPosition(in Vector2 position) => position + HalfSpan;
    }
}