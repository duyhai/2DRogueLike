using System;
using Godot;

public class PlayerFollowingInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        if (gameObject.isDead) return;

        Player player = (Player)gameObject.GetNodeOrNull("../Player");
        gameObject.velocity = (player.Position - gameObject.Position).Normalized() * gameObject.Stats.Speed;
    }
}