using System;
using JetBrains.Annotations;
using N8Engine.Debugging;
using N8Engine.External;
using N8Engine.External.Console;
using N8Engine.Inputs;
using N8Engine.Internal;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using static N8Engine.InternalServices;
using static N8Engine.Services;


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
            InternalServices.InternalEvents = new InternalEvents();
            InternalServices.UpdateEvents = new UpdateEvents();
            InternalServices.RenderingEvents = new RenderingEvents();
            
            Debug = new CustomDebugger(launcher.CustomLogger);
            Renderer = new ConsoleRenderer((short) launcher.FontSize, _windowHandle, launcher.WindowSize);
            Window = new NonResizableWindow(launcher.WindowTitle, launcher.WindowSize, _windowHandle);
            SceneManager = new GameObjectSceneManager(launcher.Scenes);
            // TODO: make this cross-platform.
            Input = new WindowsInputHandler();
        }

        public void Start()
        {
            // TODO: this is hella messy so probably clean it up later.
            Window.OnBackgroundChanged += Renderer.ChangeBackground;
            InternalServices.InternalEvents.OnPreUpdate += Input.CheckInput;
            InternalServices.RenderingEvents.OnRender += Renderer.DisplayPixels;
            
            // TODO: see above TODO.
            if (_isRunning) return;
            _isRunning = true;
            _gameLoop.Run(() =>
            {
                Window.Show();
                SceneManager.LoadFirstScene();
            }, deltaTime =>
            {
                // TODO: maybe this stuff goes in the scene manager/scene or something?
                InternalServices.UpdateEvents.Invoke(deltaTime);
                InternalServices.RenderingEvents.Invoke(Renderer);
            });
            
            // TODO: see above TODO as well.
            Window.OnBackgroundChanged -= Renderer.ChangeBackground;
            InternalServices.InternalEvents.OnPreUpdate -= Input.CheckInput;
            InternalServices.RenderingEvents.OnRender -= Renderer.DisplayPixels;
        }
    }
}