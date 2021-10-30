using N8Engine.Debugging;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class Launcher
    {
        public virtual ILogger CustomLogger { get; } = null;
        public abstract string WindowTitle { get; }
        public abstract ScreenResolution WindowSize { get; }
        public abstract Scene[] Scenes { get; }
    }
}