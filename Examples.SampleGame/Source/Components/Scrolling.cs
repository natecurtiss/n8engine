namespace SampleGame;

sealed class Scrolling
{
    public bool IsScrolling { get; private set; }
    public float Speed { get; }
    
    public Scrolling(float speed) => Speed = speed;

    public void Start() => IsScrolling = true;
    public void Stop() => IsScrolling = false;
}