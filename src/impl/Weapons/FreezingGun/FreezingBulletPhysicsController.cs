using Godot;

public partial class FreezingBulletPhysicsController : SimpleBulletPhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        FreezingBullet freezingBullet = (FreezingBullet)gameObject;
        var collision = freezingBullet.MoveAndCollide(freezingBullet.velocity * (float)delta);
        if (freezingBullet.Collided || collision == null) return;
        
        freezingBullet.velocity = Vector2.Zero;
        freezingBullet.HitTarget((Node)collision.GetCollider());
        freezingBullet.Collided = true;
    }
}