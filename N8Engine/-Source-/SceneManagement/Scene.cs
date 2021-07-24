using System.Collections.Generic;

namespace N8Engine.SceneManagement
{
    public abstract class Scene
    {
        internal readonly List<GameObject> GameObjects = new();
        internal string Name { get; set; }
        internal int Index { get; set; }

        internal void Load() => OnSceneLoaded();

        internal void Unload()
        {
            foreach (var gameObject in GameObjects) 
                gameObject.Destroy();
            GameObjects.Clear();
        }

        protected abstract void OnSceneLoaded();
    }
}