using System;
using JetBrains.Annotations;
using N8Engine.Debugging;
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
        readonly IDebugger _debug;
        readonly IWindow _window;
        readonly ISceneManager _sceneManager;
        readonly IRenderer _renderer;
        
        bool _isRunning;

        public Game()
        {
            // Make sure to register the service after creating it to avoid nullref exceptions when cross-referencing services on creation.
            var launcher = new T();
            _gameLoop = new GameLoop(launcher.TargetFramerate);
            
            // This is the proper way to create a new service,
            _debug = new CustomDebugger(launcher.CustomLogger);
            Services.Debug = _debug;
            
            _window = new Window(launcher.WindowTitle, launcher.WindowSize, _windowHandle, _gameLoop.InternalEvents);
            Services.Window = _window;
            
            _sceneManager = new SceneManager(launcher.Scenes, _gameLoop.InternalEvents);
            Services.SceneManager = _sceneManager;
            
            _renderer = new ConsoleRenderer((short) launcher.FontSize, _windowHandle, launcher.WindowSize, _gameLoop.RenderingEvents);
        }

        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;
            _gameLoop.Run();
        }
    }
}