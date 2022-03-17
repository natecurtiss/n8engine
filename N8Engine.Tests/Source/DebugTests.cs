using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace N8Engine.Tests;

sealed class DebugTests
{
    [Test]
    public void TestRedirectOutput()
    {
        var s = "something";
        Debug.OnOutput(o => s = o.ToString());
        Debug.Log("something else");
        AreEqual(s, "something else");
    }
}