using System;
using N8Engine.Inputs;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Application
    {
        public static int FramesPerSecond => GameLoop.FramesPerSecond;

        public static void Start(short fontSize = 5, bool maximizeToFullscreen = false, Action onLaunchedCallback = default, Action onNextFrameCallback = default)
        {
            PathExtensions.Initialize();
            Debug.Initialize();
            Window.Initialize(fontSize);
            SceneManager.Initialize();
            Renderer.Initialize();
            Input.Initialize();
            onLaunchedCallback?.Invoke();
            GameLoop.Run(onNextFrameCallback);
        }
    }
}