namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        public readonly string Path;
        
        public Sprite(in string path)
        {
            Path = path;
        }
    }
}