using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerWalkAnimation : Animation
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(SpritesFolder.Path + "player_1.n8sprite", Vector.Zero, ShouldFlipHorizontally),
            new(SpritesFolder.Path + "player_2.n8sprite", Vector.Zero, ShouldFlipHorizontally),
            new(SpritesFolder.Path + "player_3.n8sprite", Vector.Zero, ShouldFlipHorizontally),
            new(SpritesFolder.Path + "player_4.n8sprite", Vector.Zero, ShouldFlipHorizontally)
        };
        protected override float TimeBetweenFrames => 0.075f;

        protected virtual bool ShouldFlipHorizontally => false;
    }
}