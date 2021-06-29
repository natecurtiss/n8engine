using System;
using N8Engine.Internal;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    /// <summary>
    /// The Console Window used to display the game.
    /// </summary>
    public static class Window
    {
        /// <summary>
        /// The title of the window.
        /// </summary>
        public static string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        public static int PixelSize
        {
            get => _pixelSize;
            set => _pixelSize = value.Clamped(1, 32);
        }
        private static int _pixelSize;
        
        /// <summary>
        /// Initializes the window.
        /// </summary>
        internal static void Initialize()
        {
            Title = "New N8Engine Game";
            ConsoleQuickEditMode.Enabled = false;
            ConsoleText.SetCurrentFont("Arial", 1);
            ConsoleResizing.Maximize();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▒▒▒▒▒▒▒▒▒");
            Console.CursorVisible = false;
        }
    }
}