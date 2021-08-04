using N8Engine;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.SceneManagement;

namespace TestGame
{
    internal sealed class DummyGameObject : GameObject
    {
        private readonly PlayerWalkAnimation _walkAnimation = new();
        private readonly FlippedPlayerWalkAnimation _flippedWalkAnimation = new();
        private readonly PlayerIdleAnimation _idleAnimation = new();
        private readonly FlippedPlayerIdleAnimation _flippedIdleAnimation = new();
        
        protected override void OnStart()
        {
            AnimationPlayer.Animation = _idleAnimation;
            AnimationPlayer.Play();
        }

        protected override void OnUpdate(float deltaTime)
        {
            AnimationPlayer.Animation = Input.MovementAxis.X switch
            {
                > 0 => _walkAnimation,
                < 0 => _flippedWalkAnimation,
                0 when AnimationPlayer.Animation == _walkAnimation => _idleAnimation,
                0 when AnimationPlayer.Animation == _flippedWalkAnimation => _flippedIdleAnimation,
                _ => AnimationPlayer.Animation
            };
            Collider.Velocity = Vector.Right * Input.MovementAxis * 2250 * deltaTime;
        }

        protected override void OnCollision(Collider otherCollider)
        {
            if (otherCollider.GameObject.Name != "wow") return;
            if (SceneManager.CurrentScene.Name == "Sample Scene")
                SceneManager.LoadNextScene();
            else
                SceneManager.LoadPreviousScene();
        }
    }
}