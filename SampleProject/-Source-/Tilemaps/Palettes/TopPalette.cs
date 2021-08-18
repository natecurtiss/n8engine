using N8Engine;

namespace SampleProject
{
    public sealed class TopPalette : FullPalette
    {
        public override GameObject Left => GameObject.Create<EmptyGameObject>();
        public override GameObject Right => GameObject.Create<EmptyGameObject>();
        public override GameObject Bottom => GameObject.Create<EmptyGameObject>();
        public override GameObject TopLeft => GameObject.Create<TopWall>();
        public override GameObject TopRight => GameObject.Create<TopWall>();
        public override GameObject BottomLeft => GameObject.Create<EmptyGameObject>();
        public override GameObject BottomRight => GameObject.Create<EmptyGameObject>();
    }
}