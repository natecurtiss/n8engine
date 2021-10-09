namespace N8Engine.Animation
{
    internal sealed class EmptyAnimation : Animation
    {
        protected override bool ShouldLoop => false;
        protected override Sequence[] Keyframes => new[]
        {
            new Sequence().Wait(0f).Do(() => { })
        };
    }
}