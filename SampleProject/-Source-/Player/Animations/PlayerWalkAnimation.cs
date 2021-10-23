using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerWalkAnimation : FrameByFrameAnimation
    {
        protected override bool ShouldLoop => true;
        protected override float TimeBetweenFrames => 0.05f;
        protected override Sprite[] Frames => new Sprite[]
        {
            new(AllSprites.PathToFolder + "player_1.png"),
            new(AllSprites.PathToFolder + "player_2.png", Vector.Up),
            new(AllSprites.PathToFolder + "player_3.png"),
            new(AllSprites.PathToFolder + "player_4.png", Vector.Up),
        };
    }
}