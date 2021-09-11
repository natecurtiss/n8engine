using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerJumpAnimation : SingleKeyframeAnimation
    {
        private readonly Sprite _sprite = new(SpritesFolder.Path + "player_5.png", Vector.Up);

        protected override void OnKeyframe(GameObject gameObject) => gameObject.SpriteRenderer.Sprite = _sprite;
    }
}