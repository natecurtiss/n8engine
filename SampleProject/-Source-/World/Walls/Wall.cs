using N8Engine.Rendering;

namespace SampleProject
{
    public abstract class Wall : Ground
    {
        protected abstract string SpriteName { get; }

        protected override void OnStart() => SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + SpriteName);
    }
}