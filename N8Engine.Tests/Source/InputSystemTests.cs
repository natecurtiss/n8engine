using N8Engine.InputSystem;
using NUnit.Framework;
using GLKey = Silk.NET.Input.Key;

namespace N8Engine.Tests;

// TODO: Refactor.
sealed class InputSystemTests
{
    const Key KEY = Key.A;
    const Key ANY = Key.Any;
    const GLKey GL_KEY = GLKey.A;

    [Test]
    public void TestIsKeyPressed()
    {
        var input = new Input();
        input.UpdateKey(KEY, true);
        Assert.IsTrue(input.IsPressed(KEY));
        Assert.IsTrue(input.IsPressed(ANY));
    }
    
    [Test]
    public void TestWasKeyJustPressed()
    {
        var input = new Input();
        input.UpdateKey(KEY, true);
        Assert.IsTrue(input.WasJustPressed(KEY));
        Assert.IsTrue(input.WasJustPressed(ANY));
        input.UpdateKey(KEY, false);
        input.UpdateKey(KEY, true);
        Assert.IsTrue(input.WasJustPressed(KEY));
        Assert.IsTrue(input.WasJustPressed(ANY));
    }
    
    [Test]
    public void TestIsKeyReleased()
    {
        var input = new Input();
        Assert.IsTrue(input.IsReleased(KEY));
        Assert.IsTrue(input.IsReleased(ANY));
        input.UpdateKey(KEY, false);
        Assert.IsTrue(input.IsReleased(KEY));
        Assert.IsTrue(input.IsReleased(ANY));
    }
    
    [Test]
    public void TestWasKeyJustReleased()
    {
        var input = new Input();
        input.UpdateKey(KEY, true);
        input.UpdateKey(KEY, false);
        Assert.IsTrue(input.WasJustReleased(KEY));
        Assert.IsTrue(input.WasJustReleased(ANY));
    }

    [Test]
    public void TestKeyConversions() => Assert.IsTrue(GL_KEY.AsKey() == KEY);
}