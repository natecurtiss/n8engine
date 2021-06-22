using NUnit.Framework;

namespace N8Engine.Objects
{
    internal sealed class GameObjectTests
    {
        [Test]
        public void TestGameObjectInitialization()
        {
            GameObject __gameObject = new DummyGameObject { Name = "Icoso -- https://youtube.com/IcosoGames" };
            __gameObject.Initialize();
            Assert.True(__gameObject.Name == "BenBonk -- https://youtube.com/BenBonk");
        }
    }
}