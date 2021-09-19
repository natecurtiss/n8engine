using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class TopPalette : FullPalette
    {
        protected override Sprite Left => null;
        protected override Sprite Right => null;
        protected override Sprite Bottom => null;
        protected override Sprite TopLeft => new(SpritesFolder.Path + "wall_top.png");
        protected override Sprite TopRight => new(SpritesFolder.Path + "wall_top.png");
        protected override Sprite BottomLeft => null;
        protected override Sprite BottomRight => null;
    }
}