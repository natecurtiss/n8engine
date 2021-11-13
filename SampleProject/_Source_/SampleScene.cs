using System;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    sealed class SampleScene : Scene
    {
        GameObject _player;
        
        protected override void OnLoaded()
        {
            _player = new GameObject
            (
                "player",
                new SpriteRenderer(Sprites.Player)
            );
        }
        
        protected override void OnUnloaded()
        {
            _player.Destroy();
        }
    }
}