using System;
using System.Linq;
using N8Engine.InputSystem;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using GLKey = Silk.NET.Input.Key;

namespace N8Engine.Tests;

sealed class InputSystemTests
{
    Input _input = null!;

    [SetUp]
    public void Setup() => _input = new();

    [Test]
    public void TestAllKeysReleasedInitially() => IsTrue(Enum.GetValues<Key>().Where(k => k != Key.Unknown && _input.IsReleased(k)).ToList().Count == Enum.GetValues<Key>().Length - 1);
    [Test]
    public void TestNoKeysPressedInitially() => IsTrue(Enum.GetValues<Key>().Where(k => k != Key.Unknown && !_input.IsPressed(k)).ToList().Count == Enum.GetValues<Key>().Length - 1);
    [Test]
    public void TestNoKeysJustReleasedInitially() => IsTrue(Enum.GetValues<Key>().Where(k => k != Key.Unknown && !_input.WasJustReleased(k)).ToList().Count == Enum.GetValues<Key>().Length - 1);
    [Test]
    public void TestNoKeysJustPressedInitially() => IsTrue(Enum.GetValues<Key>().Where(k => k != Key.Unknown && !_input.WasJustPressed(k)).ToList().Count == Enum.GetValues<Key>().Length - 1);

    [Test]
    public void TestIsSpecificKeyPressed()
    {
        _input.UpdateKey(Key.A, true);
        IsTrue(_input.IsPressed(Key.A));
    }
    
    [Test]
    public void TestIsAnyKeyPressed()
    {
        _input.UpdateKey(Key.A, true);
        IsTrue(_input.IsPressed(Key.Any));
    }
    
    [Test]
    public void TestWasSpecificKeyJustPressed()
    {
        _input.UpdateKey(Key.A, true);
        IsTrue(_input.WasJustPressed(Key.A));
    }
    
    [Test]
    public void TestWasAnyKeyJustPressed()
    {
        _input.UpdateKey(Key.A, true);
        IsTrue(_input.WasJustPressed(Key.Any));
    }
    
    [Test]
    public void TestIsSpecificKeyReleased()
    {
        _input.UpdateKey(Key.A, true);
        _input.UpdateKey(Key.A, false);
        IsTrue(_input.IsReleased(Key.A));
    }
    
    [Test]
    public void TestIsAnyKeyReleased()
    {
        _input.UpdateKey(Key.A, true);
        _input.UpdateKey(Key.A, false);
        IsTrue(_input.IsReleased(Key.Any));
    }
    
    [Test]
    public void TestWasSpecificKeyJustReleased()
    {
        _input.UpdateKey(Key.A, true);
        _input.UpdateKey(Key.A, false);
        IsTrue(_input.WasJustReleased(Key.A));
    }
    
    [Test]
    public void TestWasAnyKeyJustReleased()
    {
        _input.UpdateKey(Key.A, true);
        _input.UpdateKey(Key.A, false);
        IsTrue(_input.WasJustReleased(Key.Any));
    }

    [Test]
    public void TestGLKeyToKeyConversion() => IsTrue(GLKey.A.AsKey() == Key.A);
}