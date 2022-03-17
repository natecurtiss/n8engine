using System;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleGame;

sealed class PlayerStart : Component
{
    readonly Func<bool> _whenToStart;
    GameObject _gameObject;

    public PlayerStart(Func<bool> whenToStart) => _whenToStart = whenToStart;

    public override void Create(GameObject gameObject, Scene scene) => _gameObject = gameObject;

    public override void Update(Frame frame)
    {
        if (_whenToStart())
        {
            Events.OnPlayerStart.Raise();
            _gameObject.RemoveComponent<PlayerStart>();
        }
    }
}