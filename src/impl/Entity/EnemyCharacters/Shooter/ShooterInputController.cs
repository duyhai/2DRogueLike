using Godot;
using System;

public class ShooterInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        if (gameObject.isDead) return;

        Shooter shooter = (Shooter)gameObject;

        var player = shooter.SightCheck();
        if (player != null)
        {
            var bulletDirection = player.GlobalPosition - shooter.weapon.GlobalPosition;
            shooter.Shoot(bulletDirection);
        }
    }
}