using N8Engine;
using N8Engine.Rendering;
using N8Engine.Rendering.Animation;

namespace TestGame
{
    public class PlayerWalkAnimation : Animation
    {
        protected override Sprite[] Frames(SpriteRenderer spriteRenderer)
        {
            var pathToSpritesFolder = PathExtensions.PathToRootFolder + "\\TestGame\\Sprites\\";
            return new Sprite[]
            {
                new(pathToSpritesFolder + "player_1.n8sprite", spriteRenderer, default, ShouldFlipHorizontally()),
                new(pathToSpritesFolder + "player_2.n8sprite", spriteRenderer, default, ShouldFlipHorizontally()),
                new(pathToSpritesFolder + "player_3.n8sprite", spriteRenderer, default, ShouldFlipHorizontally()),
                new(pathToSpritesFolder + "player_4.n8sprite", spriteRenderer, default, ShouldFlipHorizontally())
            };
        }

        protected override float TimeBetweenFrames() => 0.075f;

        protected virtual bool ShouldFlipHorizontally() => false;
    }
}