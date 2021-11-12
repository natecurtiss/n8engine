using N8Engine.SceneManagement;

namespace N8Engine
{
    public static class Application
    {
        public static Game Build(int targetFps, params Scene[] scenes) => new
        (
            targetFps,
            new SceneManager(scenes),
            new GameObjectEvents()
        );
    }
}