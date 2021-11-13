namespace N8Engine
{
    public abstract class Component
    {
        public GameObject GameObject { get; private set; }

        internal void AttatchTo(GameObject gameObject)
        {
            GameObject = gameObject;
            OnStart();
        }
        internal void Detatch()
        {
            GameObject = null;
            OnEnd();
        }
        internal void EarlyUpdate(Time time) => OnEarlyUpdate(time);
        internal void Update(Time time) => OnUpdate(time);
        internal void LateUpdate(Time time) => OnLateUpdate(time);
        internal void Render() => OnRender();
        
        protected virtual void OnStart() { }
        protected virtual void OnEarlyUpdate(Time time) { }
        protected virtual void OnUpdate(Time time) { }
        protected virtual void OnLateUpdate(Time time) { }
        protected virtual void OnRender() { }
        protected virtual void OnEnd() { }
    }

}