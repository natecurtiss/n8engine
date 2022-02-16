namespace N8Engine;

public struct Frame
{
    public readonly float DeltaTime;
    
    public Frame(float deltaTime)
    {
        DeltaTime = deltaTime;
    }
}