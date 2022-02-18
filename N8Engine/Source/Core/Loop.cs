using System;
using System.Diagnostics;

namespace N8Engine;

sealed class Loop
{
    readonly float _secondsPerUpdate;
    readonly Action<Frame> _onUpdate;

    public Loop(int targetFps) => _secondsPerUpdate = 1f / targetFps;
    public Loop(int targetFps, Action<Frame> onUpdate) : this(targetFps) => _onUpdate = onUpdate;

    public void Run()
    {
        var timer = new Stopwatch();
        var last = 0f;
        timer.Start();
        while (true)
        {
            var didUpdate = false;
            var current = ToSeconds(timer.ElapsedMilliseconds);
            var elapsed = current - last;
            var frame = new Frame(elapsed);
            while (elapsed >= _secondsPerUpdate)
            {
                didUpdate = true;
                elapsed = Math.Min(elapsed - _secondsPerUpdate, 0f);
                _onUpdate(frame);
            }
            if (didUpdate) last = current - elapsed;
        }
    }

    static float ToSeconds(long milliseconds) => milliseconds / 1000f;
}