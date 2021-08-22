using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class Launcher
    {
        public abstract Scene[] Scenes { get; }
        public abstract short FontSize { get; }
        public abstract string PathToDebugLogsFile { get; }
        
        public virtual void OnFirstDebugFrame() { }
        public virtual void OnEveryDebugFrame() { }
        
        public virtual void OnFirstFrame() { }
        public virtual void OnEveryFrame() { }

        internal void Initialize()
        {
#if DEBUG
            GameLoop.OnStart += OnFirstDebugFrame;
            GameLoop.OnUpdate += _ => OnEveryDebugFrame();
#else
            GameLoop.OnStart += OnFirstFrame;
            GameLoop.OnUpdate += _ => OnEveryFrame();
#endif
        }
    }

}