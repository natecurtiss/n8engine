using System;
using System.Linq;
using N8Engine.SceneManagement;
using NUnit.Framework;

namespace N8Engine.Tests;

sealed class SceneManagementTests
{
    sealed class Events : GameEvents
    {
        public event Action<Frame>? OnEarlyUpdate;
        public event Action<Frame>? OnUpdate;
        public event Action<Frame>? OnLateUpdate;
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

    const string FIRST = "first";
    const string SECOND = "second";

    [Test]
    public void TestInitialScene()
    {
        var s1 = new S1();
        var sm = new SceneManager(new Events(), s1);
        Assert.IsTrue(s1.First().IsAlive);
        Assert.IsTrue(sm.CurrentScene.Count() == 1);
    }
    
    [Test]
    public void TestLoadScene()
    {
        var s1 = new S1();
        var sm = new SceneManager(new Events(), s1);
        Assert.IsTrue(s1.First().IsAlive);
        sm.Load(new S2());
        Assert.IsTrue(sm.CurrentScene.First().Name == SECOND);
    }

    [Test]
    public void TestUnloadScene()
    {
        var s1 = new S1();
        var sm = new SceneManager(new Events(), s1);
        var go = s1.First();
        sm.Load(new S2());
        Assert.IsFalse(go.IsAlive);
        Assert.IsTrue(!s1.Any());
    }
}