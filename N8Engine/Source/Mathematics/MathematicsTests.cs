using NUnit.Framework;

namespace N8Engine.Mathematics
{
    internal sealed class MathematicsTests
    {
        [Test]
        public void TestRectangleIsPositionInside()
        {
            var rectangle = new Rectangle(Vector.One * 3, (StaticObject) Vector.Zero);
            Assert.IsTrue(rectangle.IsPositionInside(Vector.Left));
        }

        [Test]
        public void TestRectangleIsOverlapping()
        {
            var firstRectangle = new Rectangle(Vector.One * 100, (StaticObject) Vector.Zero);
            var secondRectangle = new Rectangle(Vector.One * 3, (StaticObject) Vector.Zero);
            Assert.IsTrue(firstRectangle.IsOverlapping(secondRectangle));
            var thirdRectangle = new Rectangle(Vector.One * 2, (StaticObject) Vector.Left);
            Assert.IsTrue(secondRectangle.IsOverlapping(thirdRectangle));
        }
    }
}