namespace N8Engine.Physics
{
    /// <summary>
    /// Settings for every <see cref="Collider"/> and <see cref="PhysicsBody"/> all in one place.
    /// </summary>
    public static class PhysicsSettings
    {
        /// <summary>
        /// Sets every <see cref="Collider.IsVisible">Collider.IsVisible</see> to this value.
        /// </summary>
        public static bool ShouldShowAllColliders { get; set; }
    }
}