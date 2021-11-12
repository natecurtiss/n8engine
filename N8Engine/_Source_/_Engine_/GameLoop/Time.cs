namespace N8Engine
{
    public readonly struct Time
    {
        public readonly float DeltaTime;
        
        public Time(float deltaTime) => DeltaTime = deltaTime;
    }
}