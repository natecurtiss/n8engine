namespace N8Engine
{
    public abstract class Component
    {
        protected GameObject GameObject { get; private set; }

        internal virtual bool CanBeAddedByUser { get; } = true;

        internal void Start() => OnStart();
        internal void Destroy() => OnDestroyed();
        internal void Update(float deltaTime) => OnUpdate(deltaTime);
        internal void PhysicsUpdate(float deltaTime) => OnPhysicsUpdate(deltaTime);
        internal void LateUpdate(float deltaTime) => OnLateUpdate(deltaTime);
        
        protected virtual void OnStart() { }
        protected virtual void OnDestroyed() { }
        protected virtual void OnUpdate(float deltaTime) { }
        protected virtual void OnPhysicsUpdate(float deltaTime) { }
        protected virtual void OnLateUpdate(float deltaTime) { }

        internal void Give(GameObject gameObject) => GameObject = gameObject;
    }
}