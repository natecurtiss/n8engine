using System;

namespace N8Engine
{
    public static class Program
    {
        private static Game _game;
        
        private static void Main(string[] arguments)
        {
            _game = new Game();
            Console.WriteLine("           ");
            Console.ReadKey();
        }
    }
}