using System;
using N8Engine.Inputs;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Application
    {
        public static int FramesPerSecond => GameLoop.FramesPerSecond;
        public static int TargetFramerate
        {
            get => GameLoop.TargetFramerate;
            set => GameLoop.TargetFramerate = value;
        }
        public static bool UseExternalErrorConsole
        {
            get => Window.UseExternalErrorConsole;
            set => Window.UseExternalErrorConsole = value;
        }
        
        public static void Start(Action onLaunchedCallback = default, Action onNextFrameCallback = default)
        {
            Debug.Initialize();
            Window.Initialize();
            SceneManager.Initialize();
            Renderer.Initialize();
            Input.Initialize();
            onLaunchedCallback?.Invoke();
            GameLoop.Run(onNextFrameCallback);
        }
    }
}