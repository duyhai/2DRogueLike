
public class SimpleBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Bullet bullet = (Bullet)gameObject;
        var collision = bullet.MoveAndCollide(bullet.velocity * delta);
        if (collision != null)
        {
            var collider = collision.Collider;
            if (collider.GetType().ToString() == "BreakableWall")
            {
                var breakableWall = (BreakableWall)collider;
                breakableWall.Hit();
            }
            bullet.QueueFree();
        }
    }
}