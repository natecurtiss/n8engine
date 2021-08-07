using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace N8Engine.SceneManagement
{
    public readonly struct ScenesFile
    {
        private readonly string _path;

        public Scene[] Scenes
        {
            get
            {
                var fileText = File.ReadAllText(_path);
                var eachSceneAsText = GetAListWithIndexesOfEachSetOfSceneDataAsAString(fileText);
                var eachSceneAsTextWithDataSplitUp = GetAnArrayWithIndexesOfEachSceneWithIndexesForEachPieceOfSceneData(eachSceneAsText);
                var scenes = new Scene[eachSceneAsTextWithDataSplitUp.Length];
                for (var i = 0; i < eachSceneAsTextWithDataSplitUp.Length; i++)
                {
                    var currentSceneDataSet = eachSceneAsTextWithDataSplitUp[i];

                    var projectName = currentSceneDataSet[0];
                    var className = currentSceneDataSet[1];
                    var type = Type.GetType(className + $", {projectName}, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                    var invalidTypeException = new InvalidOperationException($"Invalid class or project name in .scenes file at index {i}!");
                    scenes[i] = GetNewSceneOfType(type, invalidTypeException);

                    var name = currentSceneDataSet[2];
                    scenes[i].Name = name;

                    var invalidIndexException = new InvalidOperationException($"Invalid scene index value in .scenes file at index {i}!");
                    var isInvalidIndex = !int.TryParse(currentSceneDataSet[3], out var index);
                    if (isInvalidIndex) throw invalidIndexException;
                    scenes[i].Index = index;
                }
                return scenes;
            }
        }

        public ScenesFile(string path) => _path = path;

        private List<string> GetAListWithIndexesOfEachSetOfSceneDataAsAString(string fileText)
        {
            var eachSceneAsText = fileText.Split('{').ToList();
            for (var index = 0; index < eachSceneAsText.Count; index++)
                eachSceneAsText[index] = eachSceneAsText[index].Replace("}", string.Empty);
            eachSceneAsText.RemoveAll(sceneText => sceneText == string.Empty);
            return eachSceneAsText;
        }

        private string[][] GetAnArrayWithIndexesOfEachSceneWithIndexesForEachPieceOfSceneData(List<string> eachSceneAsText)
        {
            var eachSceneAsTextWithDataSplitUp = new string[eachSceneAsText.Count][];
            for (var i = 0; i < eachSceneAsTextWithDataSplitUp.Length; i++)
                eachSceneAsTextWithDataSplitUp[i] = eachSceneAsText[i].Split(',');
            for (var scene = 0; scene < eachSceneAsTextWithDataSplitUp.Length; scene++)
                for (var data = 0; data < eachSceneAsTextWithDataSplitUp[scene].Length; data++)
                {
                    eachSceneAsTextWithDataSplitUp[scene][data] = eachSceneAsTextWithDataSplitUp[scene][data].Split('"')[1];
                    eachSceneAsTextWithDataSplitUp[scene][data] = eachSceneAsTextWithDataSplitUp[scene][data].Replace(@"""", string.Empty);
                }
            return eachSceneAsTextWithDataSplitUp;
        }

        private Scene GetNewSceneOfType(Type type, Exception invalidTypeException) => Activator.CreateInstance(type ?? throw invalidTypeException) as Scene;
    }
}