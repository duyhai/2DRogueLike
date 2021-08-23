using Godot;
using Godot.Collections;

public class RocketExplosionPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        RocketExplosion explosion = (RocketExplosion)gameObject;
        Area2D hitbox = explosion.GetNode<Area2D>("Area2D");
        Array bodies = hitbox.GetOverlappingBodies();
        if (bodies.Count != 0)
        {
            foreach (var body in bodies)
            {
                DamageUtil.HandleDamage(explosion.initiator, (Node)body, explosion.damage);
            }
            explosion.SetPhysicsProcess(false);
        }
    }
}