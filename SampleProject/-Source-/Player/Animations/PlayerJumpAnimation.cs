using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Rendering.Animation;

namespace SampleProject
{
    public class PlayerJumpAnimation : Animation
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(SpritesFolder.Path + "player_5.n8sprite", new Vector(0f, -4f), ShouldFlipHorizontally)
        };
        protected override float TimeBetweenFrames => 0f;
        
        protected virtual bool ShouldFlipHorizontally => false;
    }
}