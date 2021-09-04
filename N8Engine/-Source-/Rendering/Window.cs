using System;
using N8Engine.Native;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public static class Window
    {
        private static float _width;
        private static float _height;
        private static Vector _span;
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
            GameLoop.OnPreUpdate += _ => UpdateWindowSize();
            
            Console.Title = "-n8engine-";
            ConsoleModeHelper.EnableAnsiEscapeSequences();
            ConsoleFontHelper.SetCurrentFont("Consolas", cameraSize);
            ConsoleQuickEditModeHelper.IsEnabled = false;
            ConsoleResizingHelper.Maximize();
            Console.CursorVisible = false;
            
            UpdateWindowSize();
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

        private static void UpdateWindowSize()
        {
            if (Console.WindowWidth == _width && Console.WindowHeight == _height) return;
            Console.Clear();
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
            _span = new Vector(_width, _height);
            LeftSide = new Vector(-_width / 2f, 0f);
            RightSide = new Vector(_width / 2f, 0f);
            TopSide = new Vector(0f, _height / 2f);
            BottomSide = new Vector(0f, -_height / 2f);
            BottomLeftCorner = new Vector(LeftSide.X, BottomSide.Y);
            BottomRightCorner = new Vector(RightSide.X, BottomSide.Y);
            TopLeftCorner = new Vector(LeftSide.X, TopSide.Y);
            TopRightCorner = new Vector(RightSide.X, TopSide.Y);
        }
    }
}