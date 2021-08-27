using Godot;
using Godot.Collections;
using Utility;

public class BallLightningBulletV2PhysicsController : LightningBulletPhysicsController
{
    public override void Update(GameObject gameObject, float delta)
    {
        var collision = gameObject.MoveAndCollide(gameObject.velocity * delta);
        if (collision != null)
        {
            gameObject.velocity = Vector2.Zero;
            gameObject.QueueFree();
        }

        base.Update(gameObject, delta);
    }
}