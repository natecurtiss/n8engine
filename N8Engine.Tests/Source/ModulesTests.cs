using NUnit.Framework;

namespace N8Engine.Tests;

sealed class ModulesTests
{
    class M : Module { }

    Modules _modules = null!;

    [SetUp]
    public void Setup() => _modules = new();
    
    [Test]
    public void TestAddModule()
    {
        _modules.Add(new M());
        Assert.IsTrue(_modules.Count == 1);
    }
    
    [Test]
    public void TestRemoveNonExistentModule() => Assert.Catch<ModuleNotFoundException>(() => _modules.Remove<M>());

    [Test]
    public void TestRemoveExistentModule()
    {
        _modules.Add(new M());
        _modules.Remove<M>();
        Assert.IsTrue(_modules.Count == 0);
    }

    [Test]
    public void TestGetNonExistentModule() => Assert.Catch<ModuleNotFoundException>(() => _modules.Get<M>());

    [Test]
    public void TestGetExistentModule()
    {
        _modules.Add(new M());
        Assert.IsNotNull(_modules.Get<M>());
    }
}