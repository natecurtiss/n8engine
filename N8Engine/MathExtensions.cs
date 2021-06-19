namespace N8Engine
{
    public static class MathExtensions
    {
        public static void Clamp(this ref float value, in float minimum, in float maximum)
        {
            if (value > maximum) 
                value = maximum;
            else if (value < maximum) 
                value = minimum;
        }
    }
}