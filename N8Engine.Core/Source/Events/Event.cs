using System;

// ReSharper disable InconsistentNaming
namespace N8Engine.Events;

public sealed class Event
{
    event Action _evt;
    public void Add(Action action) => _evt += action;
    public void Remove(Action action) => _evt -= action;
    public void Raise() => _evt?.Invoke();
}

public sealed class Event<T>
{
    event Action<T> _evt;
    public void Add(Action<T> action) => _evt += action;
    public void Remove(Action<T> action) => _evt -= action;
    public void Raise(T t) => _evt?.Invoke(t);
}

public sealed class Event<T1, T2>
{
    event Action<T1, T2> _evt;
    public void Add(Action<T1, T2> action) => _evt += action;
    public void Remove(Action<T1, T2> action) => _evt -= action;
    public void Raise(T1 t1, T2 t2) => _evt?.Invoke(t1, t2);
}

public sealed class Event<T1, T2, T3>
{
    event Action<T1, T2, T3> _evt;
    public void Add(Action<T1, T2, T3> action) => _evt += action;
    public void Remove(Action<T1, T2, T3> action) => _evt -= action;
    public void Raise(T1 t1, T2 t2, T3 t3) => _evt?.Invoke(t1, t2, t3);
}

public sealed class Event<T1, T2, T3, T4>
{
    event Action<T1, T2, T3, T4> _evt;
    public void Add(Action<T1, T2, T3, T4> action) => _evt += action;
    public void Remove(Action<T1, T2, T3, T4> action) => _evt -= action;
    public void Raise(T1 t1, T2 t2, T3 t3, T4 t4) => _evt?.Invoke(t1, t2, t3, t4);
}