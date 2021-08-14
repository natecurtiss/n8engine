using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public class PlayerJumpAnimation : AnimationBase
    {
        protected override Sprite[] Frames => new Sprite[]
        {
            new(PathToSpritesFolder + "player_5.n8sprite", new Vector(0f, -2.5f), ShouldFlipHorizontally)
        };
        protected override float TimeBetweenFrames => 0f;
        
        protected virtual bool ShouldFlipHorizontally => false;
    }
}