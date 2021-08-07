using Godot;
using Godot.Collections;

public class LightningBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        LightningBullet lightningBullet = (LightningBullet)gameObject;
        Area2D hitbox = gameObject.GetNode<Area2D>("Area2D");
        Array bodies = hitbox.GetOverlappingBodies();
        if (bodies.Count != 0)
        {
            foreach (var body in bodies)
            {
                DamageUtil.HandleDamage(lightningBullet, (Node)body, lightningBullet.damage);
            }
            lightningBullet.SetPhysicsProcess(false);
        }
    }
}