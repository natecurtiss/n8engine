using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class DoorKey : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + "key.n8sprite");
        }
    }
}