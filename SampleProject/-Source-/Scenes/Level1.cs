using System.Drawing;
using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class Level1 : Scene
    {
        protected override void OnSceneLoaded()
        {
            var tilemap = new AutoTilemap<TopAndSidesPalette>();
            tilemap.Place(Window.BottomLeftCorner, new IntegerVector(10, 4), Pivot.BottomLeft);

            GameObject.Create<Player>("player");
            // GameObject.Create<Door>("door").Transform.Position += new Vector(252f, -39f);
            // GameObject.Create<DoorKey>("key");

            const string line_name = "line";
            var line = new Line(Color.White, Window.Width);
            GameObject.CreateStaticSprite(line, line_name).Transform.Position = Vector.Zero;
            for (var i = 0; i < 5; i++)
            {
                GameObject.CreateStaticSprite(line, line_name).Transform.Position = Vector.Up * 10f * i;
                GameObject.CreateStaticSprite(line, line_name).Transform.Position = Vector.Down * 10f * i;
            }
        }
    }
}