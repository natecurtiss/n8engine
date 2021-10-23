using System.Runtime.Versioning;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    /// <summary>
    /// I suggest googling the word "application" if you do not know what it means.
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// Get-only property that returns the frames per second of the game, which is capped at 60fps.
        /// </summary>
        public static int FramesPerSecond => GameLoop.FramesPerSecond;

        /// <summary>
        /// Call this from the main method to start your game.
        /// </summary>
        /// <param name="launcher"> Ideally, the class that contains the main method should inherit from <see cref="Launcher"/> and pass itself in to this method.</param>
        [SupportedOSPlatform("windows")]
        public static void Start(Launcher launcher)
        {
            Debug.Initialize(launcher);
            Window.Initialize(launcher.FontSize);
            SceneManager.Initialize(launcher.Scenes);
            Renderer.Initialize();
            Input.Initialize();
            GameLoop.Run();
        }
    }
}