namespace N8Engine.Animation
{
    public abstract class SingleKeyframeAnimation : Animation
    {
        protected abstract void OnKeyframe(GameObject gameObject, float deltaTime);

        internal override void Tick(GameObject gameObject, float deltaTime) => OnKeyframe(gameObject, deltaTime);
    }
}