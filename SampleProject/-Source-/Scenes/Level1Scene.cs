using N8Engine;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public sealed class Level1Scene : Scene
    {
        protected override void OnSceneLoaded()
        {
            var player = GameObject.Create<Player>("player");
            var floor = GameObject.Create<Floor>("floor");
            floor.Transform.Position += Vector.Up * 30f;
            var floor2 = GameObject.Create<Floor>("floor");
            floor2.Transform.Position += Vector.Right * 100f;
        }
    }
}