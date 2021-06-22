using N8Engine.Core;
using N8Engine.Inputs;
using N8Engine.Rendering;

Application.Start();

namespace N8Engine.Core
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    internal static class Application
    {
        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            Window.Initialize();
            Input.Initialize();
            GameLoop.Run();
        }
    }
}