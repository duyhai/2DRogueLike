using Godot;

public partial class TankPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        Tank tank = (Tank)gameObject;
        var collision = tank.MoveAndCollide(tank.Velocity * (float)delta);

        if (collision != null)
        {
            tank.Velocity = tank.Velocity.Slide(collision.GetNormal());

            Timer attackTimer = gameObject.GetNode<Timer>("Timer");
            if (attackTimer.IsStopped())
            {
                DamageUtil.HandleDamage(gameObject, (Node)collision.GetCollider(), gameObject.Stats.Damage);
                attackTimer.Start();
            }
        }
    }
}