using N8Engine;

namespace SampleProject
{
    public sealed class TopAndSidesPalette : FullPalette
    {
        public override GameObject Bottom => GameObject.Create<EmptyGameObject>();
        public override GameObject BottomLeft => GameObject.Create<LeftWall>();
        public override GameObject BottomRight => GameObject.Create<RightWall>();
    }
}