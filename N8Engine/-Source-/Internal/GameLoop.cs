using System;
using System.Diagnostics;

namespace N8Engine.Internal
{
    sealed class GameLoop : IInternalEvents, IUpdateEvents, IRenderingEvents
    {
        readonly float _updateRate;

        public event Action OnInternalStart;
        public event Action OnInternalPreUpdate;
        public event Action<float> OnUpdate;
        public event Action<float> OnPhysicsUpdate;
        public event Action<float> OnLateUpdate;
        public event Action OnPreRender;
        public event Action OnRender;
        public event Action OnPostRender;
        
        public int FramesPerSecond { get; private set; }
        // TODO: probably move these things into their own classes.
        public IInternalEvents InternalEvents => this;
        public IUpdateEvents UpdateEvents => this;
        public IRenderingEvents RenderingEvents => this;

        public GameLoop(int targetFramerate) => _updateRate = 1f / targetFramerate;

        public void Run()
        {
            OnInternalStart?.Invoke();
            
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
                    Tick(timePassed);
                }
            }
        }

        void Tick(float deltaTime)
        {
            OnInternalPreUpdate?.Invoke();
            
            OnUpdate?.Invoke(deltaTime);
            OnPhysicsUpdate?.Invoke(deltaTime);
            OnLateUpdate?.Invoke(deltaTime);

            OnPreRender?.Invoke();
            OnRender?.Invoke();
            OnPostRender?.Invoke();
        }
    }
}