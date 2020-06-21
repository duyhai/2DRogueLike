using Godot;
using System.Diagnostics;

public class SimpleBulletPhysicsController : PhysicsController
{
    public override void Update(KinematicBody2D node, float delta)
    {
        Bullet bullet = (Bullet)node;
        var collision = bullet.MoveAndCollide(bullet.velocity * delta);
        if (collision != null)
        {
            var collider = collision.Collider;
            if (collider.GetType().ToString() == "BreakableWall")
            {
                var breakableWall = (BreakableWall)collider;
                breakableWall.Hit();
            }
            bullet.QueueFree();
        }
    }
}