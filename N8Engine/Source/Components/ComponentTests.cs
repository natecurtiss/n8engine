using NUnit.Framework;

namespace N8Engine.Components
{
    internal sealed class ComponentTests
    {
        [Test]
        public void TestAddComponent()
        {
            GameObject __gameObject = new GameObject("Test AddComponent GameObject");
            __gameObject.AddComponent(out DummyComponent __dummyComponent);
            Assert.IsNotNull(__dummyComponent);
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
    }
}