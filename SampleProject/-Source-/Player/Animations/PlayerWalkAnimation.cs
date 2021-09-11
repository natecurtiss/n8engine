using System;
using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerWalkAnimation : FixedAnimation
    {
        private readonly Sprite _firstFrame = new(SpritesFolder.Path + "player_1.png");
        private readonly Sprite _secondFrame = new(SpritesFolder.Path + "player_2.png", Vector.Down);
        private readonly Sprite _thirdFrame = new(SpritesFolder.Path + "player_3.png");
        private readonly Sprite _fourthFrame = new(SpritesFolder.Path + "player_4.png", Vector.Down);

        protected override Action<GameObject, float>[] Keyframes => new Action<GameObject, float>[]
        {
            (gameObject, _) => gameObject.SpriteRenderer.Sprite = _firstFrame,
            (gameObject, _) => gameObject.SpriteRenderer.Sprite = _secondFrame,
            (gameObject, _) => gameObject.SpriteRenderer.Sprite = _thirdFrame,
            (gameObject, _) => gameObject.SpriteRenderer.Sprite = _fourthFrame
        };
        protected override float TimeBetweenEachKeyframe => 0.075f;
        protected override bool ShouldLoop => true;
    }
}