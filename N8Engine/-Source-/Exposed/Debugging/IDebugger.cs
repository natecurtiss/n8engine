using JetBrains.Annotations;

namespace N8Engine.Debugging
{
    public interface IDebugger
    {
        void Log([CanBeNull] object message);
    }
}