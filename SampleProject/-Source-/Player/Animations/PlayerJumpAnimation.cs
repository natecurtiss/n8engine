using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerJumpAnimation : FrameByFrameAnimation
    {
        protected override bool ShouldLoop => false;
        protected override float TimeBetweenFrames => 0.03f;
        protected override Sprite[] Frames => new[]
        {
            AllSprites.Player.Take(0, 2),
            AllSprites.Player.Take(1, 2)
        };
    }
}