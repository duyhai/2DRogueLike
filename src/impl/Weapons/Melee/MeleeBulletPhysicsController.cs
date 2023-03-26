using Godot;
using Godot.Collections;

public partial class MeleeBulletPhysicsController : PhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        Bullet bullet = (Bullet)gameObject;
        Area2D swingingArea = bullet.GetNode<Area2D>("Area2D");
        var bodies = swingingArea.GetOverlappingBodies();
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