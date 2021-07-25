using System;
using System.IO;
using N8Engine.Mathematics;
using N8Engine;

namespace N8Engine.SceneManagement
{
    public static class SceneManager
    {
        internal static Scene CurrentScene { get; private set; }
        private static Scene[] _scenes;

        internal static void Initialize()
        {
            var allScenesFiles = Directory.GetFiles(PathExtensions.PathToRootFolder, "*.scenes", SearchOption.AllDirectories);
            var userScenesFile = string.Empty;
            foreach (var scenesFile in allScenesFiles)
                if (!scenesFile.Contains("ignore.scenes"))
                {
                    userScenesFile = scenesFile;
                    break;
                }
            // var userScenesFile = Directory.GetFiles(PathExtensions.PathToRootFolder, "ignore.scenes", SearchOption.AllDirectories)[0];
            try
            {
                _scenes = new ScenesFile(userScenesFile).Scenes;
            }
            catch (Exception)
            {
                throw new FileNotFoundException("No .scenes file was found, please add one :)");
            }
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