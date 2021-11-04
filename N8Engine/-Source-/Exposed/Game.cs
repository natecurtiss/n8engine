using System;
using JetBrains.Annotations;
using N8Engine.Debugging;
using N8Engine.External;
using N8Engine.External.Console;
using N8Engine.Inputs;
using N8Engine.Internal;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public sealed class Game<T> where T : Launcher, new()
    {
        readonly IntPtr _windowHandle = ConsoleInfo.Handle;
        readonly GameLoop _gameLoop;

        bool _isRunning;

        public Game()
        {
            // To future readers (probably just nate) I'm sorry for everything terrible I've done in this class.
            // Please clean this up without breaking everything.
            // The idea behind this is to reduce coupling between the service classes and use this as a sort of "controller"
            // to bring them all together, but I'm not sure a massive Service Locator is the answer.
            var launcher = new T();
            _gameLoop = new GameLoop(launcher.TargetFramerate);

            // TODO: rename these or something because the static import doesn't work.
            Services.InternalEvents = new InternalEvents();
            Services.UpdateEvents = new UpdateEvents();
            Services.RenderingEvents = new RenderingEvents();
            
            Services.Debug = new CustomDebugger(launcher.CustomLogger);
            Services.Renderer = new ConsoleRenderer((short) launcher.FontSize, _windowHandle, launcher.WindowSize);
            Services.Window = new NonResizableWindow(launcher.WindowTitle, launcher.WindowSize, _windowHandle);
            Services.SceneManager = new GameObjectSceneManager(launcher.Scenes);
            // TODO: make this cross-platform.
            Services.Input = new WindowsInputHandler();
            
            // TODO: this is hella messy so probably clean it up later.
            Services.Window.OnBackgroundChanged += Services.Renderer.ChangeBackground;
            Services.InternalEvents.OnPreUpdate += Services.Input.CheckInput;
            Services.RenderingEvents.OnRender += Services.Renderer.DisplayPixels;
        }

        ~Game()
        {
            // TODO: see above TODO.
            Services.Window.OnBackgroundChanged -= Services.Renderer.ChangeBackground;
            Services.InternalEvents.OnPreUpdate -= Services.Input.CheckInput;
            Services.RenderingEvents.OnRender -= Services.Renderer.DisplayPixels;
        }

        public void Start()
        {
            // TODO: see above TODO as well.
            if (_isRunning) return;
            _isRunning = true;
            _gameLoop.Run(() =>
            {
                Services.Window.Show();
                Services.SceneManager.LoadFirstScene();
            }, deltaTime =>
            {
                // TODO: maybe this stuff goes in the scene manager/scene or something?
                Services.UpdateEvents.Invoke(deltaTime);
                Services.RenderingEvents.Invoke(Services.Renderer);
            });
        }
    }
}