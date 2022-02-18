using N8Engine.Events;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly GameObjectEvents _events;
    
    internal SceneManager(GameObjectEvents events)
    {
        _events = events;
    }

    public void Load(Scene scene)
    {
        
    }

    public void UnloadScene()
    {
        
    }
}