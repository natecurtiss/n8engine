using N8Engine.Core;
using NUnit.Framework;

namespace N8Engine.Components
{
    internal sealed class ComponentTests
    {
        [Test]
        public void TestAddComponent()
        {
            GameObject __gameObject = new GameObject("Test AddComponent GameObject");
            Component __component = __gameObject.AddComponent<DummyComponent>();
            Assert.IsNotNull(__component);
        }

        [Test]
        public void TestGetComponent()
        {
            GameObject __gameObject = new GameObject("Test GetComponent GameObject");
            Transform __transform = __gameObject.GetComponent<Transform>();
            Assert.IsNotNull(__transform);
        }
        
        [Test]
        public void TestAddAndGetComponent()
        {
            GameObject __gameObject = new GameObject("Test GetComponent GameObject");
            __gameObject.AddComponent<DummyComponent>();
            Assert.IsNotNull(__gameObject.GetComponent<DummyComponent>());
        }
        
        [Test]
        public void TestDestroyComponent()
        {
            GameObject __gameObject = new GameObject("GameObject to Destroy");
            Component __component = __gameObject.AddComponent<DummyComponent>();
            Object.Destroy(__component);
            Assert.IsNull(__component);
        }
        
        [Test]
        public void TestDestroyComponentAndGetComponent()
        {
            GameObject __gameObject = new GameObject("GameObject to Destroy");
            Component __component = __gameObject.AddComponent<DummyComponent>();
            Object.Destroy(__component);
            Assert.IsNull(__gameObject.GetComponent<DummyComponent>());
        }
    }
}