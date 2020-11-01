public class BouncyBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Bullet bullet = (Bullet)gameObject;
        var collision = bullet.MoveAndCollide(bullet.velocity * delta);
        if (collision != null)
        {
            bullet.velocity = bullet.velocity.Bounce(collision.Normal);
            bullet.HitTarget(collision);
        }
    }
}