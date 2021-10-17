namespace N8Engine.Animation
{
    /// <summary>
    /// Same as <see cref="N8Engine.Animation.Animation.Nothing">Animation.Nothing.</see>
    /// </summary>
    internal sealed class EmptyAnimation : FreeAnimation
    {
        protected override bool ShouldLoop => false;
        protected override Sequence[] Keyframes => new[]
        {
            new Sequence().Wait(0f).Do(() => { })
        };
    }
}