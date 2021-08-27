using System;
using N8Engine.Native;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public static class Window
    {
        internal const float RATIO_OF_HORIZONTAL_PIXELS_TO_VERTICAL_PIXELS = 2f;
        
        private static readonly float _width = Console.WindowWidth;
        private static readonly float _height = Console.WindowHeight;
        private static readonly Vector _span = new(_width, _height);
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
            ConsoleResizingHelper.Maximize();
            Console.CursorVisible = false;
            
            BottomLeftCorner = new Vector(-Console.WindowWidth / 2f, Console.WindowHeight / 2f);
            BottomRightCorner = new Vector(Console.WindowWidth / 2f, Console.WindowHeight / 2f);
            TopLeftCorner = new Vector(-Console.WindowWidth / 2f, -Console.WindowHeight / 2f);
            TopRightCorner = new Vector(Console.WindowWidth / 2f, -Console.WindowHeight / 2f);
            LeftSide = new Vector(-Console.WindowWidth / 2f, 0f);
            RightSide = new Vector(Console.WindowWidth / 2f, 0f);
            TopSide = new Vector(0f, -Console.WindowHeight / 2f);
            BottomSide = new Vector(0f, Console.WindowHeight / 2f);
        }

        internal static Vector FromWindowPositionToWorldPosition(this Vector position) => position + _span / 2;
        
        internal static bool IsWithinWindow(this Vector position) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X <= _width - 1 &&
            position.Y <= _height - 1;
    }
}