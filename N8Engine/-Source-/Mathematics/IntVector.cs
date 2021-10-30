using System;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// A <see cref="Vector"/> with <see cref="int">integer</see> values.
    /// </summary>
    public struct IntVector : IEquatable<IntVector>
    {
        public static bool operator ==(IntVector first, IntVector second) => first.Equals(second);
        public static bool operator !=(IntVector first, IntVector second) => !(first == second);
        public static IntVector operator +(IntVector first, IntVector second) => new(first.X + second.X, first.Y + second.Y);
        public static IntVector operator +(IntVector vector) => vector;
        public static IntVector operator -(IntVector first, IntVector second) => new(first.X - second.X, first.Y - second.Y);
        public static IntVector operator -(IntVector vector) => new(-vector.X, -vector.Y);
        public static IntVector operator *(IntVector vector, int multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        public static IntVector operator *(int multiplier, IntVector vector) => new(vector.X * multiplier, vector.Y * multiplier);
        public static IntVector operator *(IntVector first, IntVector second) => new(first.X * second.X, first.Y * second.Y);
        public static IntVector operator /(IntVector vector, int divisor) => new(vector.X / divisor, vector.Y / divisor);
        public static IntVector operator /(IntVector first, IntVector second) => new(first.X / second.X, first.Y / second.Y);
        public static implicit operator IntVector(Vector vector) => new(vector.X.RoundedDown(), vector.Y.RoundedDown());

        /// <summary>
        /// The equivalent of an <see cref="IntVector"/> with values of (0, 0).
        /// </summary>
        public static readonly IntVector Zero = new();
        /// <summary>
        /// The equivalent of an <see cref="IntVector"/> with values of (1, 1).
        /// </summary>
        public static readonly IntVector One = new(1);
        /// <summary>
        /// The equivalent of an <see cref="IntVector"/> with values of (0, 1).
        /// </summary>
        public static readonly IntVector Up = new(0, 1);
        /// <summary>
        /// The equivalent of an <see cref="IntVector"/> with values of (0, -1).
        /// </summary>
        public static readonly IntVector Down = new(0, -1);
        /// <summary>
        /// The equivalent of an <see cref="IntVector"/> with values of (-1, 0).
        /// </summary>
        public static readonly IntVector Left = new(-1, 0);
        /// <summary>
        /// The equivalent of an <see cref="IntVector"/> with values of (1, 0).
        /// </summary>
        public static readonly IntVector Right = new(1, 0);

        readonly int _x;
        readonly int _y;
        
        /// <summary>
        /// The first value in the <see cref="IntVector">IntegerVector.</see>
        /// </summary>
        public int X
        {
            get => _x;
            set => this = new IntVector(value, _y);
        }
        /// <summary>
        /// The second value in the <see cref="IntVector">IntegerVector.</see>
        /// </summary>
        public int Y
        {
            get => _y;
            set => this = new IntVector(_x, value);
        }

        /// <summary>
        /// Creates an <see cref="IntVector"/> with equal <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public IntVector(int bothXAndY)
        {
            _x = bothXAndY;
            _y = bothXAndY;
        }
        
        /// <summary>
        /// Creates an <see cref="IntVector"/> with specified <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public IntVector(int x, int y)
        {
            _x = x;
            _y = y;
        }
        
        public override bool Equals(object obj) => obj is IntVector other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X.ToString()},{Y.ToString()})";
        
        public bool Equals(IntVector other) => X.Equals(other.X) && Y.Equals(other.Y);
    }
}