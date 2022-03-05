using NUnit.Framework;

namespace N8Engine.Tests;

// TODO: Refactor.
sealed class ModulesTests
{
    class Empty : Module { }

    [Test]
    public void TestGetModule()
    {
        var modules = new Modules();
        Assert.Catch(() => modules.Get<Empty>());
        modules.Add(new Empty());
        Assert.IsNotNull(modules.Get<Empty>());
    }
    
    [Test]
    public void TestAddModule()
    {
        var modules = new Modules();
        modules.Add(new Empty());
        Assert.IsNotNull(modules.Get<Empty>());
    }
    
    [Test]
    public void TestRemoveModule()
    {
        var modules = new Modules();
        modules.Add(new Empty());
        modules.Remove<Empty>();
        Assert.Catch(() => modules.Get<Empty>());
    }
}