using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerIdleAnimation : FrameByFrameAnimation
    {
        protected override bool ShouldLoop => true;
        protected override float TimeBetweenFrames => 0.35f;
        protected override Sprite[] Frames => new[]
        {
            AllSprites.Player.Take(0, 0),
            AllSprites.Player.Take(1, 0)
        };
    }
}