namespace SampleProject
{
    public sealed class FlippedPlayerJumpAnimation : PlayerJumpAnimation
    {
        protected override bool ShouldFlipHorizontally => true;
    }
}