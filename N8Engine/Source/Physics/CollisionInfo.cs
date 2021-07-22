using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    public readonly struct CollisionInfo
    {
        public static implicit operator bool(CollisionInfo collisionInfo) => collisionInfo.DidCollide;
            
        public readonly bool DidCollide;
        public readonly Direction DirectionOfCollision;

        public CollisionInfo(Direction directionOfCollision, bool didCollide)
        {
            DirectionOfCollision = directionOfCollision;
            DidCollide = didCollide;
        }
    }
}