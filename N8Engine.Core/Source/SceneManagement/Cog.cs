namespace N8Engine.SceneManagement;

public interface Cog
{
    void OnSceneLoad(Scene scene);
    void OnSceneUpdate();
    void OnSceneRender();
    void OnSceneUnload();
}