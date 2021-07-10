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
        public static Vector2 LeftCenter => Center + Vector2.Left * Console.WindowWidth;
        public static Vector2 RightCenter => Center + Vector2.Right * Console.WindowWidth;
        public static Vector2 TopCenter => Center + Vector2.Up * Console.WindowHeight;
        public static Vector2 BottomCenter => Center + Vector2.Down * Console.WindowHeight;
        public static Vector2 BottomLeft => Center - _halfSpan;
        public static Vector2 TopLeft => Center + new Vector2(-_halfSpan.X, _halfSpan.Y);
        public static Vector2 BottomRight => Center + new Vector2(_halfSpan.X, -_halfSpan.Y);
        public static Vector2 TopRight => Center + _halfSpan;
        private static Vector2 Center => Vector2.Zero;

        private static readonly int _width = Console.WindowWidth;
        private static readonly int _height = Console.WindowHeight;
        private static readonly Vector2 _span = new(_width, _height);
        private static readonly Vector2 _halfSpan = _span / 2f;
        
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

        public static Vector2 GetWindowPositionAsWorldPosition(in Vector2 position) => position + _halfSpan;
    }
}