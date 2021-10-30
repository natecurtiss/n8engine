namespace N8Engine.Mathematics
{
    /// <summary>
    /// A set of extension methods for the numeric <see cref="IntVector"/> struct.
    /// </summary>
    public static class IntVectorExtensions
    {
        /// <summary>
        /// Returns the <see cref="IntVector">position</see> of the object adjusted to match a given <see cref="Pivot"/> based on its current <see cref="IntVector">position</see> and <see cref="Pivot">Pivot.</see>
        /// </summary>
        /// <param name="position"> The current <see cref="IntVector">position</see> of the object. </param>
        /// <param name="currentPivot"> The current <see cref="Pivot"/> of the object. </param>
        /// <param name="sizeOfObject"> The <see cref="IntVector">size</see> of the entire object. </param>
        /// <param name="newPivot"> The <see cref="Pivot"/> to adjust the <see cref="IntVector">position</see> to. </param>
        /// <seealso cref="VectorExtensions.AdjustedToPivot"/>
        public static IntVector AdjustedToPivot(this IntVector position, Pivot currentPivot, IntVector sizeOfObject, Pivot newPivot)
        {
            var pos = (Vector) position; 
            var newPosition = pos.AdjustedToPivot(currentPivot, sizeOfObject, newPivot);
            return newPosition;
        }
    }
}