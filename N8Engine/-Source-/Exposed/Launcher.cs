using JetBrains.Annotations;
using N8Engine.Debugging;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class Launcher
    {
        [CanBeNull]
        public virtual ILogger CustomLogger { get; } = null;
        
        public abstract string WindowTitle { get; }
        public abstract ScreenResolution WindowSize { get; }
        [ValueRange(1, 8)]
        public abstract int FontSize { get; }
        
        [ValueRange(0, 200)]
        public abstract int TargetFramerate { get; }
        [NotNull]
        public abstract Scene[] Scenes { get; }
    }
}