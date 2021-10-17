using System.Diagnostics.CodeAnalysis;
using N8Engine;
using N8Engine.Events;

namespace SampleProject
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class EventManager
    {
        public static readonly Event OnKeyCollected = new();
    }
}