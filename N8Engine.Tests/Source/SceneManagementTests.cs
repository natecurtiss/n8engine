using System;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Windowing;
using NUnit.Framework;
using Silk.NET.Core.Contexts;
using Silk.NET.OpenGL;
using static NUnit.Framework.Assert;

namespace N8Engine.Tests;

sealed class SceneManagementTests
{
    sealed class W : WindowSize, WindowEvents { int WindowSize.Width => 0; int WindowSize.Height => 0; public event Action? OnClose; }
    sealed class L : Loop { public event Action? OnStart; public event Action<Frame>? OnUpdate; public event Action? OnRender; }
    sealed class S1 : Scene { protected override void Load() => Create("a"); }
    sealed class S2 : Scene { protected override void Load() => Create("b"); }

    SceneManager _sceneManager = null!;
    
    [SetUp]
    public void Setup() => _sceneManager = new(new L(), new W(), _ => { }, _ => { });

    [Test]
    public void TestDefaultScene() => IsNotNull(_sceneManager.CurrentScene);
    
    [Test]
    public void TestLoadScene() => DoesNotThrow(() => _sceneManager.Load(new S1()));

    [Test]
    public void TestLoadSceneUpdatesCurrentScene()
    {
        var s1 = new S1();
        _sceneManager.Load(s1);
        AreEqual(s1, _sceneManager.CurrentScene);
    }
    
    [Test]
    public void TestLoadedSceneCreatesGameObjects()
    {
        var s1 = new S1();
        _sceneManager.Load(s1);
        IsTrue(s1.Count() == 1);
    }
    
    [Test]
    public void TestLoadedSceneGameObjectsAreNotDestroyed()
    {
        var s1 = new S1();
        _sceneManager.Load(s1);
        IsFalse(s1.First().IsDestroyed);
    }

    [Test]
    public void TestDestroySceneGameObjectsOnUnload()
    {
        var s1 = new S1();
        _sceneManager.Load(s1);
        _sceneManager.Load(new S2());
        IsTrue(!s1.Any());
    }
}