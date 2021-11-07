using N8Engine.Rendering;

namespace N8Engine.SceneManagement
{
    public interface ISceneManager<T>
    {
        IScene<T> CurrentScene { get; }
        void LoadScene(string name);
        void LoadScene(int index);
        void LoadScene(IScene<T> scene);
        void LoadNextScene();
        void LoadPreviousScene();
        void LoadCurrentScene();
        void LoadFirstScene();
        internal void UpdateCurrentScene(float deltaTime, IRenderer renderer);
        internal void AddToCurrentScene(T obj);
        internal void RemoveFromCurrentScene(T obj);
    }
}