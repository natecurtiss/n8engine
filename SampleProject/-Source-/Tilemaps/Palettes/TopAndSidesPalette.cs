using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class TopAndSidesPalette : FullPalette
    {
        protected override Sprite Bottom => null;
        protected override Sprite BottomLeft => new(AllSprites.PathToFolder + "wall_left.png");
        protected override Sprite BottomRight => new(AllSprites.PathToFolder + "wall_right.png");
    }
}