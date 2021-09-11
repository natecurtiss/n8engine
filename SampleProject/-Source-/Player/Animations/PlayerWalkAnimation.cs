using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerWalkAnimation : Animation
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(SpritesFolder.Path + "player_1.png", Vector.Zero),
            new(SpritesFolder.Path + "player_2.png", Vector.Down),
            new(SpritesFolder.Path + "player_3.png", Vector.Zero),
            new(SpritesFolder.Path + "player_4.png", Vector.Down)
        };
        protected override float TimeBetweenFrames => 0.075f;

        protected virtual bool ShouldFlipHorizontally => false;
    }
}