using N8Engine.Mathematics;

namespace N8Engine
{
    public class Transform : Component
    {
        internal override bool CanBeAddedByUser => false;

        Vector _position;
        public Vector Position
        {
            get => _position;
            set => _position = OnPositionChanged(value);
        }
        // TODO: add the rest of the transformations.
        // public Vector Scale { get; set; }
        // public Vector Rotation { get; set; }

        protected virtual Vector OnPositionChanged(Vector value) => value;
    }
}