using N8Engine;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public sealed class SampleScene : Scene
    {
        protected override void OnSceneLoaded()
        {
            var player = GameObject.Create().AddComponent<SpriteRenderer>().SetSprite(SpritesFolder.Player[0, 1]);
            player.Transform.Position = Vector.Zero;
        }
    }
}