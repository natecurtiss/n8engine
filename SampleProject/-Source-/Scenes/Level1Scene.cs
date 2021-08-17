using N8Engine;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public sealed class Level1Scene : Scene
    {
        protected override void OnSceneLoaded()
        {
            GameObject.Create<Player>("player").Transform.Position = Vector.Zero;
            AutoTilemap<LeftWall, RightWall, TopWall, BottomWall, TopRightWall, TopLeftWall, BottomRightWall, BottomLeftWall, EmptyGameObject>
                .Place(new Vector(0f, 0f), new Vector(8, 4), new Vector(12f, 12f), TilePivot.Center);
        }
    }
}