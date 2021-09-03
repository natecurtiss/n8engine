using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Tilemaps;
using N8Engine.Inputs;

namespace SampleProject
{
    public sealed class Level1 : Scene
    {
        protected override void OnSceneLoaded()
        {
            var tilemap = new AutoTilemap<FullPalette>();
            tilemap.Place(Window.BottomLeftCorner, new IntegerVector(10, 4), Pivot.BottomLeft);

            GameObject.Create<Player>("player");
            GameObject.Create<Door>("door").Transform.Position += new Vector(252f, -39f);
            GameObject.Create<DoorKey>("key");
            PhysicsSettings.ShouldShowAllColliders = true;
        }
    }
}