using N8Engine.Rendering;

namespace N8Engine.Objects
{
    public sealed class DummyGameObject : GameObject
    {
        protected override Sprite RenderSprite() => null;

        protected override void OnStart() =>
            Name = "BenBonk -- https://youtube.com/BenBonk";
    }
}