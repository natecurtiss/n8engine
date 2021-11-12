using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Application
    {
        public static Game Build(int targetFps, string title, IntVector windowSize, params Scene[] scenes) => new
        (
            targetFps,
            new GameWindow(title, windowSize),
            new SceneManager(scenes),
            new GameObjectEvents()
        );
    }
}