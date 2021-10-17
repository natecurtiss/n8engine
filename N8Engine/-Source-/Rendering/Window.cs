using System;
using N8Engine.External;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public static class Window
    {
        public static readonly int Width = Console.WindowWidth / Renderer.NUMBER_OF_CHARACTERS_PER_PIXEL;
        public static readonly int Height = Console.WindowHeight;

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
            
            LeftSide = new Vector(-Width / 2f, 0f);
            RightSide = new Vector(Width / 2f, 0f);
            TopSide = new Vector(0f, Height / 2f);
            BottomSide = new Vector(0f, -Height / 2f);
            BottomLeftCorner = new Vector(LeftSide.X, BottomSide.Y);
            BottomRightCorner = new Vector(RightSide.X, BottomSide.Y);
            TopLeftCorner = new Vector(LeftSide.X, TopSide.Y);
            TopRightCorner = new Vector(RightSide.X, TopSide.Y);
        }
    }
}