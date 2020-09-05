using Godot;
using System;

public class ShooterInputController : InputController 
{
    public override void Update(GameObject gameObject)
    {
        Shooter Shooter = (Shooter)gameObject;
        var player = (Player)Shooter.GetNodeOrNull("../Player");
        if (player != null)
        {
            var bulletDirection = player.weapon.GlobalPosition - Shooter.weapon.GlobalPosition;
            Shooter.Shoot(bulletDirection, Shooter.CollisionLayer, Shooter.CollisionMask);
        }
    }
}