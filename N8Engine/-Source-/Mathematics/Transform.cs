using System.Collections.Generic;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// The <see cref="Component"/> of a <see cref="GameObject"/> that handles <see cref="Transform.Position">position,</see> rotation, and scaling (only position as of now, but this will probably change).
    /// </summary>
    public sealed class Transform : Component
    {
        /// <summary>
        /// The position of the <see cref="GameObject"/> in world space.
        /// </summary>
        public Vector Position { get; set; }

        internal Transform(GameObject gameObject) : base(gameObject) { }
    }
}