using N8Engine;
using N8Engine.Rendering.Animation;

namespace SampleProject
{
    public abstract class AnimationBase : Animation
    {
        protected readonly string PathToSpritesFolder = PathExtensions.PathToRootFolder + "\\SampleProject\\Sprites\\";
    }
}