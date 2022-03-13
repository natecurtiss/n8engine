using System.Numerics;
using N8Engine.Utilities;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace N8Engine.Tests;

sealed class BoundsTests
{
    Bounds _bounds;

    [SetUp]
    public void Setup() => _bounds = new(Vector2.Zero, Vector2.One * 100);

    [Test]
    [TestCase(0, 0, true)]
    [TestCase(-5, 3, true)]
    [TestCase(-50, 50, true)]
    [TestCase(-26, 48, true)]
    [TestCase(0, 100, false)]
    [TestCase(-51, 51, false)]
    [TestCase(-500000, 2, false)]
    [TestCase(10000, 10000, false)]
    public void TestBoundsContains(int x, int y, bool shouldContain)
    {
        var point = new Vector2(x, y);
        AreEqual(_bounds.Contains(point), shouldContain);
    }

    [Test]
    public void TestBoundsAdd()
    {
        var point = new Vector2(100, 100);
        _bounds.Add(point);
        IsTrue(_bounds.Contains(point));
    }
}