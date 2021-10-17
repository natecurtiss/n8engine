using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class Level1 : Level
    {
        protected override void OnSceneLoaded(Player player, KeyToTheDoor key, Door door)
        {
            new AutoTilemap<TopAndSidesPalette, TilemapThatCanBeJumpedOn>()
                .Place(Window.BottomLeftCorner + Vector.Left * 20, new IntegerVector(7, 4), Pivot.BottomLeft)
                .Place(Window.BottomRightCorner + Vector.Right * 20, new IntegerVector(7, 4), Pivot.BottomRight)
                .Place(Window.BottomSide, new IntegerVector(3, 2), Pivot.Bottom);
            key.Transform.Position = Vector.Down * 30f;
            door.Transform.Position = Window.RightSide + Vector.Down * 20f + Vector.Left * 4f;
        }
    }
}