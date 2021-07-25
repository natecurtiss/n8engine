using System.IO;
using N8Engine.Mathematics;
using Shared;

namespace N8Engine.SceneManagement
{
    public static class SceneManager
    {
        internal static Scene CurrentScene { get; private set; }
        private static Scene[] _scenes;

        internal static void Initialize()
        {
            var scenesFilePath = Directory.GetFiles(PathExtensions.PathToRootFolder, "*.scenes", SearchOption.AllDirectories)[0];
            _scenes = new ScenesFile(scenesFilePath).Scenes;
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
            var nextSceneIndex = (currentSceneIndex + 1).Clamped(0, _scenes.Length - 1);
            LoadScene(nextSceneIndex);
        }
        
        public static void LoadPreviousScene()
        {
            var currentSceneIndex = CurrentScene.Index;
            var previousSceneIndex = (currentSceneIndex - 1).Clamped(0, _scenes.Length - 1);
            LoadScene(previousSceneIndex);
        }

        public static void LoadCurrentScene() => LoadScene(CurrentScene);

        private static void LoadScene(Scene scene)
        {
            CurrentScene?.Unload();
            CurrentScene = scene;
            scene.Load();
        }
    }
}