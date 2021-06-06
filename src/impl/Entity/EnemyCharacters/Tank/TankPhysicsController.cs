using Godot;
using System;

public class TankPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Tank tank = (Tank)gameObject;
        var modifiedVelocity = tank.velocity * tank.speedModifier;
        var collision = tank.MoveAndCollide(modifiedVelocity * delta);

        if (collision != null)
        {
            modifiedVelocity = modifiedVelocity.Slide(collision.Normal);
            tank.HitTarget(collision);
        }
    }
}