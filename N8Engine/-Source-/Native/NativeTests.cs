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
    }
}