namespace N8Engine.SceneManagement
{
    public interface ISceneManager
    {
        Scene CurrentScene { get; }
        void LoadScene(Scene scene);
        void LoadScene(string name);
        void LoadScene(int index);
        void LoadNextScene();
        void LoadPreviousScene();
        void LoadCurrentScene();
    }
}