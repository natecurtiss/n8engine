using N8Engine;
using N8Engine.SceneManagement;

namespace TestGame
{
    public sealed class SampleScene2 : Scene
    {
        protected override void OnSceneLoaded()
        {
            var gameObject = GameObject.Create<DummyGameObject>();
        }
    }
}