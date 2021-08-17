using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class Level1Scene : Scene
    {
        protected override void OnSceneLoaded()
        {
            GameObject.Create<Player>("player").Transform.Position = Vector.Zero;
            AutoTilemap<TopOnlyTilePalette>.Place(Window.BottomLeftCorner, new Vector(7, 3),TilePivot.BottomLeft);
        }
    }
}