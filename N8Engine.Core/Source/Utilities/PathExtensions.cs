using System.IO;
using System.Runtime.CompilerServices;

namespace N8Engine.Utilities;

public static class PathExtensions
{
    public static string Find(this string path, [CallerFilePath] string caller = "", int iterations = 8)
    {
        path = $"{caller}/{path}";
        if (File.Exists(path))
            return path;
        var approximated = path;
        for (var i = 0; i < iterations; i++)
        {
            approximated = approximated.Insert(caller.Length, "../");
            if (File.Exists(approximated))
                return approximated;
        }
        return path;
    }
}