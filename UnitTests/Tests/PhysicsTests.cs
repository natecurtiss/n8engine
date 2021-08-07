using N8Engine.Mathematics;
using N8Engine.Physics;
using NUnit.Framework;

namespace N8Engine.Tests
{
    internal sealed class PhysicsTests
    {
        [Test]
        public void TestBoundingBoxIsPositionInside()
        {
            var rectangle = new BoundingBox(Vector.One * 3, Vector.Zero);
            Assert.IsTrue(rectangle.IsPositionInside(Vector.Left));
        }

        [Test]
        public void TestBoundingBoxIsOverlapping()
        {
            var firstRectangle = new BoundingBox(Vector.One * 100, Vector.Zero);
            var secondRectangle = new BoundingBox(Vector.One * 3, Vector.Zero);
            Assert.IsTrue(firstRectangle.IsOverlapping(secondRectangle));
            var thirdRectangle = new BoundingBox(Vector.One * 2, Vector.Left);
            Assert.IsTrue(secondRectangle.IsOverlapping(thirdRectangle));
        }
    }
}