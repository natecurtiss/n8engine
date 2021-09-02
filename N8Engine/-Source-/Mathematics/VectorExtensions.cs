using static N8Engine.Mathematics.Pivot;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// A set of extension methods for the numeric <see cref="Vector"/> struct.
    /// </summary>
    public static class VectorExtensions
    {
        public static Vector AdjustToPivot(this Vector bottomLeft, Vector sizeOfObject, Pivot pivot)
        {
            var center = bottomLeft - sizeOfObject / 2f;
            var moveLeftByHalf = new Vector(-sizeOfObject.X, 0) / 2f;
            var moveRightByHalf = new Vector(sizeOfObject.X, 0) / 2f;
            var moveUpByHalf = new Vector(0, sizeOfObject.Y) / 2f;
            var moveDownByHalf = new Vector(0, -sizeOfObject.Y) / 2f;
            
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