using System;
using N8Engine.Rendering;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;
using Shared;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        private static bool _wasSpacebarPressedLastFrame;
        
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                PathExtensions.PathToRootFolder + "\\N8Engine\\-Source-\\Temporary\\sus.n8sprite",
                SpriteRenderer
            );
            Collider.Size = new Vector(20, 15);
            Collider.Offset = Vector.Right * 3;
            // Collider.IsDebugModeEnabled = true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            Console.Title = GameLoop.FramesPerSecond.ToString();
            Collider.Velocity = Input.MovementAxis * 5000 * deltaTime;
            if (Key.Spacebar.WasPressedDownThisFrame())
            {
                if (SceneManager.CurrentScene.Name == "Sample Scene")
                    SceneManager.LoadNextScene();
                else
                    SceneManager.LoadPreviousScene();
            }
        }
    }
}