using NUnit.Framework;

namespace N8Engine.Tests;

sealed class GameObjectTests
{
    sealed class Empty : Component { }
    static GameObject GameObject() => new(new());
    static Empty Component() => new();
    
    [Test]
    public void TestDestroyGameObject()
    {
        var go = GameObject(); 
        Assert.IsTrue(go.IsAlive);
        go.Destroy();
        Assert.IsFalse(go.IsAlive);
    }

    [Test]
    public void TestAddComponent()
    {
        var go = GameObject();
        var c = Component();
        go.AddComponent(c);
        Assert.IsNotNull(go.GetComponent<Empty>());
    }
    
    [Test]
    public void TestGetComponent()
    {
        var go = GameObject();
        Assert.Catch(() => go.GetComponent<Empty>());
    }
    
    [Test]
    public void TestRemoveComponent()
    {
        var go = GameObject();
        var c = Component();
        go.AddComponent(c);
        go.RemoveComponent(c);
        Assert.Catch(() => go.GetComponent<Empty>());
    }
}