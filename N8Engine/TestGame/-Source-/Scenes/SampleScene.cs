using N8Engine;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace TestGame
{
    public sealed class SampleScene : Scene
    {
        protected override void OnSceneLoaded()
        {
            var firstGameObject = GameObject.Create<DummyGameObject>();
            firstGameObject.Transform.Position += Vector.Right * 100;
            var secondGameObject = GameObject.Create<DummyGameObject2>(); 
            secondGameObject.Name = "wow";
        }
    }
}