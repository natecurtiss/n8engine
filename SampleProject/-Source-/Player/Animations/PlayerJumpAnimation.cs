using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerJumpAnimation : SingleFrameAnimation
    {
        protected override Sprite Sprite => new(SpritesFolder.Path + "player_5.png", Vector.Down);
    }
}