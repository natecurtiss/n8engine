namespace N8Engine.Animation
{
    internal sealed class EmptyAnimation : Animation
    {
        protected override bool ShouldLoop => false;
        protected override Keyframe[] Keyframes => new Keyframe[]
        {
            Wait(0f)
        };
    }
}