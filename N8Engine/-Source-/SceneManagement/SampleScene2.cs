namespace N8Engine.SceneManagement
{
    public sealed class SampleScene2 : Scene
    {
        protected override void OnSceneLoaded()
        {
            var gameObject = GameObject.Create<DummyGameObject>();
        }
    }
}