namespace N8Engine.SceneManagement
{
    public interface ISceneManager
    {
        Scene CurrentScene { get; }
        void LoadScene(string name);
        void LoadScene(int index);
        void LoadScene(Scene scene);
        void LoadNextScene();
        void LoadPreviousScene();
        void LoadCurrentScene();
        void LoadFirstScene();
    }
}