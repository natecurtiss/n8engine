using System;

namespace N8Engine
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
    }
}