using System;
using N8Engine.Utilities;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace N8Engine.Tests;

sealed class ServiceLocatorTests
{
    sealed class S { }
    sealed class E : Exception {  }

    ServiceLocator<S, E> _services;

    [SetUp]
    public void Setup() => _services = new();
    
    [Test]
    public void TestRegisterService()
    {
        _services.Register(new S());
        IsTrue(_services.Count == 1);
    }
    
    [Test]
    public void TestDeregisterNonExistentService() => Catch<ServiceNotFoundException>(() => _services.Deregister<S>());

    [Test]
    public void TestDeregisterExistentService()
    {
        _services.Register(new S());
        _services.Deregister<S>();
        IsTrue(_services.Count == 0);
    }

    [Test]
    public void TestFindNonExistentService() => Catch<ServiceNotFoundException>(() => _services.Find<S>());

    [Test]
    public void TestFindExistentService()
    {
        _services.Register(new S());
        IsNotNull(_services.Find<S>());
    }
}