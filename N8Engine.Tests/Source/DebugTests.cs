using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace N8Engine.Tests;

sealed class DebugTests
{
    Debug _debug = null!;
    
    [SetUp]
    public void Setup() => _debug = new(_ => { });

    [Test]
    public void TestRedirectOutput()
    {
        var s = "something";
        _debug.OnOutput(o => s = o.ToString());
        _debug.Log("something else");
        AreEqual(s, "something else");
    }
}