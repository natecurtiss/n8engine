using System;
using N8Engine;
using N8Engine.InputSystem;
using N8Engine.SceneManagement;

namespace SampleProject
{
    sealed class Level1 : Scene
    {
        GameObject _player;
        GameObject _stage;
        
        protected override void OnLoaded()
        {
            _player = new GameObject("player",
                new SpriteRenderer(Sprites.Player),
                new TopDownInput(),
                new Movement(10f)
            );
            _stage = new GameObject("stage",
                new SpriteRenderer(Sprites.Stage, -1)
            );
        }
        
        protected override void OnUnloaded()
        {
            // _player.Destroy();
            // _stage.Destroy();
        }
    }
}