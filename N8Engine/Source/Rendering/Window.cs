using System;
using System.Collections.Generic;
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
        /// <summary>
        /// The width of the window.
        /// </summary>
        public static int Width
        {
            get => Console.WindowWidth;
            set => Console.WindowWidth = value;
        }
        /// <summary>
        /// The height of the window.
        /// </summary>
        public static int Height
        {
            get => Console.WindowHeight;
            set => Console.WindowHeight = value;
        }
        /// <summary>
        /// Hides the cursor if false.
        /// </summary>
        public static bool IsCursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }
        /// <summary>
        /// The color of the Console background.
        /// </summary>
        public static Color BackgroundColor { get; set; }

        internal static readonly Dictionary<Vector2, int> Cells = new();

        /// <summary>
        /// Initializes the window.
        /// </summary>
        internal static void Initialize()
        {
            IsCursorVisible = false;
            Title = "New N8Engine Game";
            ConsoleWindow.QuickEditMode = false;
        }

        internal static void AddToRenderQueue(in Sprite sprite, Vector2 position)
        {
            position = position.AsConsolePosition();

        }

        internal static Vector2 AsConsolePosition(this in Vector2 position)
        {
            Vector2 __zero = new(Width / 2f, Height / 2f);
            return __zero + position;
        }
    }
}