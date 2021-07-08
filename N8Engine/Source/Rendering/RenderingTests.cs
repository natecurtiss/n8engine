using NUnit.Framework;

namespace N8Engine.Rendering
{
    internal sealed class RenderingTests
    {
        [Test]
        public void TestReadN8SpriteFile()
        {
            N8SpriteFile __file = new();
            string[] __pixels = __file.ReadFile("{Test}{Hello}{Wow}");
            Assert.AreEqual("Test", __pixels[0]);
        }
    }
}