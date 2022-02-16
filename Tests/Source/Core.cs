using NUnit.Framework;

namespace N8Engine.Tests;

sealed class Core
{
    class Empty : Module { }
    readonly Modules _modules = new();

    [Test]
    public void TestAddModule()
    {
        _modules.Add(new Empty());
        Assert.IsNotNull(_modules.Get<Empty>());
        _modules.Remove<Empty>();
    }
    
    [Test]
    public void TestRemoveModule()
    {
        _modules.Add(new Empty());
        _modules.Remove<Empty>();
        Assert.Catch(() => _modules.Get<Empty>());
    }
    
    [Test]
    public void TestGetModule() => Assert.Catch(() => _modules.Get<Empty>());
}