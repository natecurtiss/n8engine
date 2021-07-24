using System;
using N8Engine.Rendering;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        private static bool _wasSpacebarPressedLastFrame;
        
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                @"C:\Users\NateDawg\RiderProjects\N8Engine" + @"\N8Engine\-Source-\Temporary\sus.n8sprite",
                SpriteRenderer
            );
            Collider.Size = new Vector(20, 15);
            Collider.Offset = Vector.Right * 3;
            // Collider.IsDebugModeEnabled = true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            Console.Title = SceneManager.CurrentScene.Name;
            Collider.Velocity = Input.MovementAxis * 2000 * deltaTime;
            if (Key.Spacebar.IsPressedDown())
            {
                if (SceneManager.CurrentScene.Name == "Sample Scene")
                    SceneManager.LoadNextScene();
                else
                    SceneManager.LoadPreviousScene();
            }
        }
    }
}