using N8Engine.External;
using N8Engine.External.Console;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class Game
    {
        readonly Window _window;
        
        public Game(Launcher launcher)
        {
            _window = new Window(launcher.WindowTitle, launcher.WindowSize.Size, ConsoleInfo.Handle);
        }

        public void Start()
        {
            _window.Show();
        }
    }
}