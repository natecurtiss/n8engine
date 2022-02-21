using N8Engine.InputSystem;
using NUnit.Framework;

namespace N8Engine.Tests;

sealed class InputSystemTests
{
    const Key KEY = Key.A;

    [Test]
    public void TestIsKeyPressed()
    {
        var input = new Input();
        input.UpdateKey(KEY, true);
        Assert.IsTrue(input.IsPressed(KEY));
    }
    
    [Test]
    public void TestWasKeyJustPressed()
    {
        var input = new Input();
        input.UpdateKey(KEY, true);
        Assert.IsTrue(input.WasJustPressed(KEY));
        input.UpdateKey(KEY, false);
        input.UpdateKey(KEY, true);
        Assert.IsTrue(input.WasJustPressed(KEY));
    }
    
    [Test]
    public void TestIsKeyReleased()
    {
        var input = new Input();
        Assert.IsTrue(input.IsReleased(KEY));
        input.UpdateKey(KEY, false);
        Assert.IsTrue(input.IsReleased(KEY));
    }
    
    [Test]
    public void TestWasKeyJustReleased()
    {
        var input = new Input();
        input.UpdateKey(KEY, true);
        input.UpdateKey(KEY, false);
        Assert.IsTrue(input.WasJustReleased(KEY));
    }
}