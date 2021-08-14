namespace N8Engine.Mathematics
{
    public sealed class Transform : Component
    {
        public Vector Position { get; set; }
        
        internal Transform(GameObject gameObject) : base(gameObject) { }
    }
}