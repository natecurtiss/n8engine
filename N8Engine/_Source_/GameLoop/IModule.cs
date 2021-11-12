using System;

namespace N8Engine
{
    public interface IModule
    {
        internal Type Type { get; }
        internal void Initialize();
        internal void Update(Time time);
    }
}