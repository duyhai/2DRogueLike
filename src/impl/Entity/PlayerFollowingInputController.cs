using System;
using Godot;

public class PlayerFollowingInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        if (gameObject.isDead) return;

        gameObject.velocity = Vector2.Zero;
        if (gameObject.IsInGroup("enemy"))
        {
            Enemy enemy = (Enemy)gameObject;
            if (!enemy.PlayerInSight) return;
        }

        Player player = (Player)gameObject.GetNodeOrNull("../Player");
        if (player != null)
        {
            gameObject.velocity = (player.Position - gameObject.Position).Normalized() * gameObject.Stats.Speed;
        }
    }
}