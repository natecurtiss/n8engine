using N8Engine.Mathematics;
using N8Engine.Physics;

namespace SampleProject
{
    public sealed class Level1Scene : LevelSceneBase
    {
        protected override void OnLevelLoaded(Door door, DoorKey key)
        {

            door.Transform.Position += new Vector(252f, -39f);
            PhysicsSettings.ShouldShowAllColliders = true;
        }
    }
}