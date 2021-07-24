using N8Engine;
using N8Engine.Debugging;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

Application.Start();

namespace N8Engine
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
            Debug.Initialize();
            SceneManager.Initialize();
            Renderer.Initialize();
            Window.Initialize();
            GameLoop.Run();
        }
    }
}