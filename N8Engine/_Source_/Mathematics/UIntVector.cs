using System;

namespace N8Engine.Mathematics
{
    public struct UIntVector
    {
        public static bool operator ==(UIntVector first, UIntVector second) => first.Equals(second);
        public static bool operator !=(UIntVector first, UIntVector second) => !(first == second);
        public static UIntVector operator +(UIntVector first, UIntVector second) => new(first.X + second.X, first.Y + second.Y);
        public static UIntVector operator +(UIntVector vector) => vector;
        public static UIntVector operator -(UIntVector first, UIntVector second) => new(first.X - second.X, first.Y - second.Y);
        public static UIntVector operator *(UIntVector vector, uint multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        public static UIntVector operator *(uint multiplier, UIntVector vector) => new(vector.X * multiplier, vector.Y * multiplier);
        public static UIntVector operator *(UIntVector first, UIntVector second) => new(first.X * second.X, first.Y * second.Y);
        public static UIntVector operator /(UIntVector vector, uint divisor) => new(vector.X / divisor, vector.Y / divisor);
        public static UIntVector operator /(UIntVector first, UIntVector second) => new(first.X / second.X, first.Y / second.Y);
        
        readonly uint _x;
        readonly uint _y;
        
        public uint X
        {
            get => _x;
            set => this = new UIntVector(value, _y);
        }
        public uint Y
        {
            get => _y;
            set => this = new UIntVector(_x, value);
        }
        
        public UIntVector(uint both)
        {
            _x = both;
            _y = both;
        }
        
        public UIntVector(uint x, uint y)
        {
            _x = x;
            _y = y;
        }
        
        public override bool Equals(object obj) => obj is UIntVector other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"({X.ToString()},{Y.ToString()})";
        public bool Equals(UIntVector other) => X.Equals(other.X) && Y.Equals(other.Y);
    }
}