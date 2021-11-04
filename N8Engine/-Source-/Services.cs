using N8Engine.Debugging;
using N8Engine.Inputs;
using N8Engine.Internal;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Services
    {
        public static IDebugger Debug { get; internal set; }
        public static IWindow Window { get; internal set; }
        public static ISceneManager SceneManager { get; internal set; }
        public static IInput Input { get; internal set; }
    }
}