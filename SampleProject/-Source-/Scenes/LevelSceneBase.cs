using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public abstract class LevelSceneBase : Scene
    {
        protected override void OnSceneLoaded() => GameObject.Create<Player>("player");
    }
}