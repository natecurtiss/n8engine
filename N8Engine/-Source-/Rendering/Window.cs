using System;
using N8Engine.Native;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public static class Window
    {
        private static readonly int _width = Console.WindowWidth;
        private static readonly int _height = Console.WindowHeight;
        private static readonly IntegerVector _span = new(_width, _height);
        public static Vector BottomLeftCorner { get; private set; }
        public static Vector BottomRightCorner { get; private set; }
        public static Vector TopLeftCorner { get; private set; }
        public static Vector TopRightCorner { get; private set; }
        public static Vector LeftSide { get; private set; }
        public static Vector RightSide { get; private set; }
        public static Vector TopSide { get; private set; }
        public static Vector BottomSide { get; private set; }

        internal static void Initialize(short cameraSize)
        {
            Console.Title = "-n8engine-";
            ConsoleModeHelper.EnableAnsiEscapeSequences();
            ConsoleFontHelper.SetCurrentFont("Consolas", cameraSize);
            ConsoleQuickEditModeHelper.IsEnabled = false;
            ConsoleResizingHelper.RemoveTitlebarAndScrollbar();
            ConsoleResizingHelper.Maximize();
            Console.CursorVisible = false;
            
            LeftSide = new Vector(-_width / 2f, 0f);
            RightSide = new Vector(_width / 2f, 0f);
            TopSide = new Vector(0f, _height / 2f);
            BottomSide = new Vector(0f, -_height / 2f);
            BottomLeftCorner = new Vector(LeftSide.X, BottomSide.Y);
            BottomRightCorner = new Vector(RightSide.X, BottomSide.Y);
            TopLeftCorner = new Vector(LeftSide.X, TopSide.Y);
            TopRightCorner = new Vector(RightSide.X, TopSide.Y);
        }

        internal static IntegerVector FromWorldPositionToWindowPosition(this Vector position)
        {
            var worldPosition = new Vector(position.X, -position.Y);
            var windowPosition = (IntegerVector) (worldPosition + _span / 2);
            return windowPosition;
        }

        internal static bool IsOutsideOfTheWorld(this IntegerVector position) => !position.IsInsideOfTheWorld();
        
        private static bool IsInsideOfTheWorld(this IntegerVector position) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= _width -1 &&
            position.Y <= _height - 1;
    }
}