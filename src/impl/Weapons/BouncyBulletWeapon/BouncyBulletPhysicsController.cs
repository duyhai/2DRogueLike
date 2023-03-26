using Godot;

public partial class BouncyBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        Bullet bullet = (Bullet)gameObject;
        var collision = bullet.MoveAndCollide(bullet.velocity * (float)delta);
        if (collision != null)
        {
            bullet.velocity = bullet.velocity.Bounce(collision.GetNormal());
            SoundManager.Instance.PlaySound(SoundPaths.Bounce);
            bullet.HitTarget((Node)collision.GetCollider());
        }
    }
}