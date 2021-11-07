using System.Collections.Generic;
using System.Linq;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.SceneManagement
{
    sealed class SceneManager : ISceneManager<GameObject>
    {
        readonly IScene<GameObject>[] _scenes;
        IScene<GameObject> _currentScene;
        
        IScene<GameObject> ISceneManager<GameObject>.CurrentScene => _currentScene;

        internal SceneManager(IScene<GameObject>[] scenes)
        {
            _scenes = scenes;
            for (var i = 0; i < _scenes.Length; i++)
                _scenes[i].Index = i;
        }

        void ISceneManager<GameObject>.LoadScene(string name)
        {
            foreach (var scene in _scenes)
                if (scene.Name == name)
                {
                    LoadScene(scene);
                    break;
                }
        }

        void ISceneManager<GameObject>.LoadScene(int index) => LoadScene(index);
        void ISceneManager<GameObject>.LoadScene(IScene<GameObject> scene) => LoadScene(scene);

        void ISceneManager<GameObject>.LoadNextScene()
        {
            var currentSceneIndex = _currentScene.Index;
            var nextSceneIndex = (currentSceneIndex + 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(nextSceneIndex);
        }
        
        void ISceneManager<GameObject>.LoadPreviousScene()
        {
            var currentSceneIndex = _currentScene.Index;
            var previousSceneIndex = (currentSceneIndex - 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(previousSceneIndex);
        }

        void ISceneManager<GameObject>.LoadCurrentScene() => LoadScene(_currentScene);
        void ISceneManager<GameObject>.LoadFirstScene() => LoadScene(_scenes[0]);

        void ISceneManager<GameObject>.UpdateCurrentScene(float deltaTime, IRenderer renderer)
        {
            var gameObjects = _currentScene.ToArray();
            foreach (var gameObject in gameObjects)
                gameObject.OnUpdate(deltaTime);
            foreach (var gameObject in gameObjects)
                gameObject.OnPhysicsUpdate(deltaTime);
            foreach (var gameObject in gameObjects)
                gameObject.OnLateUpdate(deltaTime);
            foreach (var gameObject in gameObjects)
                gameObject.OnPreRender(renderer);
        }

        void ISceneManager<GameObject>.AddToCurrentScene(GameObject gameObject) => _currentScene.Add(gameObject);
        void ISceneManager<GameObject>.RemoveFromCurrentScene(GameObject gameObject) => _currentScene.Remove(gameObject);

        void LoadScene(IScene<GameObject> scene)
        {
            _currentScene?.Unload();
            _currentScene = scene;
            scene.Load();
        }
        
        void LoadScene(int index)
        {
            foreach (var scene in _scenes)
                if (scene.Index == index)
                {
                    LoadScene(scene);
                    break;
                }
        }
    }
}