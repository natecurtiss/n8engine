namespace TestGame
{
    public sealed class FlippedPlayerWalkAnimation : PlayerWalkAnimation
    {
        protected override bool ShouldFlipHorizontally() => true;
    }
}