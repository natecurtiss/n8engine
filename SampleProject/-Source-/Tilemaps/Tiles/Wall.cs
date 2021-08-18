using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public abstract class Wall : GameObject
    {
        protected abstract string SpriteName { get; }

        protected override void OnStart() => SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + SpriteName);
    }
}