using System.Collections.Generic;

namespace N8Engine.Mathematics
{
    internal static class SequenceManager
    {
        private static readonly List<Sequence> _sequencesToExecute = new();

        public static void Initialize() => GameLoop.OnUpdate += OnUpdate;

        public static void Add(Sequence sequence) => _sequencesToExecute.Add(sequence);
        
        public static void Remove(Sequence sequence) => _sequencesToExecute.Remove(sequence);

        private static void OnUpdate(float deltaTime)
        {
            foreach (var sequence in _sequencesToExecute.ToArray())
                sequence.Tick(deltaTime);
        }
    }
}