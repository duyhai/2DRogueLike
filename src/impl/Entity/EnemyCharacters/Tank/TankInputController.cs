using System;
using Godot;

public class TankInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        if (gameObject.isDead) return;

        Tank Tank = (Tank)gameObject;
        Player player = (Player)Tank.GetNodeOrNull("../Player");
        Tank.velocity = (player.Position - Tank.Position).Normalized() * Tank.speed;
    }
}