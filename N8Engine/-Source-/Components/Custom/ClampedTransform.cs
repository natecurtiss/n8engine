using System;
using N8Engine.Mathematics;
using Math = N8Engine.Mathematics.Math;

namespace N8Engine
{
    public sealed class ClampedTransform : Transform
    {
        public Vector Minimum { get; set; } = new(Math.NEGATIVE_INFINITY, Math.INFINITY);
        public Vector Maximum { get; set; } = new(Math.NEGATIVE_INFINITY, Math.INFINITY);

        protected override Vector OnPositionChanged(Vector value) => new(value.X.ClampedBetween(Minimum.X, Maximum.X), value.Y.ClampedBetween(Minimum.Y, Maximum.Y));
    }

    public static partial class GameObjectExtensions
    {
        public static GameObject ClampX(this GameObject gameObject, float minimumX, float maximumX)
        {
            var transform = gameObject.GetComponent<ClampedTransform>();
            transform.Minimum = new(minimumX, transform.Minimum.Y);
            transform.Maximum = new(maximumX, transform.Maximum.Y);
            return gameObject;
        }
        
        public static GameObject ClampY(this GameObject gameObject, float minimumY, float maximumY)
        {
            var transform = gameObject.GetComponent<ClampedTransform>();
            transform.Minimum = new(transform.Minimum.X, minimumY);
            transform.Maximum = new(transform.Maximum.X, maximumY);
            return gameObject;
        }

        public static GameObject ClampXAndY(this GameObject gameObject, float minimumX, float maximumX, float minimumY, float maximumY)
        {
            var transform = gameObject.GetComponent<ClampedTransform>();
            transform.Minimum = new(minimumX, minimumY);
            transform.Maximum = new(maximumX, maximumY);
            return gameObject;
        }
    }
}