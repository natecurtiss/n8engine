using System;

namespace N8Engine.Internal
{
    public sealed class UpdateEvents
    {
        public event Action<float> OnUpdate;
        public event Action<float> OnPhysicsUpdate;
        public event Action<float> OnLateUpdate;

        public void Invoke(float deltaTime)
        {
            OnUpdate?.Invoke(deltaTime);
            OnPhysicsUpdate?.Invoke(deltaTime);
            OnLateUpdate?.Invoke(deltaTime);
        }
    }
}