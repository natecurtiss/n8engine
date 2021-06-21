namespace N8Engine.Core
{
    public sealed class InternalGameObject
    {
        public static implicit operator GameObject(in InternalGameObject internalGameObject) => 
            internalGameObject._gameObject.IsDestroyed ? null : internalGameObject._gameObject;
        
        private readonly GameObject _gameObject;

        internal InternalGameObject(in GameObject gameObject) =>
            _gameObject = gameObject;
    }
}