using Godot;
using System;

public class TankPhysicsController : PhysicsController {
    public override void Update(GameObject gameObject, float delta)
    {
        Tank tank = (Tank)gameObject;
        var collision = gameObject.MoveAndCollide(tank.velocity * delta);
        
        if (collision != null)
        {
            gameObject.velocity = gameObject.velocity.Slide(collision.Normal);
            tank.HitTarget(collision);
        }
    }
}