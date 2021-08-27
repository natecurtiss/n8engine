using System;

namespace N8Engine.Mathematics
{
    public struct IntegerVector : IEquatable<IntegerVector>
    {
        public static bool operator ==(IntegerVector first, IntegerVector second) => first.X == second.X && first.Y == second.Y;
        public static bool operator !=(IntegerVector first, IntegerVector second) => !(first == second);
        public static IntegerVector operator +(IntegerVector first, IntegerVector second) => new(first.X + second.X, first.Y + second.Y);
        public static IntegerVector operator +(IntegerVector vector) => vector;
        public static IntegerVector operator -(IntegerVector first, IntegerVector second) => new(first.X - second.X, first.Y - second.Y);
        public static IntegerVector operator -(IntegerVector vector) => new(-vector.X, -vector.Y);
        public static IntegerVector operator *(IntegerVector vector, int multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        public static IntegerVector operator *(int multiplier, IntegerVector vector) => new(vector.X * multiplier, vector.Y * multiplier);
        public static IntegerVector operator *(IntegerVector first, IntegerVector second) => new(first.X * second.X, first.Y * second.Y);
        public static IntegerVector operator /(IntegerVector vector, int divisor) => new(vector.X / divisor, vector.Y / divisor);
        public static IntegerVector operator /(IntegerVector first, IntegerVector second) => new(first.X / second.X, first.Y / second.Y);
        public static implicit operator IntegerVector(Vector vector) => new((int) vector.X, (int) vector.Y);

        public static readonly IntegerVector Zero = new();
        public static readonly IntegerVector One = new(1);
        public static readonly IntegerVector Up = new(0, 1);
        public static readonly IntegerVector Down = new(0, -1);
        public static readonly IntegerVector Left = new(-1, 0);
        public static readonly IntegerVector Right = new(1, 0);
        
        private readonly int _x;
        private readonly int _y;
        
        public int X
        {
            get => _x;
            set => this = new IntegerVector(value, _y);
        }
        public int Y
        {
            get => _y;
            set => this = new IntegerVector(_x, value);
        }

        public IntegerVector(int bothXAndY)
        {
            _x = bothXAndY;
            _y = bothXAndY;
        }
        
        public IntegerVector(int x, int y)
        {
            _x = x;
            _y = y;
        }
        
        public override bool Equals(object obj) => obj is IntegerVector other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X.ToString()},{Y.ToString()})";
        
        public bool Equals(IntegerVector other) => X.Equals(other.X) && Y.Equals(other.Y);
    }
}