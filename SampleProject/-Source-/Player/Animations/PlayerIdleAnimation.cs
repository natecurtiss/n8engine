using System;
using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerIdleAnimation : FixedAnimation
    {
        private readonly Sprite _firstFrame = new(SpritesFolder.Path + "player_1.png");
        private readonly Sprite _secondFrame = new(SpritesFolder.Path + "player_5.png", Vector.Up);

        protected override Action<GameObject, float>[] Keyframes => new Action<GameObject, float>[]
        {
            (gameObject, _) => gameObject.SpriteRenderer.Sprite = _firstFrame,
            (gameObject, _) => gameObject.SpriteRenderer.Sprite = _secondFrame,
        };
        protected override float TimeBetweenEachKeyframe => 0.35f;
        protected override bool ShouldLoop => true;
    }
}