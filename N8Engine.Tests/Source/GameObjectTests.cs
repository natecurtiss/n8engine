using System;
using N8Engine.SceneManagement;
using NUnit.Framework;

namespace N8Engine.Tests;

sealed class GameObjectTests
{
    sealed class B : Component { }
    sealed class C : Component { }
    sealed class S : Scene { public override void Load() { } }

    [Test]
    public void TestGetComponent()
    {
        var go = new GameObject(new S(), "foo");
        Assert.IsNull(go.GetComponent<C>());
        go.AddComponent(new C());
        Assert.IsNotNull(go.GetComponent<C>());
        Assert.IsNull(go.GetComponent<B>());
    }

    [Test]
    public void TestAddComponent()
    {
        var go = new GameObject(new S(), "foo");
        go.AddComponent(new C());
        Assert.IsNotNull(go.GetComponent<C>());
        go.Destroy();
        
    }

    [Test]
    public void TestRemoveComponent()
    {
        var go = new GameObject(new S(), "foo");
        var c = new C();
        
        go.AddComponent(c);
        go.RemoveComponent(c);
        Assert.IsNull(go.GetComponent<C>());
        
        go.AddComponent(new C());
        go.RemoveComponent(go.GetComponent<C>());
        Assert.IsNull(go.GetComponent<C>());
        
        Assert.Catch(() => go.RemoveComponent(new B()));
    }
    
    [Test]
    public void TestDestroyGameObject()
    {
        var go = new GameObject(new S(), "foo");
        var c = new C();
        go.AddComponent(c);
        Assert.IsFalse(go.IsDestroyed);
        
        go.Destroy();
        Assert.IsTrue(go.IsDestroyed);
        Assert.Catch(() => go.GetComponent<C>());
        Assert.Catch(() => go.RemoveComponent(c));
        Assert.Catch(() => go.AddComponent(new C()));
        Assert.Catch(() => go.AddComponent(new B()));
    }

    [Test]
    public void TestNameGameObject()
    {
        var go = new GameObject(new S(), "foo");
        Assert.IsTrue(go.Name == "foo");
    }
    
    [Test]
    public void TestRenameGameObject()
    {
        var go = new GameObject(new S(), "foo");
        var name = "fdjkl;asfdsa";
        go.Name = name;
        Assert.IsTrue(go.Name == name);
    }
}