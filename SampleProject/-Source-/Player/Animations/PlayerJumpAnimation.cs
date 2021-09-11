using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerJumpAnimation : Animation
    {
        protected override Sprite[] Frames => new[]
        {
            Sprite.Create(SpritesFolder.Path + "player_5.png", Vector.Up)
        };
        protected override float TimeBetweenFrames => 0f;
        
        protected virtual bool ShouldFlipHorizontally => false;
    }
}