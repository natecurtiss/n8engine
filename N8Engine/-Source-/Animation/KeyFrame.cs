using System;

namespace N8Engine.Animation
{
    public abstract partial class Animation
    {
        protected readonly struct Keyframe
        {
            internal readonly float Time;
            internal readonly Action<GameObject, float> OnTick;

            public Keyframe(float time, Action<GameObject, float> onTick)
            {
                Time = time;
                OnTick = onTick;
            }
        }
    }
}