using N8Engine.Inputs;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Application
    {
        public static int FramesPerSecond => GameLoop.FramesPerSecond;

        public static void Start(Launcher launcher)
        {
            Debug.Initialize(launcher.PathToDebugLogsFile);
            Window.Initialize(launcher.CameraSize);
            SceneManager.Initialize(launcher.Scenes);
            Renderer.Initialize();
            Input.Initialize();
            launcher.OnLaunched();
            GameLoop.Run(launcher.OnEveryFrame);
        }
    }
}