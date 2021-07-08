using System;
using System.Diagnostics.CodeAnalysis;
using SystemVector2 = System.Numerics.Vector2;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// A struct that contains holds two values (X and Y); useful for representing positions or directions.
    /// </summary>
    [SuppressMessage("ReSharper", "PossiblyImpureMethodCallOnReadonlyVariable")]
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public struct Vector2
    {
        #region Equality Overload Methods

        /// <summary>
        /// Returns true if the <see cref="Vector2"/> is equal to the <see cref="Vector2"/> passed in.
        /// </summary>
        /// <param name="vector"> The <see cref="Vector2"/> to compare to. </param>
        /// <returns> True if the <see cref="Vector2"/>s are equal. </returns>
        public bool Equals(in Vector2 vector) => this == vector;

        /// <summary>
        /// Returns true if the <see cref="Vector2"/> is equal to the object passed in.
        /// </summary>
        /// <param name="obj"> The object to compare to. </param>
        /// <returns> True if the <see cref="Vector2"/> is equal to the object. </returns>
        public override bool Equals(object obj) => obj is Vector2 __other && this == __other;

        public override int GetHashCode() => base.GetHashCode();
        
        #endregion

        #region Operator Overloads

        /// <summary>
        /// Returns true if the <see cref="Vector2"/>s are equal
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> True if the <see cref="Vector2"/>s' X and Y values are equal. </returns>
        public static bool operator ==(in Vector2 first, in Vector2 second) => first.X == second.X && first.Y == second.Y;

        /// <summary>
        /// Returns true if the <see cref="Vector2"/>s are not equal.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> True if the <see cref="Vector2"/>s' X and Y values are not equal. </returns>
        public static bool operator !=(in Vector2 first, in Vector2 second) => !(first == second);
        
        /// <summary>
        /// Adds two <see cref="Vector2"/>s.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> The result of the addition. </returns>
        public static Vector2 operator +(in Vector2 first, in Vector2 second) => new(first.X + second.X, first.Y + second.Y);
        
        /// <summary>
        /// Subtracts two <see cref="Vector2"/>s.
        /// </summary>
        /// <param name="first"> The <see cref="Vector2"/> to subtract from. </param>
        /// <param name="second"> The <see cref="Vector2"/> to subtract by. </param>
        /// <returns> The result of the subtraction. </returns>
        public static Vector2 operator -(in Vector2 first, in Vector2 second) => new(first.X - second.X, first.Y - second.Y);
        
        /// <summary>
        /// Multiplies a <see cref="Vector2"/> by a float.
        /// </summary>
        /// <param name="vector"> A <see cref="Vector2"/>. </param>
        /// <param name="multiplier"> The value to multiply by. </param>
        /// <returns> The result of the multiplication. </returns>
        public static Vector2 operator *(in Vector2 vector, in float multiplier) => new(vector.X * multiplier, vector.Y * multiplier);
        
        /// <summary>
        /// Multiplies a float by a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="multiplier"> The value to multiply by. </param>
        /// <param name="vector"> A <see cref="Vector2"/>. </param>
        /// <returns> The result of the multiplication. </returns>
        public static Vector2 operator *(in float multiplier, in Vector2 vector) => new(vector.X * multiplier, vector.Y * multiplier);
        
        /// <summary>
        /// Multiplies two <see cref="Vector2"/>s by returning the product of each pair of elements.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> The product of the <see cref="Vector2"/>s' pairs of elements; (2, 1) * (3, 2) = (6, 2). </returns>
        public static Vector2 operator *(in Vector2 first, in Vector2 second) => new(first.X * second.X, first.Y * second.Y);
        
        /// <summary>
        /// Divides a <see cref="Vector2"/> by a float.
        /// </summary>
        /// <param name="vector"> A <see cref="Vector2"/>. </param>
        /// <param name="divisor"> The value to divide by. </param>
        /// <returns> The result of the division. </returns>
        public static Vector2 operator /(in Vector2 vector, in float divisor) => new(vector.X / divisor, vector.Y / divisor);
        
        /// <summary>
        /// Divides two <see cref="Vector2"/>s by returning the quotient of each pair of elements.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> The quotient of the <see cref="Vector2"/>s' pairs of elements; (6, 8) / (3, 2) = (2, 4). </returns>
        public static Vector2 operator /(in Vector2 first, in Vector2 second) => new(first.X / second.X, first.Y / second.Y);
        
        #endregion
        
        #region Base Values
        
        /// <summary> The first value of the <see cref="Vector2"/>. </summary>
        public float X { get; set; }
        /// <summary> The second value of the <see cref="Vector2"/>. </summary>
        public float Y { get; set; }
        
        #endregion
        
        #region Shortcuts
        
        /// <summary>
        /// A <see cref="Vector2"/> of (0, 0).
        /// </summary>
        public static Vector2 Zero => new();
        
        /// <summary>
        /// A <see cref="Vector2"/> of (1, 1).
        /// </summary>
        public static Vector2 One => new(1f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (0, 1).
        /// </summary>
        public static Vector2 Up => new(0f, 1f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (0, -1).
        /// </summary>
        public static Vector2 Down => new(0f, -1f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (1, 0).
        /// </summary>
        public static Vector2 Right => new(1f, 0f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (-1, 0).
        /// </summary>
        public static Vector2 Left => new (-1f, 0f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (1, 1).
        /// </summary>
        public static Vector2 UpRight => new(1f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (-1, 1).
        /// </summary>
        public static Vector2 UpLeft => new(-1f, 1f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (1, -1).
        /// </summary>
        public static Vector2 DownRight => new(1f, -1f);
        
        /// <summary>
        /// A <see cref="Vector2"/> of (-1, -1).
        /// </summary>
        public static Vector2 DownLeft => new(-1f);

        #endregion

        #region Properties
        
        /// <summary>
        /// The magnitude/length of the <see cref="Vector2"/>.
        /// </summary>
        public float Magnitude => this.AsSystemVector2().Length();
        
        /// <summary>
        /// The squared magnitude/length of the vector.
        /// </summary>
        public float SquareMagnitude => this.AsSystemVector2().LengthSquared();
        
        /// <summary>
        /// The normalized vector.
        /// </summary>
        public Vector2 Normalized => SystemVector2.Normalize(this.AsSystemVector2()).AsVector2();
        
        /// <summary>
        /// The absolute value of the vector.
        /// </summary>
        public Vector2 AbsoluteValue => new(X.AbsoluteValue(), Y.AbsoluteValue());
        
        /// <summary>
        /// The opposite of the current vector.
        /// </summary>
        public Vector2 Negated => this * -1;
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Creates a <see cref="Vector2"/> with an equal X and Y.
        /// </summary>
        /// <param name="both"> The value of both X and Y. </param>
        public Vector2(in float both)
        {
            X = both;
            Y = both;
        }

        /// <summary>
        /// Created a <see cref="Vector2"/> with an X and Y.
        /// </summary>
        /// <param name="x"> The X value. </param>
        /// <param name="y"> The Y value. </param>
        public Vector2(in float x, in float y)
        {
            X = x;
            Y = y;
        }
        
        #endregion

        #region Instance Methods
        
        /// <summary>
        /// Adds a <see cref="Vector2"/> to the current vector.
        /// </summary>
        /// <param name="addend"> The vector to add by. </param>
        public void Add(in Vector2 addend) => this = this + addend;

        /// <summary>
        /// Subtracts a <see cref="Vector2"/> from the current vector.
        /// </summary>
        /// <param name="difference"> The vector to subtract by. </param>
        public void Subtract(in Vector2 difference) => this = this - difference;
        
        /// <summary>
        /// Multiplies the current <see cref="Vector2"/> by a float.
        /// </summary>
        /// <param name="multiplier"> The float to multiply by. </param>
        public void Multiply(in float multiplier) => this = this * multiplier;

        /// <summary>
        /// Multiplies the current <see cref="Vector2"/> by another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="multiplier"> The vector to multiply by. </param>
        public void Multiply(in Vector2 multiplier) => this = this * multiplier;

        /// <summary>
        /// Divides the current vector by a float.
        /// </summary>
        /// <param name="divisor"> The float to divide by. </param>
        public void Divide(in float divisor) => this = this / divisor;
        
        /// <summary>
        /// Divides the current vector by another vector.
        /// </summary>
        /// <param name="divisor"> The vector to divide by. </param>
        public void Divide(in Vector2 divisor) => this = this / divisor;
        
        /// <summary>
        /// Returns the distance from the current vector to the target vector.
        /// </summary>
        /// <param name="target"> The target vector. </param>
        /// <returns> The distance between the current vector and the target vector. </returns>
        public float DistanceTo(in Vector2 target) =>
            SystemVector2.Distance(this.AsSystemVector2(), target.AsSystemVector2());
        
        /// <summary>
        /// Returns the squared distance from the current vector to the target vector.
        /// </summary>
        /// <param name="target"> The target vector. </param>
        /// <returns> The squared distance between the current vector and the target vector. </returns>
        public float DistanceToSquared(in Vector2 target) =>
            SystemVector2.DistanceSquared(this.AsSystemVector2(), target.AsSystemVector2());

        /// <summary>
        /// Performs a linear interpolation on the current vector to the target vector by an amount clamped between 0 and 1.
        /// </summary>
        /// <param name="target"> The target Vector2 to interpolate to. </param>
        /// <param name="amount"> The amount to interpolate by, clamped between 0 and 1. </param>
        /// <returns> The squared distance between the current vector and the target vector .</returns>
        public void LerpTo(in Vector2 target, in float amount) =>
            this = Lerp(this, target, amount);

        
        /// <summary>
        /// Performs a reflection of the current vector off of a surface's normal vector.
        /// </summary>
        /// <param name="normal"> The normal vector of the surface to reflect off of. </param>
        public void ReflectOffOf(in Vector2 normal) => this = Reflect(this, normal);

        /// <summary>
        /// Returns a reflection of the current vector off of a surface's normal vector.
        /// </summary>
        /// <param name="normal"> The normal of the surface to reflect off of. </param>
        /// <returns> a reflection of the current vector off of a surface's normal vector. </returns>
        public Vector2 ReflectionOff(in Vector2 normal) => Reflect(this, normal);
        
        /// <summary>
        /// Normalizes the current vector.
        /// </summary>
        public void Normalize() => this = this.Normalized;

        /// <summary>
        /// Negates the current vector.
        /// </summary>
        public void Negate() => this *= -1f;

        #endregion
        
        #region Static Methods
        
        /// <summary>
        /// Returns the sum of two vectors. 
        /// </summary>
        /// <param name="first"> The first vector. </param>
        /// <param name="second"> The second vector. </param>
        /// <returns> The sum of the two vectors. </returns>
        public static Vector2 Add(in Vector2 first, in Vector2 second) => first + second;

        /// <summary>
        /// Returns the difference between two vectors.
        /// </summary>
        /// <param name="first"> The first vector. </param>
        /// <param name="second"> The second vector. </param>
        /// <returns> The difference of the two vectors. </returns>
        public static Vector2 Subtract(in Vector2 first, in Vector2 second) => first - second;

        /// <summary>
        /// Returns the product of a vector and a float.
        /// </summary>
        /// <param name="vector"> The vector to multiply. </param>
        /// <param name="multiplier"> The float to multiply by. </param>
        /// <returns> The product of the vector and the float. </returns>
        public static Vector2 Multiply(in Vector2 vector, in float multiplier) => vector * multiplier;

        /// <summary>
        /// Returns the product of a float and a vector.
        /// </summary>
        /// <param name="multiplier"> The float to multiply by. </param>
        /// <param name="vector"> The vector to multiply. </param>
        /// <returns> The product of the float and vector. </returns>
        public static Vector2 Multiply(in float multiplier, in Vector2 vector) => vector * multiplier;
        
        /// <summary>
        /// Returns the product of two vectors by returning the product of each pair of elements.
        /// </summary>
        /// <param name="vector"> The first vector. </param>
        /// <param name="second"> The second vector. </param>
        /// <returns> The product of the vectors' pairs of elements; (2, 1) * (3, 2) = (6, 2). </returns>
        public static Vector2 Multiply(in Vector2 vector, in Vector2 second) => vector * second;
        
        /// <summary>
        /// Returns the quotient of a vector and a float.
        /// </summary>
        /// <param name="dividend"> The vector to divide. </param>
        /// <param name="divisor"> The float to divide by. </param>
        /// <returns> The quotient of the vector and the float. </returns>
        public static Vector2 Divide(in Vector2 dividend, in float divisor) => dividend / divisor;
        
        /// <summary>
        /// Returns the quotient of two vectors by returning the quotient of each pair of elements.
        /// </summary>
        /// <param name="dividend"> The vector to divide. </param>
        /// <param name="divisor"> The vector to divide by. </param>
        /// <returns> The quotient of the vectors' pairs of elements; (6, 8) / (3, 2) = (2, 4). </returns>
        public static Vector2 Divide(in Vector2 dividend, in Vector2 divisor) => dividend / divisor;

        /// <summary>
        /// Returns the Euclidean distance between two vectors using the Pythagorean Theorem.
        /// </summary>
        /// <param name="first"> The first vector. </param>
        /// <param name="second"> The second vector. </param>
        /// <returns> The Euclidean distance between the two vectors. </returns>
        public static float Distance(in Vector2 first, in Vector2 second) => first.DistanceTo(second);

        /// <summary>
        /// Returns the squared Euclidean distance between two vectors using the Pythagorean Theorem.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> The squared Euclidean distance between the two <see cref="Vector2"/>s. </returns>
        public static float DistanceSquared(in Vector2 first, in Vector2 second) => first.DistanceToSquared(second);
        
        /// <summary>
        /// Returns the dot product between two <see cref="Vector2"/>s.
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> The dot product between the two <see cref="Vector2"/>s. </returns>
        public static float DotProduct(in Vector2 first, in Vector2 second) => SystemVector2.Dot(first.AsSystemVector2(), second.AsSystemVector2());
        
        /// <summary>
        /// Returns a linear interpolation between two <see cref="Vector2"/>s by an amount clamped between 0 and 1.
        /// </summary>
        /// <param name="vector"> The <see cref="Vector2"/> to interpolate. </param>
        /// <param name="target"> The target <see cref="Vector2"/> to interpolate to. </param>
        /// <param name="amount"> The amount to interpolate by, clamped between 0 and 1. </param>
        /// <returns> The first <see cref="Vector2"/> interpolated by amount to the target <see cref="Vector2"/>. </returns>
        public static Vector2 Lerp(in Vector2 vector, in Vector2 target, in float amount) =>
            SystemVector2.Lerp(vector.AsSystemVector2(), target.AsSystemVector2(), amount.Clamped01()).AsVector2();

        /// <summary>
        /// Returns a <see cref="Vector2"/> who's elements are the minimum of each of the pairs of elements in the two <see cref="Vector2"/>s; (0,5) and (1, 3) returns (0, 3).
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> A <see cref="Vector2"/> who's elements are the minimum of each of the pairs of elements in the two <see cref="Vector2"/>s; (0,5) and (1, 3) returns (0, 3). </returns>
        public static Vector2 Minimum(Vector2 first, in Vector2 second) =>
            new(Math.Minimum(first.X, second.X), Math.Minimum(first.Y, second.Y));
        
        /// <summary>
        /// Returns a <see cref="Vector2"/> who's elements are the maximum of each of the pairs of elements in the two <see cref="Vector2"/>s; (0,5) and (1, 3) returns (1, 5).
        /// </summary>
        /// <param name="first"> The first <see cref="Vector2"/>. </param>
        /// <param name="second"> The second <see cref="Vector2"/>. </param>
        /// <returns> A <see cref="Vector2"/> who's elements are the maximum of each of the pairs of elements in the two <see cref="Vector2"/>s; (0,5) and (1, 3) returns (1, 5). </returns>
        public static Vector2 Maximum(in Vector2 first, in Vector2 second) =>
            new(Math.Maximum(first.X, second.X), Math.Minimum(first.Y, second.Y));

        /// <summary>
        /// Returns a reflection of the current <see cref="Vector2"/> off of a surface's normal <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector"> The <see cref="Vector2"/> to reflect. </param>
        /// <param name="normal"> The normal <see cref="Vector2"/> of the surface to reflect off of. </param>
        /// <returns> A reflection of the current <see cref="Vector2"/> off of a surface's normal <see cref="Vector2"/>. </returns>
        public static Vector2 Reflect(in Vector2 vector, in Vector2 normal) =>
            SystemVector2.Reflect(vector.AsSystemVector2(), normal.AsSystemVector2()).AsVector2();

        #endregion
    }

    /// <summary>
    /// A static class with extension methods for converting to and from System.Numerics.Vector2 and <see cref="Vector2"/>.
    /// </summary>
    internal static class InternalVector2Extensions
    {
        /// <summary>
        /// Returns a System.Numerics.Vector2 from a N8Engine.Mathematics.Vector2.
        /// </summary>
        /// <param name="vector"> a N8Engine.Mathematics.Vector2. </param>
        /// <returns> a System.Numerics.Vector2. </returns>
        public static SystemVector2 AsSystemVector2(this in Vector2 vector) => new(vector.X, vector.Y);
        
        /// <summary>
        /// Returns a N8Engine.Mathematics.Vector2 from a System.Numerics.Vector2.  
        /// </summary>
        /// <param name="vector"> a System.Numerics.Vector2. </param>
        /// <returns> a N8Engine.Mathematics.Vector2. </returns>
        public static Vector2 AsVector2(this in SystemVector2 vector) => new(vector.X, vector.Y);
    }
}