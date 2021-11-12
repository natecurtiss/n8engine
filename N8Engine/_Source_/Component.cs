namespace N8Engine
{
    public abstract class Component
    {
        public GameObject AttachedTo { get; private set; }

        internal void AttatchTo(GameObject gameObject)
        {
            AttachedTo = gameObject;
            OnStart();
        }
        internal void Detatch()
        {
            AttachedTo = null;
            OnEnd();
        }
        internal void Update(Time time) => OnUpdate(time);
        internal void LateUpdate(Time time) => OnLateUpdate(time);
        internal void Render() => OnRender();
        
        protected virtual void OnStart() { }
        protected virtual void OnUpdate(Time time) { }
        protected virtual void OnLateUpdate(Time time) { }
        protected virtual void OnRender() { }
        protected virtual void OnEnd() { }
    }

}