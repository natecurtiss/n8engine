using System.IO;
using NUnit.Framework;

namespace N8Engine.Native
{
    internal sealed class NativeTests
    {
        [Test]
        public void TestPathToRootFolder()
        {
            var pathToRootFolder = PathExtensions.PathToRootFolder;
            Assert.AreEqual(@"C:\Users\NateDawg\RiderProjects\N8Engine", pathToRootFolder);
        }

        [Test]
        public void TestPathToProjectAnchor()
        {
            var pathToRootFolder = PathExtensions.PathToRootFolder;
            var expectedPathToAnchor = pathToRootFolder + @"\N8Engine\-Source-";
            var actualPathToAnchor = PathExtensions.PathToProjectAnchor;
            Assert.AreEqual(expectedPathToAnchor, actualPathToAnchor);
        }
    }
}