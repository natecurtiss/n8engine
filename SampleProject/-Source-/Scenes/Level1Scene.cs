using N8Engine;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public sealed class Level1Scene : Scene
    {
        protected override void OnSceneLoaded()
        {
            GameObject.Create<Player>("player");
            GameObject.Create<Walls.UpperLeftWall>("floor").Transform.Position = new Vector(-300f, 30f);
            GameObject.Create<Walls.UpperWall>("floor").Transform.Position = new Vector(-276f, 30f);
        }
    }
}