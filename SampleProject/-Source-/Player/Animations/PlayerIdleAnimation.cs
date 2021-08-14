using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerIdleAnimation : AnimationBase
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(PathToSpritesFolder + "player_1.n8sprite", Vector.Zero, ShouldFlipHorizontally),
            new(PathToSpritesFolder + "player_5.n8sprite", new Vector(0f, -2f), ShouldFlipHorizontally)
        };

        protected override float TimeBetweenFrames => 0.35f;

        protected virtual bool ShouldFlipHorizontally => false;
    }
}