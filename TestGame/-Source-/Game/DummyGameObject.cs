using N8Engine.Rendering;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.SceneManagement;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                PathExtensions.PathToRootFolder + "\\TestGame\\Sprites\\sus.n8sprite",
                SpriteRenderer
            );
            Collider.Size = new Vector(20, 15);
            Collider.Offset = Vector.Right * 3;
            // Collider.IsDebugModeEnabled = true;
            // throw new Exception("test lol");
        }

        protected override void OnUpdate(float deltaTime)
        {
            Collider.Velocity = Input.MovementAxis * 2250 * deltaTime;
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