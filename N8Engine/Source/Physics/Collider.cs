using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    public sealed class Collider
    {
        private readonly Transform _transform;

        internal DebugRectangle DebugRectangle;

        private Rectangle _rectangle;
        private Vector _size;
        
        public bool DebugModeEnabled { get; set; }
        public Vector Offset { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                _rectangle = new Rectangle(value, _transform.Position + Offset);
                DebugRectangle = new DebugRectangle(value, _transform.Position + Offset);
                _size = value;
            }
        }
        
        internal Collider(in Transform transform)
        {
            _transform = transform;
            GameLoop.OnPhysicsUpdate += PhysicsUpdate;
        }

        private void PhysicsUpdate()
        {
            
        }
    }
}