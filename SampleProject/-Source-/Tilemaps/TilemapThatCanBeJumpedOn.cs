using N8Engine;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class TilemapThatCanBeJumpedOn : BaseTilemapObject, ICanBeJumpedOn
    {
        protected override void OnStart()
        {
            Collider.ShowDebugCollider = true;
        }
    }
}