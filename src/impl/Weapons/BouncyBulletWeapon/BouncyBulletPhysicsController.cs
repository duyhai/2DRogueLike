using Godot;

public partial class BouncyBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        Bullet bullet = (Bullet)gameObject;
        var collision = bullet.MoveAndCollide(bullet.Velocity * (float)delta);
        if (collision != null)
        {
            bullet.Velocity = bullet.Velocity.Bounce(collision.GetNormal());
            SoundManager.Instance.PlaySound(SoundPaths.Bounce);
            bullet.HitTarget((Node)collision.GetCollider());
        }
    }
}