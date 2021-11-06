using System;
using N8Engine.Debugging;
using N8Engine.External.Console;
using N8Engine.Inputs;
using N8Engine.Loop;
using N8Engine.Rendering;
using N8Engine.SceneManagement;


namespace N8Engine
{
    public sealed class Game<T> where T : Launcher, new()
    {
        readonly IntPtr _windowHandle = ConsoleInfo.Handle;
        readonly GameLoop _gameLoop;

        public Game()
        {
            var launcher = new T();
            _gameLoop = new GameLoop(launcher.TargetFramerate);
            Services.Setup
            (
                _gameLoop,
                new ConsoleRenderer((short) launcher.FontSize, _windowHandle, launcher.WindowSize),
                new CustomDebugger(launcher.CustomLogger),
                new NonResizableWindow(launcher.WindowTitle, launcher.WindowSize, _windowHandle),
                new GameObjectSceneManager(launcher.Scenes),
                new WindowsInputHandler()
            );
        }

        public void Start() => _gameLoop.Run();
    }
}