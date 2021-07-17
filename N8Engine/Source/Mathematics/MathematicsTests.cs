using NUnit.Framework;

namespace N8Engine.Mathematics
{
    internal sealed class MathematicsTests
    {
        [Test]
        public void TestRectangleIsPositionInside()
        {
            Rectangle __first = new(Vector.One * 3);
            Assert.IsTrue(__first.IsPositionInside(Vector.Left));
        }

        [Test]
        public void TestRectangleIsOverlapping()
        {
            Rectangle __first = new(Vector.One * 100);
            Rectangle __second = new(Vector.One * 3);
            Assert.IsTrue(__first.IsOverlapping(__second));
            Rectangle __third = new(Vector.One * 2, Vector.Left * 2);
            Assert.IsTrue(__second.IsOverlapping(__third));
        }
    }
}