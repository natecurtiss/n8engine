using N8Engine.SceneManagement;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override void Load()
    {
        Create("player").AddComponent(new Player());
    }
}