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
                new SpriteRenderer(Sprite.FromImage(@"C:\Users\NateDawg\RiderProjects\n8engine\SampleProject\Sprites\player.png"))
            );
        }
        
        protected override void OnUnloaded()
        {
            
        }
    }
}