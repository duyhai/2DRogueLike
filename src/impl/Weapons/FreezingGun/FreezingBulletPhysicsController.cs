using Godot;

public class FreezingBulletPhysicsController : SimpleBulletPhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        FreezingBullet freezingBullet = (FreezingBullet)gameObject;
        var collision = freezingBullet.MoveAndCollide(freezingBullet.velocity * delta);
        if (!freezingBullet.Collided)
        {
            if (collision != null)
            {
                freezingBullet.velocity = Vector2.Zero;
                freezingBullet.HitTarget((Node)collision.Collider);
                freezingBullet.Collided = true;
            }
        }
    }
}