using static N8Engine.Mathematics.Pivot;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// A set of extension methods for the numeric <see cref="Vector"/> struct.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns the <see cref="Vector">position</see> of the object adjusted the to match a given <see cref="Pivot"/> based on its current <see cref="Vector">position</see> (assuming it's the bottom left of the object) and its total size.
        /// <para>In short: we just change the <see cref="Pivot"/> of an object with a bottom left <see cref="Pivot"/>
        /// (things generally start at 0, 0 and work their way up, which is why it's assumed that the current <see cref="Pivot"/> is the bottom left.)</para>
        /// </summary>
        public static Vector AdjustToPivot(this Vector bottomLeft, Vector sizeOfObject, Pivot pivot)
        {
            var center = bottomLeft - sizeOfObject / 2f;
            var moveLeftByHalf = new Vector(-sizeOfObject.X, 0) / 2f;
            var moveRightByHalf = new Vector(sizeOfObject.X, 0) / 2f;
            var moveUpByHalf = new Vector(0, sizeOfObject.Y) / 2f;
            var moveDownByHalf = new Vector(0, -sizeOfObject.Y) / 2f;
            
            // I've included diagrams here to represent where these numbers come from.
            // The darkest part is the pivot, the lightest part is the object, and the mid-toned shape is where the center pivot is.
            return pivot switch
            {
                Center => center,
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓   
//                          ▓▓▓▓▓▓▒▒░░▒▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▒░░▒▒▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ 

                Top => center + moveDownByHalf,
//                                ▒▒▒▒▒ 
//                                ▒▒▒▒▒
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ 

                Bottom => center + moveUpByHalf,
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  
//                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓        
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                          ▓▓▓▓▓▓░░░░░▓▓▓▓▓▓
//                                ▒▒▒▒▒ 
//                                ▒▒▒▒▒

                Right => center + moveLeftByHalf,
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//              ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░ ▒▒▒▒▒  
//              ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░ ▒▒▒▒▒        
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓

                Left => center + moveRightByHalf,
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                 ▒▒▒▒▒ ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓  
//                                 ▒▒▒▒▒ ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓        
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓

                TopRight => center + moveLeftByHalf + moveDownByHalf,
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓         

                TopLeft => center + moveRightByHalf + moveDownByHalf,
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒
//                                       ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ░░░░░▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//                                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓

                BottomRight => center + moveLeftByHalf + moveUpByHalf,
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//               ▓▓▓▓▓▓▓▓▓▓▓▓░░░░░
//                                 ▒▒▒▒▒
//                                 ▒▒▒▒▒

                BottomLeft => center + moveRightByHalf + moveUpByHalf,
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
        }
    }
}