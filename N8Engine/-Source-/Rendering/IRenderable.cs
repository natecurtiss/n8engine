namespace N8Engine.Rendering
{
    interface IRenderable
    {
        // TODO: maybe make this a 2d array?
        internal IPixel[] Pixels { get; }
    }
}