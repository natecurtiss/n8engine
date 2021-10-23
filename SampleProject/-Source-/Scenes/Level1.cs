using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class Level1 : Level
    {
        public override string Name => "Level 1";
        
        protected override void OnSceneLoaded(Player player)
        {
            new AutoTilemap<GroundPalette, TilemapThatCanBeJumpedOn>()
                .Place(Window.BottomLeftCorner + Vector.Left * 20, new IntegerVector(6, 2), Pivot.BottomLeft)
                .Place(Window.BottomRightCorner + Vector.Right * 20, new IntegerVector(6, 2), Pivot.BottomRight);
        }
    }
}