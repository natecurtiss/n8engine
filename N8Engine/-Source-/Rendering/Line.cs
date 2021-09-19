using System.Text;
using N8Engine.Mathematics;
using Math = N8Engine.Mathematics.Math;

namespace N8Engine.Rendering
{
    public sealed class Line : GameObject
    {
        private string _color = string.Empty;
        
        protected override void OnStart() => SpriteRenderer.SortingOrder = Math.INFINITY;
        
        public Line Copy(Vector position)
        {
            var line = GameObject.Create<Line>("line");
            if (_color != string.Empty)
                line.MakeColor(_color);
            line.Transform.Position = position;
            return line;
        }

        public Line MakeColor(string color)
        {
            _color = color;
            var output = new StringBuilder();
            var pixels = new string[1];
            AddLine(output, color);
            pixels[0] = output.ToString();
            SpriteRenderer.Sprite = new Sprite(pixels);
            return this;
        }

        private void AddLine(StringBuilder output, string color)
        {
            for (var index = 0; index < Window.Width; index++)
                output.Append($"{{{color},{color}}}");
        }
    }
}