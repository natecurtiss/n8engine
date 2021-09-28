using System;
using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerIdleAnimation : Animation
    {
        private const float TIME_BETWEEN_KEYFRAMES = 0.35f;

        private readonly Sprite _1 = new(SpritesFolder.Path + "player_1.png");
        private readonly Sprite _2 = new(SpritesFolder.Path + "player_5.png", Vector.Down);

        protected override Keyframe[] Keyframes => new Keyframe[]
        {
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _1),
            Wait(TIME_BETWEEN_KEYFRAMES),
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _2),
            Wait(TIME_BETWEEN_KEYFRAMES)
        };
        protected override bool ShouldLoop => true;
    }
}