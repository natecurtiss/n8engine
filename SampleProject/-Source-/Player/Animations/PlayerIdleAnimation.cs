using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerIdleAnimation : Animation
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(SpritesFolder.Path + "player_1.n8sprite", Vector.Zero, ShouldFlipHorizontally),
            new(SpritesFolder.Path + "player_5.n8sprite", Vector.Up, ShouldFlipHorizontally)
        };

        protected override float TimeBetweenFrames => 0.35f;

        protected virtual bool ShouldFlipHorizontally => false;
    }
}