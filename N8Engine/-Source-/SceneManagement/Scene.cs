using System.Collections;
using System.Collections.Generic;

namespace N8Engine.SceneManagement
{
    public abstract class Scene : IEnumerable<GameObject>
    {
        private readonly List<GameObject> _gameObjects = new();

        // TODO set these somewhere.
        public int Index { get; }
        public string Name { get; }
        
        public IEnumerator<GameObject> GetEnumerator() => _gameObjects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void Add(GameObject gameObject) => _gameObjects.Add(gameObject);

        public void Remove(GameObject gameObject) => _gameObjects.Remove(gameObject);

        internal void Load() => OnSceneLoaded();

        internal void Unload()
        {
            foreach (var gameObject in _gameObjects.ToArray()) 
                gameObject.Destroy();
        }
        
        protected abstract void OnSceneLoaded();
    }
}