using System;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Animation
{
    public abstract class FrameByFrameAnimation : Animation
    {
        private int _currentFrame;
        private Sprite[] _cachedFrames = Array.Empty<Sprite>();
        
        private int NumberOfFrames => _cachedFrames.Length;
        private int CurrentFrame
        {
            get => _currentFrame;
            set => _currentFrame = value >= _cachedFrames.Length ? 0 : value.KeptAboveZero();
        }
        
        protected abstract float TimeBetweenFrames { get; }
        protected abstract Sprite[] Frames { get; }
        
        protected override Sequence[] Keyframes => new[]
        {
            new Sequence()
                .Do(gameObject => gameObject.SpriteRenderer.Sprite = _cachedFrames[CurrentFrame])
                .Wait(TimeBetweenFrames)
                .Do(() => CurrentFrame++)
                .Repeat(NumberOfFrames)
        };

        private protected override void OnInitialized() => _cachedFrames = Frames;
    }
}