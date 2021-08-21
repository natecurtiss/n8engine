using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class Level1Scene : LevelSceneBase
    {
        protected override void OnSceneLoaded()
        {
            base.OnSceneLoaded();
            AutoTilemap<TopAndSidesPalette>.Generator
                .Place(Window.BottomLeftCorner + Vector.Left * 24f, new Vector(20, 3), TilePivot.BottomLeft);
        }
    }
}