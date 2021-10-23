using System.Drawing;
using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public abstract class Level : Scene
    {
        protected sealed override void OnSceneLoaded()
        {
            Window.BackgroundColor = Color.FromArgb(255, 209, 209);
            var player = GameObject.Create<Player>("player").Spawn();
            OnSceneLoaded(player);
        }

        protected abstract void OnSceneLoaded(Player player);
    }
}