using N8Engine.Objects;

namespace N8Engine.SceneManagement
{
    public readonly struct Scene
    {
        public readonly string Name;
        public readonly string Path;
        public readonly GameObject[] GameObjects;

        public Scene(in string name, in string path, in GameObject[] gameObjects)
        {
            Name = name;
            Path = path;
            GameObjects = gameObjects;
        }
    }
}