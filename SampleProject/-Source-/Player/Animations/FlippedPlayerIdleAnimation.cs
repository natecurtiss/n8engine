namespace SampleProject
{
    public sealed class FlippedPlayerIdleAnimation : PlayerIdleAnimation
    {
        protected override bool ShouldFlipHorizontally() => true;
    }
}