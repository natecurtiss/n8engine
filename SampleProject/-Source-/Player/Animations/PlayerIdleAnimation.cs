using System;
using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerIdleAnimation : FrameByFrameAnimation
    {
        protected override bool ShouldLoop => true;
        protected override float TimeBetweenFrames => 0.35f;
        protected override Sprite[] Frames => new Sprite[]
        {
            new(SpritesFolder.Path + "player_1.png"),
            new(SpritesFolder.Path + "player_5.png", Vector.Down)
        };
    }
}