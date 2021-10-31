namespace N8Engine.Mathematics
{
    /// <summary>
    /// A set of extension methods for the numeric <see cref="Vector"/> struct.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns the <see cref="Vector">position</see> of the object adjusted to match a given <see cref="Pivot"/> based on its current <see cref="Vector">position</see> and <see cref="Pivot">Pivot.</see>
        /// </summary>
        /// <param name="position"> The current <see cref="Vector">position</see> of the object. </param>
        /// <param name="currentPivot"> The current <see cref="Pivot"/> of the object. </param>
        /// <param name="sizeOfObject"> The <see cref="Vector">size</see> of the entire object. </param>
        /// <param name="newPivot"> The <see cref="Pivot"/> to adjust the <see cref="Vector">position</see> to. </param>
        /// <seealso cref="IntVectorExtensions.AdjustedToPivot"/>
        public static Vector AdjustedToPivot(this Vector position, Pivot currentPivot, Vector sizeOfObject, Pivot newPivot)
        {
            var moveLeftByHalf = new Vector(-sizeOfObject.X, 0) / 2f;
            var moveRightByHalf = new Vector(sizeOfObject.X, 0) / 2f;
            var moveUpByHalf = new Vector(0, sizeOfObject.Y) / 2f;
            var moveDownByHalf = new Vector(0, -sizeOfObject.Y) / 2f;
            
            // TODO: document this.
            var center = currentPivot switch
            {
                Pivot.Center => position,
                Pivot.Top => position + moveUpByHalf,
                Pivot.Bottom => position + moveDownByHalf,
                Pivot.Left => position + moveLeftByHalf,
                Pivot.Right => position + moveRightByHalf,
                Pivot.TopLeft => position + moveUpByHalf + moveLeftByHalf,
                Pivot.TopRight => position + moveUpByHalf + moveRightByHalf,
                Pivot.BottomLeft => position + moveDownByHalf + moveLeftByHalf,
                Pivot.BottomRight => position + moveDownByHalf + moveRightByHalf,
                var _ => position
            };

            // I've included diagrams here to represent where these numbers come from.
            // The darkest part is the pivot, the lightest part is the object, and the mid-toned shape is where a sample object in the center of the screen is.
            var newPosition = newPivot switch
            {
                Pivot.Center => center,
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓   
//                          ▓▓▓▓▓▓▒▒░░▒▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▒░░▒▒▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ 

                Pivot.Top => center + moveDownByHalf,
//                                ▒▒▒▒▒ 
//                                ▒▒▒▒▒
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ 

                Pivot.Bottom => center + moveUpByHalf,
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓        
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                                ▒▒▒▒▒ 
//                                ▒▒▒▒▒

                Pivot.Right => center + moveLeftByHalf,
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//              ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░ ▒▒▒▒▒  
//              ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░ ▒▒▒▒▒        
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓

                Pivot.Left => center + moveRightByHalf,
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                 ▒▒▒▒▒ ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓  
//                                 ▒▒▒▒▒ ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓        
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓

                Pivot.TopRight => center + moveLeftByHalf + moveDownByHalf,
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓         

                Pivot.TopLeft => center + moveRightByHalf + moveDownByHalf,
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒
//                                       ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓

                Pivot.BottomRight => center + moveLeftByHalf + moveUpByHalf,
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒

                Pivot.BottomLeft => center + moveRightByHalf + moveUpByHalf,
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒

                var _ => center
            };
            return newPosition;
        }

        public static IntVector Rounded(this Vector vector) => new(vector.X.Rounded(), vector.Y.Rounded());
        public static IntVector RoundedUp(this Vector vector) => new(vector.X.RoundedUp(), vector.Y.RoundedUp());
        public static IntVector RoundedDown(this Vector vector) => new(vector.X.RoundedDown(), vector.Y.RoundedDown());
    }
}