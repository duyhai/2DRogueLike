using Godot;

public class BouncyBullet : Bullet
{
    static int DAMAGE = 20;

    public BouncyBullet() :
        base(new NullInputController(), new BouncyBulletPhysicsController(), new NullGraphicsController(), DAMAGE)
    {
        speed = 750;
    }

    public override void HitTarget(KinematicCollision2D collision)
    {
        base.HitTarget(collision);
        var collider = (GameObject)collision.Collider;
        if (collider.GetType().BaseType.ToString() != nameof(Block))
        {
            QueueFree();
        }
    }
}
