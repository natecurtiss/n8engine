namespace TestGame
{
    public sealed class FlippedPlayerIdleAnimation : PlayerIdleAnimation
    {
        protected override bool ShouldFlipHorizontally() => true;
    }
}