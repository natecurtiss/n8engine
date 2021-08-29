using System;
using System.IO;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.SceneManagement
{
    public static class SceneManager
    {
        public static Scene CurrentScene { get; private set; }
        static Scene[] _scenes;

        internal static void Initialize(Scene[] scenes)
        {
            _scenes = scenes;
            LoadScene(_scenes[0]);
        }

        public static void LoadScene(string name)
        {
            foreach (var scene in _scenes)
                if (scene.Name == name)
                {
                    LoadScene(scene);
                    break;
                }
        }

        public static void LoadScene(int index)
        {
            foreach (var scene in _scenes)
                if (scene.Index == index)
                {
                    LoadScene(scene);
                    break;
                }
        }

        public static void LoadNextScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var nextSceneIndex = (currentSceneIndex + 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(nextSceneIndex);
        }
        
        public static void LoadPreviousScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var previousSceneIndex = (currentSceneIndex - 1).ClampedBetween(0, _scenes.Length - 1);
            LoadScene(previousSceneIndex);
        }

        public static void LoadCurrentScene() => LoadScene(CurrentScene);

        static void LoadScene(Scene scene)
        {
            CurrentScene?.Unload();
            CurrentScene = scene;
            scene.Load();
        }
    }
}