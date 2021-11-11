using System.Collections.Generic;
using System.Diagnostics;

namespace N8Engine
{
    public sealed class Game
    {
        readonly float _secondsPerUpdate;
        readonly IEnumerable<IModule> _modules;

        internal Game(int targetFps, params IModule[] modules)
        {
            _secondsPerUpdate = 1f / targetFps;
            _modules = modules;
        }

        public void Start()
        {
            InitializeModules();
            var timer = new Stopwatch();
            var last = 0f;
            timer.Start();
            while (true)
            {
                var current = ToSeconds(timer.ElapsedMilliseconds);
                var elapsed = current - last;
                var time = new Time(elapsed);
                while (elapsed >= _secondsPerUpdate)
                {
                    elapsed = Math.Min(elapsed - _secondsPerUpdate, 0f);
                    UpdateModules(time);
                }
                last = current - elapsed;
            }
        }

        void InitializeModules()
        {
            foreach (var module in _modules)
                module.Initialize();
        }

        void UpdateModules(Time time)
        {
            foreach (var module in _modules)
                module.Update(time);
        }

        float ToSeconds(long milliseconds) => milliseconds / 1000f;
    }
}