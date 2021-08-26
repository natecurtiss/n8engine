using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class TopPalette : FullPalette
    {
        public override Sprite Left => null;
        public override Sprite Right => null;
        public override Sprite Bottom => null;
        public override Sprite TopLeft => new(SpritesFolder.Path + "wall_top.n8sprite");
        public override Sprite TopRight => new(SpritesFolder.Path + "wall_top.n8sprite");
        public override Sprite BottomLeft => null;
        public override Sprite BottomRight => null;
    }
}