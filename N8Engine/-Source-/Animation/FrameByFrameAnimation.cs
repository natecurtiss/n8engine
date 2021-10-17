using System;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Animation
{
    /// <summary>
    /// An <see cref="Animation"/> that changes a <see cref="GameObject.SpriteRenderer">SpriteRenderer's</see> <see cref="Sprite"/> every frame.
    /// </summary>
    public abstract class FrameByFrameAnimation : Animation
    {
        private int _currentFrame;
        private Sprite[] _cachedFrames = Array.Empty<Sprite>();

        protected sealed override Sequence[] Keyframes => new[]
        {
            new Sequence()
                .Do(gameObject => gameObject.SpriteRenderer.Sprite = _cachedFrames[CurrentFrame])
                .Wait(TimeBetweenFrames)
                .Do(() => CurrentFrame++)
                .Repeat(NumberOfFrames)
        };

        /// <summary>
        /// The time between <see cref="Frames">each animation frame.</see>
        /// </summary>
        protected abstract float TimeBetweenFrames { get; }
        /// <summary>
        /// The frames to switch between.
        /// </summary>
        protected abstract Sprite[] Frames { get; }
        
        private int NumberOfFrames => _cachedFrames.Length;
        private int CurrentFrame
        {
            get => _currentFrame;
            set => _currentFrame = value >= _cachedFrames.Length ? 0 : value.KeptAboveZero();
        }

        private protected override void OnInitialized() => _cachedFrames = Frames;
    }
}