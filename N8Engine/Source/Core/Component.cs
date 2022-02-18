using System;

namespace N8Engine;

public abstract class Component
{
    internal Type Type { get; set; }
    public virtual void Create(GameObject gameObject) { }
    public virtual void Destroy() { }
    public virtual void EarlyUpdate(Frame frame) { }
    public virtual void Update(Frame frame) { }
    public virtual void LateUpdate(Frame frame) { }
}