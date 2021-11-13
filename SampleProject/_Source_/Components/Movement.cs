using N8Engine;
using N8Engine.InputSystem;

namespace SampleProject
{
    sealed class Movement : Component
    {
        readonly float _speed;
        TopDownInput _input;
        
        public Movement(float speed) => _speed = speed;

        protected override void OnStart() => _input = GameObject.Get<TopDownInput>();
        protected override void OnUpdate(Time time) => GameObject.Position += _input.Axis * _speed * time.DeltaTime;
    }
}