using N8Engine.Mathematics;
using NUnit.Framework;

namespace N8Engine.Physics
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

        [Test]
        public void TestBoundingBoxSides()
        {
            var rectangle = new BoundingBox(Vector.One * 2f, Vector.Zero);
            Assert.AreEqual(new Vector(-1f ,0f), rectangle.Left);
            Assert.AreEqual(new Vector(1f ,0f), rectangle.Right);
            Assert.AreEqual(new Vector(0f ,-1f), rectangle.Bottom);
            Assert.AreEqual(new Vector(0f ,1f), rectangle.Top);
        }
    }
}