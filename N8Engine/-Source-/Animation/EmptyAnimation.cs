namespace N8Engine.Animation
{
    internal sealed class EmptyAnimation : SingleKeyframeAnimation
    {
        protected override void OnKeyframe(GameObject gameObject) { }
    }
}