using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public abstract class WallBase : GameObject
    {
        protected abstract string SpriteName { get; }
        protected virtual Vector Offset => Vector.Zero;

        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + SpriteName, Offset);
            Collider.Size = new Vector(24f, 14f);
            // Collider.IsDebugModeEnabled = true;
        }
    }
}