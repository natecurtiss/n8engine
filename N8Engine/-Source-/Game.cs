using N8Engine.External;
using N8Engine.External.Console;
using N8Engine.Internal;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public sealed class Game<T> where T : Launcher, new()
    {
        readonly GameLoop _gameLoop;
        readonly Window _window;
        readonly SceneManager _sceneManager;
        
        public Game()
        {
            var launcher = new T();
            _gameLoop = new GameLoop(launcher.TargetFramerate);
            _window = new Window(launcher.WindowTitle, launcher.WindowSize.Size, ConsoleInfo.Handle, _gameLoop);
            _sceneManager = new SceneManager(launcher.Scenes, _gameLoop);
        }

        public void Start() => _gameLoop.Run();
    }
}