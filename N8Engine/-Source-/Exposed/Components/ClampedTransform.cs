using System;
using N8Engine.Mathematics;
using Math = N8Engine.Mathematics.Math;

namespace N8Engine
{
    public sealed class ClampedTransform : Transform
    {
        Vector _minimum = new(Math.NEGATIVE_INFINITY, Math.INFINITY);
        Vector _maximum = new(Math.NEGATIVE_INFINITY, Math.INFINITY);

        protected override Vector OnPositionChanged(Vector value) => new(value.X.ClampedBetween(_minimum.X, _maximum.X), value.Y.ClampedBetween(_minimum.Y, _maximum.Y));

        public GameObject ClampX(float minimumX, float maximumX)
        {
            _minimum = new(minimumX, _minimum.Y);
            _maximum = new(maximumX, _maximum.Y);
            return GameObject;
        }
        
        public GameObject ClampY(float minimumY, float maximumY)
        {
            _minimum = new(_minimum.X, minimumY);
            _maximum = new(_maximum.X, maximumY);
            return GameObject;
        }

        public GameObject ClampXAndY(float minimumX, float maximumX, float minimumY, float maximumY)
        {
            _minimum = new(_minimum.X, _minimum.Y);
            _maximum = new(_maximum.X, _maximum.Y);
            return GameObject;
        }
    }
}