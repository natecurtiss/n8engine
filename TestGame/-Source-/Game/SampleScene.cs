using N8Engine.Mathematics;

namespace N8Engine.SceneManagement
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