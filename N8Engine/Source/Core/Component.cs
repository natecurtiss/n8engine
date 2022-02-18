using System;

namespace N8Engine;

public interface Component
{
    internal Type Type { get; set; }
    public void Create(GameObject gameObject) { }
    public void Destroy() { }
    public void EarlyUpdate(Frame frame) { }
    public void Update(Frame frame) { }
    public void LateUpdate(Frame frame) { }
}