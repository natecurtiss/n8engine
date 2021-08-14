using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Floor : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite(PathExtensions.PathToRootFolder + "\\SampleProject\\Sprites\\floor.n8sprite");
            Collider.Size = new Vector(80, 10);
        }
    }
}