using System;
using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerWalkAnimation : Animation
    {
        private const float TIME_BETWEEN_FRAMES = 0.05f;
        private readonly Sprite[] _frames = 
        {
            new(SpritesFolder.Path + "player_1.png"),
            new(SpritesFolder.Path + "player_2.png", Vector.Up),
            new(SpritesFolder.Path + "player_3.png"),
            new(SpritesFolder.Path + "player_4.png", Vector.Up),
        };
        private int _currentFrame;
        private int CurrentFrame
        {
            get => _currentFrame;
            set => _currentFrame = value >= _frames.Length ? 0 : value.KeptAboveZero();
        }

        protected override Sequence[] Keyframes => new[]
        {
            new Sequence()
                .Do(gameObject => gameObject.SpriteRenderer.Sprite = _frames[CurrentFrame])
                .Wait(TIME_BETWEEN_FRAMES)
                .Do(() => CurrentFrame++)
                .Repeat(_frames.Length)
        };
        protected override bool ShouldLoop => true;
    }
}