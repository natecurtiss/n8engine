using NUnit.Framework;

namespace N8Engine.Mathematics
{
    internal sealed class MathematicsTests
    {
        [Test]
        public void TestRectangleIsPositionInside()
        {
            var rectangle = new Rectangle(Vector.One * 3);
            Assert.IsTrue(rectangle.IsPositionInside(Vector.Left));
        }

        [Test]
        public void TestRectangleIsOverlapping()
        {
            var firstRectangle = new Rectangle(Vector.One * 100);
            var secondRectangle = new Rectangle(Vector.One * 3);
            Assert.IsTrue(firstRectangle.IsOverlapping(secondRectangle));
            var thirdRectangle = new Rectangle(Vector.One * 2, Vector.Left * 2);
            Assert.IsTrue(secondRectangle.IsOverlapping(thirdRectangle));
        }
    }
}