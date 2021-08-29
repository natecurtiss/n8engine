using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Door : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.SortingOrder = -1;
            SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + "door.n8sprite");
        }

        protected override void OnDestroy()
        {
            
        }
    }
}