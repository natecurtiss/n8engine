using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerJumpAnimation : Animation
    {
        private readonly Sprite _1 = new(SpritesFolder.Path + "player_5.png", Vector.Down);

        protected override Sequence[] Keyframes => new[]
        {
            new Sequence().Do(gameObject => gameObject.SpriteRenderer.Sprite = _1)
        };
        protected override bool ShouldLoop => true;
    }
}