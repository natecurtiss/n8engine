using System;
using N8Engine.External;
using N8Engine.External.Console;
using N8Engine.Internal;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public sealed class Game<T> where T : Launcher, new()
    {
        readonly IntPtr _windowHandle = ConsoleInfo.Handle;
        
        readonly GameLoop _gameLoop;
        readonly Window _window;
        readonly IRenderer _renderer;
        readonly SceneManager _sceneManager;

        public Game()
        {
            var launcher = new T();
            _gameLoop = new GameLoop(launcher.TargetFramerate);
            _window = new Window(launcher.WindowTitle, launcher.WindowSize, _windowHandle, _gameLoop);
            _renderer = new ConsoleRenderer((short) launcher.FontSize, _gameLoop);
            _sceneManager = new SceneManager(launcher.Scenes, _gameLoop);
        }

        public void Start() => _gameLoop.Run();
    }
}