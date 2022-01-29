using System.Diagnostics;

namespace N8Engine;

sealed class Loop
{
    readonly float _updateRate;
    readonly Action<Frame> _onUpdate;

    public Loop(int targetFps, Action<Frame> onUpdate)
    {
        _updateRate = 1f / targetFps;
        _onUpdate = onUpdate;
    }

    public void Start()
    {
        var last = 0L;
        var timer = new Stopwatch();
        var frameTimer = 0f;
        var elapsed = 0f;
        var frames = 0;
        var fps = 0;
        
        const float ms_to_seconds = 0.001f;
        const float one_second = 1f;
        
        while (true)
        {
            var now = timer.ElapsedMilliseconds;
            elapsed += now - last;
            var deltaTime = elapsed * ms_to_seconds;
            frameTimer += deltaTime;
            if (frameTimer >= one_second)
            {
                frameTimer = 0f;
                fps = frames;
            }
            if (deltaTime >= _updateRate)
            {
                _onUpdate(new(deltaTime, fps));
                frames++;
                elapsed = 0f;
                last = now;
            }
        }
    }
}