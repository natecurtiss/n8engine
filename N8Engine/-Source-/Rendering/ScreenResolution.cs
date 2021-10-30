using N8Engine.External;
using N8Engine.External.User;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public readonly struct ScreenResolution
    {
        public readonly IntVector Size;
        
        public ScreenResolution(float widthScale, float heightScale)
        {
            var scale = new Vector(widthScale, heightScale);
            var screenSize = UserMetrics.MonitorSize;
            var windowSize = (screenSize / scale).Rounded();
            Size = windowSize;
        }
    }
}