using N8Engine.SceneManagement;

namespace N8Engine;

public abstract class Component
{
    public virtual void Destroy() { }
    public virtual void Create() { }
    public virtual void Create(Scene scene) { }
    public virtual void Create(GameObject gameObject) { }
    public virtual void Create(GameObject gameObject, Scene scene) { }
    public virtual void Start() { }
    public virtual void EarlyUpdate() { }
    public virtual void EarlyUpdate(Frame frame) { }
    public virtual void Update() { }
    public virtual void Update(Frame frame) { }
    public virtual void LateUpdate() { }
    public virtual void LateUpdate(Frame frame) { }
    public virtual void Render() { }
}