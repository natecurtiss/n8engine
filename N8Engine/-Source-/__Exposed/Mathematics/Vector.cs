using System;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// A structure with an <see cref="X"/> and <see cref="Y"/> value - used for things like positions and velocity.
    /// </summary>
    public struct Vector : IEquatable<Vector>
    {
        public static bool operator ==(Vector first, Vector second) => first.Equals(second);
        public static bool operator !=(Vector first, Vector second) => !(first == second);
        public static Vector operator +(Vector first, Vector second) => new(first.X + second.X, first.Y + second.Y);
        public static Vector operator +(Vector first, IntVector second) => new(first.X + second.X, first.Y + second.Y);
        public static Vector operator +(IntVector first, Vector second) => new(first.X + second.X, first.Y + second.Y);
        public static Vector operator +(Vector vector) => vector;
        public static Vector operator -(Vector first, Vector second) => new(first.X - second.X, first.Y - second.Y);
        public static Vector operator -(Vector first, IntVector second) => new(first.X - second.X, first.Y - second.Y);
        public static Vector operator -(IntVector first, Vector second) => new(first.X - second.X, first.Y - second.Y);
        public static Vector operator -(Vector vector) => new(-vector.X, -vector.Y);
        public static Vector operator *(Vector vector, float multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        public static Vector operator *(float multiplier, Vector vector) => new(vector.X * multiplier, vector.Y * multiplier);
        public static Vector operator *(Vector first, Vector second) => new(first.X * second.X, first.Y * second.Y);
        public static Vector operator *(Vector first, IntVector second) => new(first.X * second.X, first.Y * second.Y);
        public static Vector operator *(IntVector first, Vector second) => new(first.X * second.X, first.Y * second.Y);
        public static Vector operator /(Vector vector, float divisor) => new(vector.X / divisor, vector.Y / divisor);
        public static Vector operator /(Vector first, Vector second) => new(first.X / second.X, first.Y / second.Y);
        public static Vector operator /(Vector first, IntVector second) => new(first.X / second.X, first.Y / second.Y);
        public static Vector operator /(IntVector first, Vector second) => new(first.X / second.X, first.Y / second.Y);
        public static implicit operator Vector(IntVector intVector) => new(intVector.X, intVector.Y);
        
        /// <summary>
        /// The equivalent of a <see cref="Vector"/> with values of (0, 0).
        /// </summary>
        public static readonly Vector Zero = new();
        /// <summary>
        /// The equivalent of a <see cref="Vector"/> with values of (1, 1).
        /// </summary>
        public static readonly Vector One = new(1f);
        /// <summary>
        /// The equivalent of a <see cref="Vector"/> with values of (0, 1).
        /// </summary>
        public static readonly Vector Up = new(0f, 1f);
        /// <summary>
        /// The equivalent of a <see cref="Vector"/> with values of (0, -1).
        /// </summary>
        public static readonly Vector Down = new(0f, -1f);
        /// <summary>
        /// The equivalent of a <see cref="Vector"/> with values of (-1, 0).
        /// </summary>
        public static readonly Vector Left = new(-1f, 0f);
        /// <summary>
        /// The equivalent of a <see cref="Vector"/> with values of (1, 0).
        /// </summary>
        public static readonly Vector Right = new(1f, 0f);

        readonly float _x;
        readonly float _y;
        
        /// <summary>
        /// The first value in the <see cref="Vector">Vector.</see>
        /// </summary>
        public float X
        {
            get => _x;
            set => this = new Vector(value, _y);
        }
        /// <summary>
        /// The second value in the <see cref="Vector">Vector.</see>
        /// </summary>
        public float Y
        {
            get => _y;
            set => this = new Vector(_x, value);
        }

        /// <summary>
        /// The un-square-rooted magnitude of the <see cref="Vector">Vector.</see>
        /// </summary>
        public float SquareMagnitude => X * X + Y * Y;
        /// <summary>
        /// The magnitude of the <see cref="Vector">Vector.</see>
        /// </summary>
        public float Magnitude => SquareMagnitude.SquareRooted();
        /// <summary>
        /// A <see cref="Vector"/> with <see cref="X"/> and <see cref="Y"/> values of the absolute values of the current <see cref="Vector">Vector's</see> <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Vector AbsoluteValue => new(X.AbsoluteValue(), Y.AbsoluteValue());
        /// <summary>
        /// Returns the <see cref="Vector"/> with a <see cref="Magnitude"/> of 1.
        /// </summary>
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

        /// <summary>
        /// Creates a <see cref="Vector"/> with equal <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Vector(float bothXAndY)
        {
            _x = bothXAndY;
            _y = bothXAndY;
        }
        
        /// <summary>
        /// Creates a <see cref="Vector"/> with specified <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
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

    /// <summary>
    /// A generic <see cref="Vector"/> with a <see cref="First"/> and <see cref="Second"/> value - each of whatever types passed in.
    /// </summary>
    /// <typeparam name="TFirst"> The type of the <see cref="Vector{TFirst, TSecond}">Vector's</see><see cref="First"> first value. </see> </typeparam>
    /// <typeparam name="TSecond"> The type of the <see cref="Vector{TFirst, TSecond}">Vector's</see><see cref="Second"> second value. </see> </typeparam>
    public struct Vector<TFirst, TSecond> : IEquatable<Vector<TFirst, TSecond>>
    {
        public static bool operator ==(Vector<TFirst, TSecond> first, Vector<TFirst, TSecond> right) => first.Equals(right);
        public static bool operator !=(Vector<TFirst, TSecond> first, Vector<TFirst, TSecond> right) => !(first == right);

        readonly TFirst _first;
        readonly TSecond _second;
        
        /// <summary>
        /// The first value of the <see cref="Vector{TFirst, TSecond}">Vector</see> of type <typeparamref name="TFirst">TFirst.</typeparamref>
        /// </summary>
        public TFirst First
        {
            get => _first;
            set => this = new Vector<TFirst, TSecond>(value, _second);
        }
        /// <summary>
        /// The second value of the <see cref="Vector{TFirst, TSecond}">Vector</see> of type <typeparamref name="TSecond">TSecond.</typeparamref>
        /// </summary>
        public TSecond Second
        {
            get => _second;
            set => this = new Vector<TFirst, TSecond>(_first, value);
        }
        
        /// <summary>
        /// Creates a <see cref="Vector{TFirst, TSecond}">Vector</see> with specified <see cref="First"/> and <see cref="Second"/> values.
        /// </summary>
        public Vector(TFirst first, TSecond second)
        {
            _first = first;
            _second = second;
        }
        
        public override bool Equals(object obj) => obj is Vector<TFirst, TSecond> other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(First, Second);
        
        public override string ToString() => $"({First.ToString()},{Second.ToString()})";

        public bool Equals(Vector<TFirst, TSecond> other) => First.Equals(other.First) && Second.Equals(other.Second);
    }
}