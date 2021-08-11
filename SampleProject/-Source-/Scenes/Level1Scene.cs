using N8Engine;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public sealed class Level1Scene : Scene
    {
        protected override void OnSceneLoaded()
        {
            var player = GameObject.Create<Player>();
            player.Name = "player";
            var floor = GameObject.Create<Floor>();
            floor.Transform.Position += Vector.Up * 30f;
            floor.Name = "floor";
        }
    }
}