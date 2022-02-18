using N8Engine.SceneManagement;

namespace SampleProject;

sealed class MainScene : Scene
{
    public override void Load()
    {
        Create("player").AddComponent(new Player());
    }
}