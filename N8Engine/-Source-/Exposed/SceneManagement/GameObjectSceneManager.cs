using N8Engine.Internal;
using N8Engine.Mathematics;

namespace N8Engine.SceneManagement
{
    sealed class GameObjectSceneManager : ISceneManager
    {
        public Scene CurrentScene { get; private set; }
        readonly Scene[] _scenes;

        internal GameObjectSceneManager(Scene[] scenes)
        {
            _scenes = scenes;
            for (var i = 0; i < _scenes.Length; i++)
                _scenes[i].Index = i;
        }

        void ISceneManager.LoadScene(string name)
        {
            foreach (var scene in _scenes)
                if (scene.Name == name)
                {
                    LoadScene(scene);
                    break;
                }
        }

        void ISceneManager.LoadScene(int index) => LoadScene(index);
        void ISceneManager.LoadScene(Scene scene) => LoadScene(scene);

        void ISceneManager.LoadNextScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var nextSceneIndex = (currentSceneIndex + 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(nextSceneIndex);
        }
        
        void ISceneManager.LoadPreviousScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var previousSceneIndex = (currentSceneIndex - 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(previousSceneIndex);
        }

        void ISceneManager.LoadCurrentScene() => LoadScene(CurrentScene);
        void ISceneManager.LoadFirstScene() => LoadScene(_scenes[0]);

        void LoadScene(Scene scene)
        {
            CurrentScene?.Unload();
            CurrentScene = scene;
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