using Godot;

public partial class SimpleBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        Bullet bullet = (Bullet)gameObject;
        var collision = bullet.MoveAndCollide(bullet.velocity * (float)delta);
        if (collision != null)
        {
            bullet.velocity = Vector2.Zero;
            bullet.HitTarget((Node)collision.GetCollider());
        }
    }
}