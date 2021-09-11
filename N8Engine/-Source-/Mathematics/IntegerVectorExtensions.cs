namespace N8Engine.Mathematics
{
    public static class IntegerVectorExtensions
    {
        public static Vector AdjustToPivot(this IntegerVector bottomLeft, IntegerVector sizeOfObject, Pivot pivot) => ((Vector) bottomLeft).AdjustToPivot(sizeOfObject, pivot);
    }
}