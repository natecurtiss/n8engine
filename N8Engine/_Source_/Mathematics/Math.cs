using System.Linq;

namespace N8Engine
{
    public static class Math
    {
        public static float Min(params float[] values) => values.Min();
        public static float Max(params float[] values) => values.Max();
    }
}