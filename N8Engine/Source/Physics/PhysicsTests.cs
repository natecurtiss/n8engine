using N8Engine.Mathematics;
using NUnit.Framework;

namespace N8Engine.Physics
{
    internal sealed class PhysicsTests
    {
        [Test]
        public void TestRectangleIsPositionInside()
        {
            var rectangle = new Rectangle(Vector.One * 3, Vector.Zero);
            Assert.IsTrue(rectangle.IsPositionInside(Vector.Left));
        }

        [Test]
        public void TestRectangleIsOverlapping()
        {
            var firstRectangle = new Rectangle(Vector.One * 100, Vector.Zero);
            var secondRectangle = new Rectangle(Vector.One * 3, Vector.Zero);
            Assert.IsTrue(firstRectangle.IsOverlapping(secondRectangle));
            var thirdRectangle = new Rectangle(Vector.One * 2, Vector.Left);
            Assert.IsTrue(secondRectangle.IsOverlapping(thirdRectangle));
        }

        [Test]
        public void TestRectangleSides()
        {
            var rectangle = new Rectangle(Vector.One * 2f, Vector.Zero);
            Assert.AreEqual(new Vector(-1f ,0f), rectangle.Left);
            Assert.AreEqual(new Vector(1f ,0f), rectangle.Right);
            Assert.AreEqual(new Vector(0f ,-1f), rectangle.Bottom);
            Assert.AreEqual(new Vector(0f ,1f), rectangle.Top);
        }
    }
}