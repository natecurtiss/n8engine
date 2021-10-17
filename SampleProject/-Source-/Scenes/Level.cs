using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public abstract class Level : Scene
    {
        protected sealed override void OnSceneLoaded()
        {
            var player = GameObject.Create<Player>("player").Spawn();
            var key = GameObject.Create<KeyToTheDoor>("key");
            var door = GameObject.Create<Door>("door");
            OnSceneLoaded(player, key, door);
        }

        protected abstract void OnSceneLoaded(Player player, KeyToTheDoor key, Door door);
    }
}