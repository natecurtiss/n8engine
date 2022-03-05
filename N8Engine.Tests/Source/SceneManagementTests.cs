using System;
using System.Linq;
using System.Numerics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Windowing;
using NUnit.Framework;
using Silk.NET.OpenGL;

namespace N8Engine.Tests;

// TODO: Refactor.
sealed class SceneManagementTests
{
    sealed class Events : Loop
    {
        public event Action<Frame>? OnUpdate;
        public event Action<GL>? OnRender;
    }

    sealed class S1 : Scene
    {
        public override void Load()
        {
            Create(FIRST);
        }
    }
    
    sealed class S2 : Scene
    {
        public override void Load()
        {
            Create(SECOND);
        }
    }

    sealed class W : WindowSize
    {
        int WindowSize.Width => 0;
        int WindowSize.Height => 0;
    }

    const string FIRST = "first";
    const string SECOND = "second";

    // TODO: This is really bad so get rid of it please and thank you.
    [SetUp]
    public void Setup() => Game.Modules.Add(new Camera(Vector2.Zero, 1f, new W()));

    [Test]
    public void TestInitialScene()
    {
        var s1 = new S1();
        var sm = new SceneManager(new Events());
        sm.Load(s1);
        Assert.IsFalse(s1.First().IsDestroyed);
        Assert.IsTrue(sm.CurrentScene.Count() == 1);
    }
    
    [Test]
    public void TestLoadScene()
    {
        var s1 = new S1();
        var sm = new SceneManager(new Events());
        sm.Load(s1);
        Assert.IsFalse(s1.First().IsDestroyed);
        sm.Load(new S2());
        Assert.IsTrue(sm.CurrentScene.First().Name == SECOND);
    }

    [Test]
    public void TestUnloadScene()
    {
        var s1 = new S1();
        var sm = new SceneManager(new Events());
        sm.Load(s1);
        var go = s1.First();
        sm.Load(new S2());
        Assert.IsTrue(go.IsDestroyed);
        Assert.IsTrue(!s1.Any());
    }
}