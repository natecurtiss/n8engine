using N8Engine.Internal;
using N8Engine.Mathematics;

namespace N8Engine.SceneManagement
{
    public sealed class SceneManager
    {
        public Scene CurrentScene { get; private set; }
        readonly Scene[] _scenes;
        readonly IInternalEvents _internalEvents;

        internal SceneManager(Scene[] scenes, IInternalEvents internalEvents)
        {
            _scenes = scenes;
            _internalEvents = internalEvents;
            for (var i = 0; i < _scenes.Length; i++)
                _scenes[i].Index = i;
            _internalEvents.OnInternalStart += OnStart;
        }

        public void LoadScene(string name)
        {
            foreach (var scene in _scenes)
                if (scene.Name == name)
                {
                    LoadScene(scene);
                    break;
                }
        }

        public void LoadScene(int index)
        {
            foreach (var scene in _scenes)
                if (scene.Index == index)
                {
                    LoadScene(scene);
                    break;
                }
        }

        public void LoadNextScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var nextSceneIndex = (currentSceneIndex + 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(nextSceneIndex);
        }
        
        public void LoadPreviousScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var previousSceneIndex = (currentSceneIndex - 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(previousSceneIndex);
        }

        public void LoadCurrentScene() => LoadScene(CurrentScene);

        void LoadScene(Scene scene)
        {
            // CurrentScene?.Unload();
            // CurrentScene = scene;
            // scene.Load();
        }

        void OnStart()
        {
            LoadScene(_scenes[0]);
            _internalEvents.OnInternalStart -= OnStart;
        }
    }
}