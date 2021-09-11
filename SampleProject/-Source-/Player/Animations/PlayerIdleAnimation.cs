using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerIdleAnimation : Animation
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(SpritesFolder.Path + "player_1.png", Vector.Zero),
            new(SpritesFolder.Path + "player_5.png", Vector.Up)
        };

        protected override float TimeBetweenFrames => 0.35f;

        protected virtual bool ShouldFlipHorizontally => false;
    }
}