using System;
using N8Engine.Inputs;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static class Application
    {
        public static event Action OnLaunch;
        
        /// <summary>
        /// Starts the application - MUST be called externally in the main method.
        /// </summary>
        public static void Start()
        {
            Debug.Initialize();
            Window.Initialize();
            SceneManager.Initialize();
            Renderer.Initialize();
            Input.Initialize();
            OnLaunch?.Invoke();
            GameLoop.Run();
        }
    }
}