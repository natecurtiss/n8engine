using System.IO;
using N8Engine.Debugging;
using N8Engine.Inputs;
using N8Engine.Loop;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Services
    {
        public static readonly string PathToProject = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
        
        public static IDebugger Debug { get; private set; }
        public static IWindow Window { get; private set; }
        public static ISceneManager SceneManager { get; private set; }
        public static IInput Input { get; private set; }

        internal static void Setup(ILoopEvents loopEvents, IRenderer renderer, IDebugger debugger, IWindow window, ISceneManager sceneManager, IInput input)
        {
            Debug = debugger;
            Window = window;
            SceneManager = sceneManager;
            Input = input;
            loopEvents.OnStart += Window.Show;
            loopEvents.OnStart += SceneManager.LoadFirstScene;
            loopEvents.OnUpdate += _ => Input.CheckInput();
            loopEvents.OnUpdate += deltaTime => SceneManager.UpdateCurrentScene(deltaTime, renderer);
            loopEvents.OnUpdate += _ => renderer.DisplayPixels();
            Window.OnBackgroundChanged += renderer.ChangeBackground;
        }
    }
}