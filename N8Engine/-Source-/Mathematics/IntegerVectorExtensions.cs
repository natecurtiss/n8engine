namespace N8Engine.Mathematics
{
    /// <summary>
    /// A set of extension methods for the numeric <see cref="IntegerVector"/> struct.
    /// </summary>
    public static class IntegerVectorExtensions
    {
        /// <summary>
        /// Returns the <see cref="IntegerVector">position</see> of the object adjusted to match a given <see cref="Pivot"/> based on its current <see cref="IntegerVector">position</see> and <see cref="Pivot">Pivot.</see>
        /// </summary>
        /// <param name="position"> The current <see cref="IntegerVector">position</see> of the object. </param>
        /// <param name="currentPivot"> The current <see cref="Pivot"/> of the object. </param>
        /// <param name="sizeOfObject"> The <see cref="IntegerVector">size</see> of the entire object. </param>
        /// <param name="newPivot"> The <see cref="Pivot"/> to adjust the <see cref="IntegerVector">position</see> to. </param>
        /// <seealso cref="VectorExtensions.AdjustedToPivot"/>
        public static IntegerVector AdjustedToPivot(this IntegerVector position, Pivot currentPivot, IntegerVector sizeOfObject, Pivot newPivot)
        {
            var pos = (Vector) position; 
            var newPosition = pos.AdjustedToPivot(currentPivot, sizeOfObject, newPivot);
            return newPosition;
        }
    }
}