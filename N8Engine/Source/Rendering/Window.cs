using System;
using System.Runtime.InteropServices;

namespace N8Engine.Rendering
{
    public static class Window
    {
        public static string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }
        public static int Width
        {
            get => Console.WindowWidth;
            set => Console.WindowWidth = value;
        }
        public static int Height
        {
            get => Console.WindowHeight;
            set => Console.WindowHeight = value;
        }
        public static bool IsCursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }
        public static Color BackgroundColor { get; set; }

        public static void Initialize()
        {
            IsCursorVisible = false;
            Title = "New N8Engine Game";
            ConsoleWindow.QuickEditMode = false;
        }
    }
}