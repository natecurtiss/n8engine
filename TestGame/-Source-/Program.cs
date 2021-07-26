using System.Reflection;
using N8Engine;

namespace TestGame
{
    internal static class Program
    {
        private static void Main()
        {
            Application.OnLaunch += OnLaunch;
            Application.Start();
        }

        private static void OnLaunch()
        {
            Debug.Log(Assembly.GetExecutingAssembly());
        }
    }
}