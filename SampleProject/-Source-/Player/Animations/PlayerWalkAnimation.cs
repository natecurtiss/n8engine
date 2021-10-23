using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerWalkAnimation : FrameByFrameAnimation
    {
        protected override bool ShouldLoop => true;
        protected override float TimeBetweenFrames => 0.05f;
        protected override Sprite[] Frames => new[]
        {
            AllSprites.Player.Take(0, 1),
            AllSprites.Player.Take(1, 1),
            AllSprites.Player.Take(2, 1),
            AllSprites.Player.Take(3, 1)
        };
    }
}