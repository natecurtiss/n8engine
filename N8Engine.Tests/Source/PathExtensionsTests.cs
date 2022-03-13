using N8Engine.Utilities;
using NUnit.Framework;
using static NUnit.Framework.FileAssert;

namespace N8Engine.Tests;

sealed class PathExtensionsTests
{
    [Test]
    public void TestFind()
    {
        var path = "Assets/dummy.txt";
        Exists(path.Find());
    }
}