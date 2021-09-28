using System;
using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerWalkAnimation : Animation
    {
        private const float TIME_BETWEEN_FRAMES = 0.075f;
        
        private readonly Sprite _1 = new(SpritesFolder.Path + "player_1.png");
        private readonly Sprite _2 = new(SpritesFolder.Path + "player_2.png", Vector.Up);
        private readonly Sprite _3 = new(SpritesFolder.Path + "player_3.png");
        private readonly Sprite _4 = new(SpritesFolder.Path + "player_4.png", Vector.Up);

        protected override Keyframe[] Keyframes => new Keyframe[]
        {
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _1),
            Wait(TIME_BETWEEN_FRAMES),
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _2),
            Wait(TIME_BETWEEN_FRAMES),
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _3),
            Wait(TIME_BETWEEN_FRAMES),
            Do(gameObject => gameObject.SpriteRenderer.Sprite = _4),
            Wait(TIME_BETWEEN_FRAMES),
        };
        protected override bool ShouldLoop => true;
    }
}