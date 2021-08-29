using System.Collections.Generic;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// The component of a <see cref="GameObject"/> that handles <see cref="Transform.Position">position,</see> rotation, and scaling (only position as of now, but this will probably change).
    /// </summary>
    public sealed class Transform : Component
    {
        /// <summary>
        /// The position of the <see cref="GameObject"/> in world space.
        /// </summary>
        public Vector Position { get; set; }

        readonly List<Sequence> _playingSequences = new();

        internal Transform(GameObject gameObject) : base(gameObject) { }

        public Sequence MoveTo(Vector targetPosition, float duration, bool shouldKillPlayingSequences = false)
        {
            var speed = targetPosition - Position;
            var sequence = new Sequence(deltaTime => Position += speed * deltaTime, duration);
            sequence.OnComplete(() => _playingSequences.Remove(sequence)).Play();
            if (shouldKillPlayingSequences)
                foreach (var playingSequences in _playingSequences.ToArray())
                    playingSequences.Kill();
            _playingSequences.Add(sequence);
            return sequence;
        }

        public Sequence MoveInDirection(Direction direction, float distance, float duration, bool shouldKillPlayingSequences = false) => MoveTo(Position + direction.AsVector() * distance, duration, shouldKillPlayingSequences);
    }
}