using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public abstract class LevelSceneBase : Scene
    {
        protected override void OnSceneLoaded()
        {
            GameObject.Create<Player>("player");
            var door = GameObject.Create<Door>("door");
            var key = GameObject.Create<DoorKey>("key");
            OnLevelLoaded(door, key);
        }

        protected abstract void OnLevelLoaded(Door door, DoorKey key);
    }
}