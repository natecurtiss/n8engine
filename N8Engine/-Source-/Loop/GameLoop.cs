using System;
using System.Diagnostics;
using N8Engine.Loop;

namespace N8Engine.Loop
{
    sealed class GameLoop : ILoopEvents
    {
        public event Action OnStart;
        public event Action<float> OnUpdate;
        
        readonly float _updateRate;
        public int FramesPerSecond { get; private set; }

        public GameLoop(int targetFramerate) => _updateRate = 1f / targetFramerate;

        public void Run()
        {
            OnStart?.Invoke();
            var frames = 0;
            var timer = 0f;
            var previousTimeInMilliseconds = 0.0;
            var stopwatch = new Stopwatch();
            
            const float milliseconds_to_seconds = 1000f;
            const float one_second = 1f;

            stopwatch.Start();
            while (true)
            {
                var currentTimeInMilliseconds = stopwatch.ElapsedMilliseconds;
                var timePassed = (float) (currentTimeInMilliseconds - previousTimeInMilliseconds) / milliseconds_to_seconds;

                if (timePassed >= _updateRate)
                {
                    frames++;
                    timer += timePassed;
                    if (timer >= one_second)
                    {
                        FramesPerSecond = frames;
                        frames = 0;
                        timer = 0f;
                    }
                    previousTimeInMilliseconds = currentTimeInMilliseconds;
                    OnUpdate?.Invoke(timePassed);
                }
            }
        }
    }
}