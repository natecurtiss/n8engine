using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class Launcher
    {
        public abstract Scene[] Scenes { get; }
        public abstract short CameraSize { get; }
        public abstract string PathToDebugLogsFile { get; }

        public virtual void OnLaunched() { }
        
        public virtual void OnEveryFrame() { }
    }

}