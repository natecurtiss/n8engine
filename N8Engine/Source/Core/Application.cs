using System;
using System.Threading.Tasks;
using N8Engine;
using N8Engine.Inputs;
using N8Engine.Rendering;

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
            Window.Initialize();
            Renderer.Initialize();
            DummyGame.Start();
            GameLoop.Run();
        }
    }
}