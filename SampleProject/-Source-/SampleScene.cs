using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public sealed class SampleScene : Scene
    {
        public override string Name => "sample scene";
        protected override void OnSceneLoaded()
        {
            var player = new GameObject().AddComponent<SpriteRenderer>().SetSprite("path");
        }
    }
}