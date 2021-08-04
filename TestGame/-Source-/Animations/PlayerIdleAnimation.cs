using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Rendering.Animation;

namespace TestGame
{
    public class PlayerIdleAnimation : Animation
    {
        protected override Sprite[] Frames(SpriteRenderer spriteRenderer)
        {
            var pathToSpritesFolder = PathExtensions.PathToRootFolder + "\\TestGame\\Sprites\\";
            return new Sprite[]
            {
                new(pathToSpritesFolder + "player_1.n8sprite", spriteRenderer, default, ShouldFlipHorizontally()),
                new(pathToSpritesFolder + "player_5.n8sprite", spriteRenderer, new Vector(0f, -2f), ShouldFlipHorizontally())
            };
        }

        protected override float TimeBetweenFrames() => 0.35f;

        protected virtual bool ShouldFlipHorizontally() => false;
    }
}