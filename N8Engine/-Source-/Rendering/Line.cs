using System;
using System.Text;
using Math = N8Engine.Mathematics.Math;

namespace N8Engine.Rendering
{
    public sealed class Line : GameObject
    {
        protected override void OnStart() => SpriteRenderer.SortingOrder = Math.INFINITY;

        public void MakeColor(string color)
        {
            var output = new StringBuilder();
            var pixels = new string[1];
            AddLine(output, color);
            pixels[0] = output.ToString();
            SpriteRenderer.Sprite = new Sprite(pixels);
        }

        private void AddLine(StringBuilder output, string color)
        {
            for (var index = 0; index < Window.Width; index++)
                output.Append($"{{{color},{color}}}"); 
        }
    }
}