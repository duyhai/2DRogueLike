using Godot;

public class SimpleBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Bullet bullet = (Bullet)gameObject;
        var collision = bullet.MoveAndCollide(bullet.velocity * delta);
        if (collision != null)
        {
            bullet.velocity = Vector2.Zero;
            bullet.HitTarget(collision);
        }
    }
}