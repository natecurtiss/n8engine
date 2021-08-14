using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public static class Walls
    {
        public abstract class Wall : GameObject
        {
            protected abstract string SpriteName { get; }

            protected override void OnStart()
            {
                SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + SpriteName);
                Collider.Size = new Vector(24f, 12f);
                Collider.IsDebugModeEnabled = true;
            }
        }
        
        public sealed class LeftWall : Wall
        {
            protected override string SpriteName => "wall_left.n8sprite";
        }

        public sealed class RightWall : Wall
        {
            protected override string SpriteName => "wall_right.n8sprite";
        }
        
        public sealed class UpperWall : Wall
        {
            protected override string SpriteName => "wall_upper.n8sprite";
        }
        
        public sealed class LowerWall : Wall
        {
            protected override string SpriteName => "wall_lower.n8sprite";
        }
        
        public sealed class UpperLeftWall : Wall
        {
            protected override string SpriteName => "wall_upper-left.n8sprite";
        }
        
        public sealed class UpperRightWall : Wall
        {
            protected override string SpriteName => "wall_upper-right.n8sprite";
        }
        
        public sealed class LowerLeftWall : Wall
        {
            protected override string SpriteName => "wall_lower-left.n8sprite";
        }
        
        public sealed class LowerRightWall : Wall
        {
            protected override string SpriteName => "wall_lower-right.n8sprite";
        }
    }
}