namespace N8Engine;

public abstract class Component
{
    public virtual void Destroy() { }
    public virtual void EarlyUpdate(Frame frame) { }
    public virtual void Update(Frame frame) { }
    public virtual void LateUpdate(Frame frame) { }
    internal virtual void Render() { }
}