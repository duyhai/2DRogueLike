using Godot;
using Godot.Collections;

public class RocketHitBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Bullet bullet = (Bullet)gameObject;
        Area2D hitbox = bullet.GetNode<Area2D>("Area2D");
        Array bodies = hitbox.GetOverlappingBodies();
        if (bodies.Count != 0)
        {
            foreach (var body in bodies)
            {
                bullet.HitTarget((Node)body);
            }
            bullet.SetPhysicsProcess(false);
        }
    }
}