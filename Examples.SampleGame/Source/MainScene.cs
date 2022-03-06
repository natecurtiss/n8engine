using System;
using N8Engine.InputSystem;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override void Load()
    {
        Create("player", out var player)
            .AddComponent(new Transform())
            .AddComponent(new Player())
            .AddComponent(new Sprite(this, player, "../../../../Assets/Sprites/n8dev.png"));
    }
}