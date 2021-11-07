using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace N8Engine.SceneManagement
{
    public abstract class Scene : IScene<GameObject>, IEnumerable<GameObject>
    {
        readonly List<GameObject> _gameObjects = new();

        string IScene<GameObject>.Name => Name;
        int IScene<GameObject>.Index { get; set; }
        string Name { get; init; }
        
        protected Scene() { }

        public static Scene Create<T>(string name) where T : Scene, new()
        {
            var scene = new T
            {
                Name = name
            };
            return scene;
        }
        
        public IEnumerator<GameObject> GetEnumerator() => _gameObjects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        void IScene<GameObject>.Add(GameObject gameObject) => _gameObjects.Add(gameObject);
        void IScene<GameObject>.Remove(GameObject gameObject) => _gameObjects.Remove(gameObject);
        
        void IScene<GameObject>.Load() => OnSceneLoaded();

        void IScene<GameObject>.Unload()
        {
            foreach (var gameObject in _gameObjects.ToArray()) 
                gameObject.Destroy();
        }

        GameObject[] IScene<GameObject>.ToArray() => this.ToArray();
        
        protected abstract void OnSceneLoaded();
    }
}