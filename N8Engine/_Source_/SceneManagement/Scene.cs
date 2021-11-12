namespace N8Engine.SceneManagement
{
    public abstract class Scene
    {
        public readonly string Name;
        public int Index { get; private set; }
        
        protected abstract void OnLoaded();
        protected abstract void OnUnloaded();
        
        internal void Load() => OnLoaded();
        internal void Unload() => OnUnloaded();
        internal void UpdateIndex(int index) => Index = index;
    }
}