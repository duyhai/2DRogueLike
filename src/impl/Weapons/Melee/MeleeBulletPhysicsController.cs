using Godot;
using Godot.Collections;

public class MeleeBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        Bullet bullet = (Bullet)gameObject;
        Area2D swingingArea = bullet.GetNode<Area2D>("Area2D");
        Array bodies = swingingArea.GetOverlappingBodies();
        if (bodies.Count != 0)
        {
            foreach (var body in bodies)
            {
                bullet.HitTarget((Node)body);
            }
            bullet.QueueFree();
        }
    }
}