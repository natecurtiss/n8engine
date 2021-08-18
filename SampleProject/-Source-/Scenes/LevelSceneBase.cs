using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public abstract class LevelSceneBase : Scene
    {
        protected override void OnSceneLoaded() => GameObject.Create<Player>("player").Transform.Position = Window.LeftSide + Vector.Right * 15f;
    }
}