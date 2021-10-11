using System.Collections.Generic;

namespace N8Engine.Animation
{
    internal sealed class EmptyAnimation : FreeAnimation
    {
        protected override bool ShouldLoop => false;
        protected override Sequence[] Keyframes => new[]
        {
            new Sequence().Wait(0f).Do(() => { })
        };
    }
}