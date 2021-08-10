using System.IO;
using N8Engine;
using N8Engine.SceneManagement;
using NUnit.Framework;
using TestGame;

namespace UnitTests
{
    internal sealed class SceneManagementTests
    {
        [Test]
        public void TestLoadScenesFromScenesFile()
        {
            var path = Directory.GetFiles(PathExtensions.PathToRootFolder, "ignore.scenes", SearchOption.AllDirectories)[0];
            var expectedScenes = new Scene[]
            {
                new SampleScene
                {
                    Index = 0, Name = "Level 1"
                },
                new SampleScene2
                {
                    Index = 1, Name = "Level 2"
                }
            };
            var actualScenes = new ScenesFile(path).Scenes;

            Assert.AreEqual(expectedScenes[0].Name, actualScenes[0].Name);
            Assert.AreEqual(expectedScenes[1].Index, actualScenes[1].Index);
        }
    }
}