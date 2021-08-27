using System;
using System.Collections.Generic;

namespace N8Engine.Mathematics
{
    public struct Vector : IEquatable<Vector>
    {
        public static bool operator ==(Vector first, Vector second) => first.X == second.X && first.Y == second.Y;
        public static bool operator !=(Vector first, Vector second) => !(first == second);
        public static Vector operator +(Vector first, Vector second) => new(first.X + second.X, first.Y + second.Y);
        public static Vector operator +(Vector vector) => vector;
        public static Vector operator -(Vector first, Vector second) => new(first.X - second.X, first.Y - second.Y);
        public static Vector operator -(Vector vector) => new(-vector.X, -vector.Y);
        public static Vector operator *(Vector vector, float multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        public static Vector operator *(float multiplier, Vector vector) => new(vector.X * multiplier, vector.Y * multiplier);
        public static Vector operator *(Vector first, Vector second) => new(first.X * second.X, first.Y * second.Y);
        public static Vector operator /(Vector vector, float divisor) => new(vector.X / divisor, vector.Y / divisor);
        public static Vector operator /(Vector first, Vector second) => new(first.X / second.X, first.Y / second.Y);
        public static implicit operator Vector(IntegerVector integerVector) => new(integerVector.X, integerVector.Y);
        
        public static readonly Vector Zero = new();
        public static readonly Vector One = new(1f);
        public static readonly Vector Up = new(0f, 1f);
        public static readonly Vector Down = new(0f, -1f);
        public static readonly Vector Left = new(-1f, 0f);
        public static readonly Vector Right = new(1f, 0f);
        
        private readonly float _x;
        private readonly float _y;
        
        public float X
        {
            get => _x;
            set => this = new Vector(value, _y);
        }
        public float Y
        {
            get => _y;
            set => this = new Vector(_x, value);
        }

        public float SquareMagnitude => X * X + Y * Y;
        public float Magnitude => SquareMagnitude.SquareRooted();
        public Vector AbsoluteValue => new(X.AbsoluteValue(), Y.AbsoluteValue());
        public Vector Normalized
        {
            get
            {
                var magnitude = Magnitude;
                if (magnitude > 0)
                    return this / magnitude;
                return Zero;
            }
        }

        public Vector(float bothXAndY)
        {
            _x = bothXAndY;
            _y = bothXAndY;
        }
        
        public Vector(float x, float y)
        {
            _x = x;
            _y = y;
        }
        
        public override bool Equals(object obj) => obj is Vector other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X.ToString()},{Y.ToString()})";
        
        public bool Equals(Vector other) => X.Equals(other.X) && Y.Equals(other.Y);
    }

    public struct Vector<TX, TY> : IEquatable<Vector<TX, TY>>
    {
        private readonly TX _x;
        private readonly TY _y;
        
        public TX X
        {
            get => _x;
            set => this = new Vector<TX, TY>(value, _y);
        }
        public TY Y
        {
            get => _y;
            set => this = new Vector<TX, TY>(_x, value);
        }
        
        public Vector(TX x, TY y)
        {
            _x = x;
            _y = y;
        }
        
        public override bool Equals(object obj) => obj is Vector<TX, TY> other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(X, Y);
        
        public override string ToString() => $"({X.ToString()},{Y.ToString()})";

        public bool Equals(Vector<TX, TY> other) => X.Equals(other.X) && Y.Equals(other.Y);
    }
}