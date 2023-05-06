using Godot;
using Godot.Collections;
using Utility;

public partial class BallLightningBulletV2PhysicsController : LightningBulletPhysicsController
{
    public override void Update(GameObject gameObject, double delta)
    {
        var collision = gameObject.MoveAndCollide(gameObject.Velocity * (float)delta);
        if (collision != null)
        {
            gameObject.Velocity = Vector2.Zero;
            gameObject.QueueFree();
        }

        base.Update(gameObject, delta);
    }
}