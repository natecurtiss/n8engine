using System.IO;

namespace N8Engine.Utilities;

public static class PathApproximation
{
    public static string Find(this string path, int iterations = 8)
    {
        if (File.Exists(path))
            return path;
        var approximated = path;
        for (var i = 0; i < iterations; i++)
        {
            approximated = $"../{approximated}";
            if (File.Exists(approximated))
                return approximated;
        }
        return path;
    }
}