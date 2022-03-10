namespace N8Engine.SceneManagement;

public interface SceneModule
{
    void OnSceneLoad(Scene scene);
    void OnSceneUpdate();
    void OnSceneRender();
    void OnSceneUnload();
}