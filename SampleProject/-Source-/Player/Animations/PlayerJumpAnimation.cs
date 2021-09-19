using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerJumpAnimation : Animation
    {
        private readonly Sprite _1 = new(SpritesFolder.Path + "player_5.png", Vector.Up);

        protected override Keyframe[] Keyframes => new Keyframe[]
        {
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _1),
        };
        protected override bool ShouldLoop => true;
    }
}