using System.Collections.Generic;

namespace N8Engine.SceneManagement
{
    public abstract class Scene
    {
        internal readonly List<GameObject> GameObjects = new();

        public string Name { get; internal set; }
        public int Index { get; internal set; }

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