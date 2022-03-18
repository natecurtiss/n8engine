namespace N8Engine.SceneManagement;

public interface Element
{
    void OnSceneLoad(Scene scene);
    void OnSceneUpdate();
    void OnSceneRender();
    void OnSceneUnload();
}