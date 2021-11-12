using System.Drawing;

namespace N8Engine.Rendering
{
    readonly struct RenderedPixel
    {
        public readonly Color Color;
        public readonly int SortingOrder;
        public bool IsClear => Color.A == 0;

        public RenderedPixel(Color color, int sortingOrder)
        {
            Color = color;
            SortingOrder = sortingOrder;
        }

        public static RenderedPixel Empty() => new(Color.Transparent, 0);
        public bool IsAbove(RenderedPixel other) => SortingOrder > other.SortingOrder;
        public bool CanRenderAbove(RenderedPixel other) => (IsAbove(other) || other.IsClear) && !IsClear;
    }
}