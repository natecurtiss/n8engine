using System;
using System.Drawing;

namespace N8Engine.Rendering
{
    public interface IWindow
    {
        internal event Action<Color> OnBackgroundChanged;
        public Color BackgroundColor { get; set; }
        internal void Show();
    }
}