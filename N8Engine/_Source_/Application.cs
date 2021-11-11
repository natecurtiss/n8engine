namespace N8Engine
{
    public static class Application
    {
        public static Game Build(int targetFps) => new
        (
            targetFps
        );
    }
}