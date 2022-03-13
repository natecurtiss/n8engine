using System;

namespace SampleGame;

sealed class Event
{
    event Action OnEvent;
    public void Add(Action action) => OnEvent += action;
    public void Remove(Action action) => OnEvent -= action;
    public void Raise() => OnEvent?.Invoke();
}