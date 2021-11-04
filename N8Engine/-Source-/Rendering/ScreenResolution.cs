using N8Engine.External.User;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public readonly struct ScreenResolution
    {
        public static implicit operator IntVector(ScreenResolution screenResolution) => screenResolution._size;

        readonly IntVector _size;
        
        public ScreenResolution(float widthScale, float heightScale)
        {
            var scale = new Vector(widthScale, heightScale);
            var windowSize = (UserMetrics.MonitorSize * scale).Rounded();
            _size = windowSize;
        }
    }
}