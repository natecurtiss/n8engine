namespace N8Engine
{
    public abstract class Component
    {
        public readonly GameObject GameObject;
        
        internal Component(GameObject gameObject) => GameObject = gameObject;

        public override string ToString() => GameObject.Name;
    }
}