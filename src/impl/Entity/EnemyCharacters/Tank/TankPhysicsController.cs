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

            Timer attackTimer = gameObject.GetNode<Timer>("Timer");
            if (attackTimer.IsStopped())
            {
                DamageUtil.HandleDamage(gameObject, (Node)collision.Collider, gameObject.Stats.Damage);
                attackTimer.Start();
            }
        }
    }
}