using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace N8Engine.Animation
{
    /// <summary>
    /// A sequence of <see cref="Keyframe">Keyframes</see> used by andocum <see cref="Animation">Animation.</see>
    /// </summary>
    public sealed class Sequence
    {
        private readonly List<Keyframe> _keyframes = new();

        internal int Length => _keyframes.Count;
        
        internal Keyframe this[int index] => _keyframes[index];

        public Sequence() { }
        private Sequence(IReadOnlyList<Keyframe> keyframes)
        {
            for (var i = 0; i < keyframes.Count; i++)
                _keyframes.Add(keyframes[i]);
        }

        internal static Sequence Merge(IEnumerable<Sequence> sequences)
        {
            var keyframes = new List<Keyframe>();
            foreach (var sequence in sequences)
                for (var keyframe = 0; keyframe < sequence.Length; keyframe++)
                    keyframes.Add(sequence[keyframe]);
            return new Sequence(keyframes);
        }

        public Sequence Wait([NonNegativeValue] float seconds)
        {
            var keyframe = new Keyframe(seconds, (_, _) => { });
            _keyframes.Add(keyframe);
            return this;
        }

        public Sequence Do([NotNull] Action<GameObject, float> action)
        {
            var keyframe = new Keyframe(0f, action);
            _keyframes.Add(keyframe);
            return this;
        }
        
        public Sequence Do([NotNull] Action<GameObject> action) => Do((gameObject, _) => action(gameObject));
        public Sequence Do([NotNull] Action<float> action) => Do((_, deltaTime) => action(deltaTime));
        public Sequence Do([NotNull] Action action) => Do((_, _) => action());
        
        public Sequence Repeat([NonNegativeValue] int loops)
        {
            var loopsNotCountingTheFirstOne = loops - 1;
            var oldKeyframes = _keyframes.ToArray();
            
            for (var loop = 0; loop < loopsNotCountingTheFirstOne; loop++)
                foreach (var keyframe in oldKeyframes)
                    _keyframes.Add(keyframe);
            return this;
        }
    }
}