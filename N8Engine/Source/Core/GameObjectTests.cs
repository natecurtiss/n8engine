using N8Engine.Components;
using NUnit.Framework;

namespace N8Engine.Core
{
    internal sealed class GameObjectTests
    {
        [Test]
        public void TestDestroyGameObject()
        {
            GameObject __gameObject = new GameObject("GameObject to Destroy");
            Object.Destroy(__gameObject);
            Assert.IsNull(__gameObject);
        }
        
        [Test]
        public void TestDestroyGameObjectWithComponents()
        {
            GameObject __gameObject = new GameObject("GameObject to Destroy");
            DummyComponent __component = __gameObject.AddComponent<DummyComponent>();
            Object.Destroy(__gameObject);
            Assert.True(__component == null);
        }
    }
}