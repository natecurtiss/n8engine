namespace N8Engine.Mathematics
{
    public static class IntegerVectorExtensions
    {
        public static IntegerVector AdjustedToPivot(this IntegerVector bottomLeft, IntegerVector sizeOfObject, Pivot pivot) => ((Vector) bottomLeft).AdjustedToPivot(sizeOfObject, pivot);
    }
}