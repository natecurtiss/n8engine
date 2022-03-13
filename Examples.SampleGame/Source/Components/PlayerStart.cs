using System;
using N8Engine;

namespace SampleGame;

sealed class PlayerStart : Component
{
    readonly Func<bool> _whenToStart;
    GameObject _gameObject;

    public PlayerStart(Func<bool> whenToStart) => _whenToStart = whenToStart;

    public override void Create(GameObject gameObject) => _gameObject = gameObject;

    public override void Update()
    {
        if (_whenToStart())
        {
            Events.OnPlayerStart.Raise();
            _gameObject.RemoveComponent<PlayerStart>();
        }
    }
}