using N8Engine.SceneManagement;

namespace N8Engine;

public abstract class Component
{
    public virtual void Destroy() { }
    public virtual void Create(GameObject gameObject, Scene scene) { }
    public virtual void Start() { }
    public virtual void EarlyUpdate(Frame frame) { }
    public virtual void Update(Frame frame) { }
    public virtual void LateUpdate(Frame frame) { }
    public virtual void Render() { }
}