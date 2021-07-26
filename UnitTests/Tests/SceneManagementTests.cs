using System.IO;
using N8Engine.SceneManagement;
using NUnit.Framework;

namespace N8Engine.Tests
{
    internal sealed class SceneManagementTests
    {
        [Test]
        public void TestLoadScenesFromScenesFile()
        {
            var path = Directory.GetFiles(PathExtensions.PathToRootFolder, "internal.scenes", SearchOption.AllDirectories)[0];
            var expectedScenes = new Scene[]
            {
                new SampleScene
                {
                    Index = 1, Name = "Sample Scene"
                },
                new SampleScene2
                {
                    Index = 0, Name = "Sample Scene 2"
                }
            };
            var actualScenes = new ScenesFile(path).Scenes;

            Assert.AreEqual(expectedScenes[0].Name, actualScenes[0].Name);
            Assert.AreEqual(expectedScenes[1].Index, actualScenes[1].Index);
        }
    }
}