using System;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// A struct that contains holds two <see cref="float">float</see> values - useful for representing positions or directions.
    /// </summary>
    public readonly struct Vector : IEquatable<Vector>
    {
        /// <summary>
        /// Returns true if the <see cref="Vector"/>s are equal
        /// </summary>
        /// <param name="first"> The first <see cref="Vector"/>. </param>
        /// <param name="second"> The second <see cref="Vector"/>. </param>
        /// <returns> True if the <see cref="Vector"/>s' X and Y values are equal. </returns>
        public static bool operator ==(Vector first, Vector second) => first.X == second.X && first.Y == second.Y;
        /// <summary>
        /// Returns true if the <see cref="Vector"/>s are not equal.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector"/>. </param>
        /// <param name="second"> The second <see cref="Vector"/>. </param>
        /// <returns> True if the <see cref="Vector"/>s' X and Y values are not equal. </returns>
        public static bool operator !=(Vector first, Vector second) => !(first == second);
        /// <summary>
        /// Adds two <see cref="Vector"/>s.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector"/>. </param>
        /// <param name="second"> The second <see cref="Vector"/>. </param>
        /// <returns> The result of the addition. </returns>
        public static Vector operator +(Vector first, Vector second) => new(first.X + second.X, first.Y + second.Y);
        /// <summary>
        /// Subtracts two <see cref="Vector"/>s.
        /// </summary>
        /// <param name="first"> The <see cref="Vector"/> to subtract from. </param>
        /// <param name="second"> The <see cref="Vector"/> to subtract by. </param>
        /// <returns> The result of the subtraction. </returns>
        public static Vector operator -(Vector first, Vector second) => new(first.X - second.X, first.Y - second.Y);
        /// <summary>
        /// Multiplies a <see cref="Vector"/> by a float.
        /// </summary>
        /// <param name="vector"> A <see cref="Vector"/>. </param>
        /// <param name="multiplier"> The value to multiply by. </param>
        /// <returns> The result of the multiplication. </returns>
        public static Vector operator *(Vector vector, float multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        /// <summary>
        /// Multiplies a float by a <see cref="Vector"/>.
        /// </summary>
        /// <param name="multiplier"> The value to multiply by. </param>
        /// <param name="vector"> A <see cref="Vector"/>. </param>
        /// <returns> The result of the multiplication. </returns>
        public static Vector operator *(float multiplier, Vector vector) => new(vector.X * multiplier, vector.Y * multiplier);
        /// <summary>
        /// Multiplies two <see cref="Vector"/>s by returning the product of each pair of elements.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector"/>. </param>
        /// <param name="second"> The second <see cref="Vector"/>. </param>
        /// <returns> The product of the <see cref="Vector"/>s' pairs of elements; (2, 1) * (3, 2) = (6, 2). </returns>
        public static Vector operator *(Vector first, Vector second) => new(first.X * second.X, first.Y * second.Y);
        /// <summary>
        /// Divides a <see cref="Vector"/> by a float.
        /// </summary>
        /// <param name="vector"> A <see cref="Vector"/>. </param>
        /// <param name="divisor"> The value to divide by. </param>
        /// <returns> The result of the division. </returns>
        public static Vector operator /(Vector vector, float divisor) => new(vector.X / divisor, vector.Y / divisor);
        /// <summary>
        /// Divides two <see cref="Vector"/>s by returning the quotient of each pair of elements.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector"/>. </param>
        /// <param name="second"> The second <see cref="Vector"/>. </param>
        /// <returns> The quotient of the <see cref="Vector"/>s' pairs of elements; (6, 8) / (3, 2) = (2, 4). </returns>
        public static Vector operator /(Vector first, Vector second) => new(first.X / second.X, first.Y / second.Y);
        
        /// <summary> The first value of the <see cref="Vector"/>. </summary>
        public readonly float X;
        /// <summary> The second value of the <see cref="Vector"/>. </summary>
        public readonly float Y;

        /// <summary>
        /// A <see cref="Vector"/> of (0, 0).
        /// </summary>
        public static Vector Zero => new();
        /// <summary>
        /// A <see cref="Vector"/> of (1, 1).
        /// </summary>
        public static Vector One => new(1f);
        /// <summary>
        /// A <see cref="Vector"/> of (0, 1).
        /// </summary>
        public static Vector Up => new(0f, 1f);
        /// <summary>
        /// A <see cref="Vector"/> of (0, -1).
        /// </summary>
        public static Vector Down => new(0f, -1f);
        /// <summary>
        /// A <see cref="Vector"/> of (-1, 0).
        /// </summary>
        public static Vector Left => new(-1f, 0f);
        /// <summary>
        /// A <see cref="Vector"/> of (1, 0).
        /// </summary>
        public static Vector Right => new(1f, 0f);
        /// <summary>
        /// The un-square-rooted <see cref="Magnitude"/> of the <see cref="Vector">Vector.</see>
        /// </summary>
        public float SquareMagnitude => X * X + Y * Y;
        /// <summary>
        /// The length of the <see cref="Vector">Vector.</see>
        /// </summary>
        public float Magnitude => SquareMagnitude.SquareRoot();
        /// <summary>
        /// A copy of the <see cref="Vector"/> with a <see cref="Magnitude"/> of 1.
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
        /// Creates a <see cref="Vector"/> with an equal X and Y.
        /// </summary>
        /// <param name="both"> The value of both X and Y. </param>
        public Vector(float both)
        {
            X = both;
            Y = both;
        }

        /// <summary>
        /// Created a <see cref="Vector"/> with an X and Y.
        /// </summary>
        /// <param name="x"> The X value. </param>
        /// <param name="y"> The Y value. </param>
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public bool Equals(Vector other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object obj) => obj is Vector other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X.ToString()},{Y.ToString()})";
    }
}