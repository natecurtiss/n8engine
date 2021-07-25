namespace N8Engine.SceneManagement
{
    internal sealed class SampleScene2 : Scene
    {
        protected override void OnSceneLoaded()
        {
            var gameObject = GameObject.Create<DummyGameObject>();
        }
    }
}