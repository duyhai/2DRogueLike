using Godot;
using System;

public class TankPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Tank tank = (Tank)gameObject;
        var collision = tank.MoveAndCollide(tank.velocity * delta);

        if (collision != null)
        {
            tank.velocity = tank.velocity.Slide(collision.Normal);
            tank.HitTarget(collision);
        }
    }
}